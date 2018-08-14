using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Four18Module : ModuleType
    {
        public Four18Module(TextureLoader textures)
            : base(new bool[,] { { true, true, true }, { false, false, true } }, 
                  textures.Load("Data/Textures/four_18.png"),
                  textures.Load("Data/Textures/four_18_glow.png"),
                  true,
                  8)
        {
        }
    }
}
