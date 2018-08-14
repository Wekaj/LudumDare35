using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Three2Module : ModuleType
    {
        public Three2Module(TextureLoader textures)
            : base(new bool[,] { { true }, { true }, { true } }, 
                  textures.Load("Data/Textures/three_2.png"),
                  textures.Load("Data/Textures/three_2_glow.png"),
                  true,
                  4)
        {
        }
    }
}
