using Xunit;
using AssemblyPublicizer;

namespace AssemblyPublicizerTest
{
    public class PatcherTest
    {
        [Fact]
        public void TestAsmModuleAssembly()
        {
            const string refPath = @"D:\SteamLibrary\steamapps\common\Pathfinder Second Adventure\Wrath_Data\Managed\Assembly-CSharp.dll";
            const string targetPath = @"C:\Users\maste\Documents\Modding\PWOTR\Mods\Dev\AssemblyPublicizer\lib\Assembly-CSharp_public.dll";
            var module = new AsmModuleDef();
            module.Load(refPath);
            module.Publicize(false);
            module.Write(targetPath);
        }
        
        [Fact]
        public void TestDnModuleAssembly()
        {
            const string refPath = @"D:\SteamLibrary\steamapps\common\Pathfinder Second Adventure\Wrath_Data\Managed\Assembly-CSharp.dll";
            const string targetPath = @"C:\Users\maste\Documents\Modding\PWOTR\Mods\Dev\AssemblyPublicizer\lib\Assembly-CSharp_public.dll";
            var module = new DnModuleDef();
            module.Load(refPath);
            module.Publicize(false);
            module.Write(targetPath);
        }

        [Fact]
        public void TestDnModuleAccess()
        {
            const string refPath = @"C:\Users\maste\Documents\Modding\PWOTR\Mods\Dev\AccessTest\lib\AccessTest.dll";
            const string targetPath = @"C:\Users\maste\Documents\Modding\PWOTR\Mods\Dev\AccessTest\lib\AccessTest_public.dll";
            var module = new DnModuleDef();
            module.Load(refPath);
            module.Publicize(false);
            module.Write(targetPath);
        }

    }
}