using AsmResolver.DotNet;
using System.Linq;

namespace AssemblyPublicizer
{
    public class AsmModuleDef : PublicizableAssembly
    {
        private ModuleDefinition? _moduleDefinition;

        public override void Dispose()
        {
            _moduleDefinition = null;
        }

        public override void Load(string filePath)
        {
            _moduleDefinition = ModuleDefinition.FromFile(filePath);
        }
        public override void Publicize(bool publicizeExplicitImpls)
        {
            if (_moduleDefinition is null) return;
            foreach (var type in _moduleDefinition.GetAllTypes())
            {
                if (type.IsNested)
                    type.IsNestedPublic = true;
                else
                    type.IsPublic = true;

                foreach (var method in type.Methods)
                {
                    // Do not publicize methodImplementations unless explicitly requested
                    var isImplementation = type.MethodImplementations.Any(i => i.Body == method);
                    if (publicizeExplicitImpls || !isImplementation)
                    {
                        method.IsPublic = true;
                    }
                }

                foreach (var field in type.Fields)
                {
                    field.IsPublic = true;
                }
            }
        }
        public override void Write(string filePath)
        {
            _moduleDefinition?.Write(filePath);
        }
    }
}