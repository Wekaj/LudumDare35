using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Four1Module : ModuleType
    {
        public Four1Module(TextureLoader textures)
            : base(new bool[,] { { true, true, true, true } }, 
                  textures.Load("Data/Textures/four_1.png"),
                  textures.Load("Data/Textures/four_1_glow.png"),
                  true,
                  8)
        {
        }
    }
}
