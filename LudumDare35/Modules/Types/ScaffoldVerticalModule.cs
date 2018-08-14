using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class ScaffoldVerticalModule : ModuleType
    {
        public ScaffoldVerticalModule(TextureLoader textures)
            : base(new bool[,] { { true, true } },
                  textures.Load("Data/Textures/scaffold_vertical.png"), 
                  textures.Load("Data/Textures/scaffold_vertical_glow.png"),
                  false,
                  0)
        {
        }
    }
}
