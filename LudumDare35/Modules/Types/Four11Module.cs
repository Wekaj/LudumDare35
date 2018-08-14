using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Four11Module : ModuleType
    {
        public Four11Module(TextureLoader textures)
            : base(new bool[,] { { true, true, true }, { true, false, false } }, 
                  textures.Load("Data/Textures/four_11.png"),
                  textures.Load("Data/Textures/four_11_glow.png"),
                  true,
                  8)
        {
        }
    }
}
