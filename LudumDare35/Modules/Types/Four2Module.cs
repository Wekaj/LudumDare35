using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Four2Module : ModuleType
    {
        public Four2Module(TextureLoader textures)
            : base(new bool[,] { { true, true }, { true, true } }, 
                  textures.Load("Data/Textures/four_2.png"),
                  textures.Load("Data/Textures/four_2_glow.png"),
                  true,
                  8)
        {
        }
    }
}
