using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Four3Module : ModuleType
    {
        public Four3Module(TextureLoader textures)
            : base(new bool[,] { { false, true }, { true, true }, { true, false } }, 
                  textures.Load("Data/Textures/four_3.png"),
                  textures.Load("Data/Textures/four_3_glow.png"),
                  true,
                  8)
        {
        }
    }
}
