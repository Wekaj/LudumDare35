using SFML.Graphics;
using SFML.System;

namespace LudumDare35.Modules
{
    internal interface IModule
    {
        bool this[int x, int y] { get; }
        
        int Width { get; }
        int Height { get; }
        Texture Texture { get; }
        Texture Glow { get; }
        bool Solid { get; }
        int Income { get; }

        void Draw(RenderTarget target, RenderStates states, Color palette, Vector2f position);
    }
}
