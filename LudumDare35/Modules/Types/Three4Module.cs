using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Three4Module : ModuleType
    {
        public Three4Module(TextureLoader textures)
            : base(new bool[,] { { false, true }, { true, true } }, 
                  textures.Load("Data/Textures/three_4.png"),
                  textures.Load("Data/Textures/three_4_glow.png"),
                  true,
                  4)
        {
        }
    }
}
