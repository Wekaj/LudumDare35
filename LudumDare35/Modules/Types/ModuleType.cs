using SFML.Graphics;
using SFML.System;

namespace LudumDare35.Modules.Types
{
    internal abstract class ModuleType
    {
        private readonly bool[,] shape;
        private readonly Texture texture;

        protected ModuleType(bool[,] shape, Texture texture, Texture glow, bool solid, int income)
        {
            this.shape = shape;
            this.texture = texture;

            Width = shape.GetLength(0);
            Height = shape.GetLength(1);
            Glow = glow;
            Solid = solid;
            Income = income;
        }

        public bool this[int x, int y] => x >= 0 && y >= 0 && x < Width && y < Height && shape[x, y];
        
        public int Width { get; }
        public int Height { get; }
        public Texture Texture => texture;
        public Texture Glow { get; }
        public bool Solid { get; }
        public int Income { get; }

        public virtual void Draw(RenderTarget target, RenderStates states, Color palette, Vector2f position)
        {
            Sprite sprite = new Sprite(texture);
            sprite.Position = position;
            sprite.Color = palette;

            target.Draw(sprite);

            sprite.Dispose();
        }
    }
}
