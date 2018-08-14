using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Four14Module : ModuleType
    {
        public Four14Module(TextureLoader textures)
            : base(new bool[,] { { false, false, true }, { true, true, true } }, 
                  textures.Load("Data/Textures/four_14.png"),
                  textures.Load("Data/Textures/four_14_glow.png"),
                  true,
                  8)
        {
        }
    }
}
