using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class SourceModule : ModuleType
    {
        public SourceModule(TextureLoader textures)
            : base(new bool[,] { { true } },
                  textures.Load("Data/Textures/source.png"),
                  textures.Load("Data/Textures/source_glow.png"),
                  true,
                  1)
        {
        }
    }
}
