using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Four8Module : ModuleType
    {
        public Four8Module(TextureLoader textures)
            : base(new bool[,] { { false, true, false }, { true, true, true } }, 
                  textures.Load("Data/Textures/four_8.png"),
                  textures.Load("Data/Textures/four_8_glow.png"),
                  true,
                  8)
        {
        }
    }
}
