using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Three3Module : ModuleType
    {
        public Three3Module(TextureLoader textures)
            : base(new bool[,] { { true, false }, { true, true } }, 
                  textures.Load("Data/Textures/three_3.png"),
                  textures.Load("Data/Textures/three_3_glow.png"),
                  true,
                  4)
        {
        }
    }
}
