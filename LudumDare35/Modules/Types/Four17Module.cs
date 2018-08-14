using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Four17Module : ModuleType
    {
        public Four17Module(TextureLoader textures)
            : base(new bool[,] { { true, true }, { true, false }, { true, false } }, 
                  textures.Load("Data/Textures/four_17.png"),
                  textures.Load("Data/Textures/four_17_glow.png"),
                  true,
                  8)
        {
        }
    }
}
