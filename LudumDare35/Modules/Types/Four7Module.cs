using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Four7Module : ModuleType
    {
        public Four7Module(TextureLoader textures)
            : base(new bool[,] { { false, true }, { true, true }, { false, true } }, 
                  textures.Load("Data/Textures/four_7.png"),
                  textures.Load("Data/Textures/four_7_glow.png"),
                  true,
                  8)
        {
        }
    }
}
