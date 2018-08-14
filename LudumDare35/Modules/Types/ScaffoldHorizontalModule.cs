using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class ScaffoldHorizontalModule : ModuleType
    {
        public ScaffoldHorizontalModule(TextureLoader textures)
            : base(new bool[,] { { true }, { true } },
                  textures.Load("Data/Textures/scaffold_horizontal.png"), 
                  textures.Load("Data/Textures/scaffold_horizontal_glow.png"),
                  false,
                  0)
        {
        }
    }
}
