using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Three5Module : ModuleType
    {
        public Three5Module(TextureLoader textures)
            : base(new bool[,] { { true, true }, { false, true } }, 
                  textures.Load("Data/Textures/three_5.png"),
                  textures.Load("Data/Textures/three_5_glow.png"),
                  true,
                  4)
        {
        }
    }
}
