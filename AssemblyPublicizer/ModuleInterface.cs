using System;

namespace AssemblyPublicizer
{
    public interface IPublicizable
    {
        void Publicize(bool publicizeExplicitImpls);
    }

    public interface IModuleDef
    {
        void Load(string filePath);
        void Write(string filePath);
    }

    public abstract class PublicizableAssembly : IDisposable, IModuleDef, IPublicizable
    {
        public abstract void Dispose();
        public abstract void Load(string filePath);
        public abstract void Publicize(bool publicizeExplicitImpls);
        public abstract void Write(string filePath);
    }
}