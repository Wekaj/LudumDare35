using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Three1Module : ModuleType
    {
        public Three1Module(TextureLoader textures)
            : base(new bool[,] { { true, true, true } }, 
                  textures.Load("Data/Textures/three_1.png"),
                  textures.Load("Data/Textures/three_1_glow.png"),
                  true,
                  4)
        {
        }
    }
}
