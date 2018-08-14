using LudumDare35.Content;

namespace LudumDare35.Modules.Types
{
    internal sealed class Four6Module : ModuleType
    {
        public Four6Module(TextureLoader textures)
            : base(new bool[,] { { false, true, true }, { true, true, false } }, 
                  textures.Load("Data/Textures/four_6.png"),
                  textures.Load("Data/Textures/four_6_glow.png"),
                  true,
                  8)
        {
        }
    }
}
