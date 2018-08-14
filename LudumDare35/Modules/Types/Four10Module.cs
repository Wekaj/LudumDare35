using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Four10Module : ModuleType
    {
        public Four10Module(TextureLoader textures)
            : base(new bool[,] { { true, true, true }, { false, true, false } }, 
                  textures.Load("Data/Textures/four_10.png"),
                  textures.Load("Data/Textures/four_10_glow.png"),
                  true,
                  8)
        {
        }
    }
}
