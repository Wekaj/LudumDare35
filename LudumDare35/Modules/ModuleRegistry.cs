using LudumDare35.Content;
using LudumDare35.Modules.Types;

namespace LudumDare35.Modules
{
    internal sealed class ModuleRegistry
    {
        public ModuleRegistry(TextureLoader textures)
        {
            Source = new SourceModule(textures);

            One = new ModuleType[]
            {
                new One0Module(textures)
            };

            Two = new ModuleType[]
            {
                new ScaffoldHorizontalModule(textures),
                new ScaffoldVerticalModule(textures)
            };

            Three = new ModuleType[] 
            {
                new Three0Module(textures),
                new Three1Module(textures),
                new Three2Module(textures),
                new Three3Module(textures),
                new Three4Module(textures),
                new Three5Module(textures)
            };

            Four = new ModuleType[]
            {
                new Four0Module(textures),
                new Four1Module(textures),
                new Four2Module(textures),
                new Four3Module(textures),
                new Four4Module(textures),
                new Four5Module(textures),
                new Four6Module(textures),
                new Four7Module(textures),
                new Four8Module(textures),
                new Four9Module(textures),
                new Four10Module(textures),
                new Four11Module(textures),
                new Four12Module(textures),
                new Four13Module(textures),
                new Four14Module(textures),
                new Four15Module(textures),
                new Four16Module(textures),
                new Four17Module(textures),
                new Four18Module(textures)
            };
        }

        public SourceModule Source { get; }
        public ModuleType[] One { get; }
        public ModuleType[] Two { get; }
        public ModuleType[] Three { get; }
        public ModuleType[] Four { get; }
    }
}
