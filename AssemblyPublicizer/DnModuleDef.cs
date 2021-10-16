using dnlib.DotNet;

namespace AssemblyPublicizer
{
    public class DnModuleDef : PublicizableAssembly
    {
        private ModuleDef? _moduleDef;

        public override void Dispose()
        {
            _moduleDef = null;
        }

        public override void Load(string filePath)
        {
            _moduleDef = ModuleDefMD.Load(filePath);
        }
        public override void Publicize(bool publicizeExplicitImpls)
        {
            if (_moduleDef is null) return;
            foreach (var type in _moduleDef.GetTypes()!)
            {
                type.Attributes &= ~TypeAttributes.VisibilityMask;
                type.Attributes |= type.IsNested ? TypeAttributes.NestedPublic : TypeAttributes.Public;

                foreach (var method in type.Methods)
                {
                    if (!publicizeExplicitImpls && method.IsFinal && method.IsVirtual) continue;
                    method.Attributes &= ~MethodAttributes.MemberAccessMask;
                    method.Attributes |= MethodAttributes.Public;
                }

                foreach (var field in type.Fields)
                {
                    field.Attributes &= ~FieldAttributes.FieldAccessMask;
                    field.Attributes |= FieldAttributes.Public;
                }
            }
        }
        public override void Write(string filePath)
        {
            _moduleDef?.Write(filePath);
        }
    }
}