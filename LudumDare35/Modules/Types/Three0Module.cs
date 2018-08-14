using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Three0Module : ModuleType
    {
        public Three0Module(TextureLoader textures)
            : base(new bool[,] { { true, true }, { true, false } }, 
                  textures.Load("Data/Textures/three_0.png"),
                  textures.Load("Data/Textures/three_0_glow.png"),
                  true,
                  4)
        {
        }
    }
}
