using AsmResolver.DotNet;
using System.Linq;


namespace AssemblyPublicizer
{
    public static class AsmModuleExtension
    {
        public static void Publicize(this ModuleDefinition moduleDefinition, bool publicizeExplicitImpls = false, bool publicizeCompilerGeneratedFields = false)
        {
            foreach (var type in moduleDefinition.GetAllTypes())
            {
                type.IsSealed = false;
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
                    if (publicizeCompilerGeneratedFields || !field.HasCustomAttribute("System.Runtime.CompilerServices", "CompilerGeneratedAttribute"))
                    {
                        field.IsPublic = true;
                    }
                }
            }
        }
    }
}