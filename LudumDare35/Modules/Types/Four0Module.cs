using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Four0Module : ModuleType
    {
        public Four0Module(TextureLoader textures)
            : base(new bool[,] { { true }, { true }, { true }, { true } }, 
                  textures.Load("Data/Textures/four_0.png"),
                  textures.Load("Data/Textures/four_0_glow.png"),
                  true,
                  8)
        {
        }
    }
}
