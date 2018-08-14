using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Four15Module : ModuleType
    {
        public Four15Module(TextureLoader textures)
            : base(new bool[,] { { true, false, false }, { true, true, true } }, 
                  textures.Load("Data/Textures/four_15.png"),
                  textures.Load("Data/Textures/four_15_glow.png"),
                  true,
                  8)
        {
        }
    }
}
