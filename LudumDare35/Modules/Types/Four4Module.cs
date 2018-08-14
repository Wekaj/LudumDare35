using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Four4Module : ModuleType
    {
        public Four4Module(TextureLoader textures)
            : base(new bool[,] { { true, false }, { true, true }, { false, true } }, 
                  textures.Load("Data/Textures/four_4.png"),
                  textures.Load("Data/Textures/four_4_glow.png"),
                  true,
                  8)
        {
        }
    }
}
