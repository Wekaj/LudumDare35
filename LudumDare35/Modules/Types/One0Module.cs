using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class One0Module : ModuleType
    {
        public One0Module(TextureLoader textures)
            : base(new bool[,] { { true } }, 
                  textures.Load("Data/Textures/one_0.png"),
                  textures.Load("Data/Textures/one_0_glow.png"),
                  true,
                  1)
        {
        }
    }
}
