using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Four9Module : ModuleType
    {
        public Four9Module(TextureLoader textures)
            : base(new bool[,] { { true, false }, { true, true }, { true, false } }, 
                  textures.Load("Data/Textures/four_9.png"),
                  textures.Load("Data/Textures/four_9_glow.png"),
                  true,
                  8)
        {
        }
    }
}
