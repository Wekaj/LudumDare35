using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Four13Module : ModuleType
    {
        public Four13Module(TextureLoader textures)
            : base(new bool[,] { { true, false }, { true, false }, { true, true } }, 
                  textures.Load("Data/Textures/four_13.png"),
                  textures.Load("Data/Textures/four_13_glow.png"),
                  true,
                  8)
        {
        }
    }
}
