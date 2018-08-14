using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Four12Module : ModuleType
    {
        public Four12Module(TextureLoader textures)
            : base(new bool[,] { { true, true }, { false, true }, { false, true } }, 
                  textures.Load("Data/Textures/four_12.png"),
                  textures.Load("Data/Textures/four_12_glow.png"),
                  true,
                  8)
        {
        }
    }
}
