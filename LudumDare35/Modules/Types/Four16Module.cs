using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Four16Module : ModuleType
    {
        public Four16Module(TextureLoader textures)
            : base(new bool[,] { { false, true }, { false, true }, { true, true } }, 
                  textures.Load("Data/Textures/four_16.png"),
                  textures.Load("Data/Textures/four_16_glow.png"),
                  true,
                  8)
        {
        }
    }
}
