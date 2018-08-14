using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Four5Module : ModuleType
    {
        public Four5Module(TextureLoader textures)
            : base(new bool[,] { { true, true, false }, { false, true, true } }, 
                  textures.Load("Data/Textures/four_5.png"),
                  textures.Load("Data/Textures/four_5_glow.png"),
                  true,
                  8)
        {
        }
    }
}
