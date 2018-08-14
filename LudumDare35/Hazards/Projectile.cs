using LudumDare35.Modules;
using SFML.Graphics;
using SFML.System;

namespace LudumDare35.Hazards
{
    internal sealed class Projectile : Drawable
    {
        private readonly Sprite sprite;
        private readonly Fortress fortress;
        private bool destroyed = false;

        public Projectile(Vector2f position, Vector2f velocity, Texture texture, Color palette, int xScale, int yScale, int width, int height, Fortress fortress)
        {
            this.sprite = new Sprite(texture) { Position = position, Color = palette, Scale = new Vector2f(xScale, yScale) };
            this.fortress = fortress;
            
            Velocity = velocity;
            Width = width;
            Height = height;
        }
        
        public Vector2f Velocity { get; }
        public int Width { get; }
        public int Height { get; }

        public void Update(float delta)
        {
            sprite.Position += Velocity * delta;
            int x = (int)((sprite.Position.X + 2f) / 16f);
            int y = (int)((sprite.Position.Y + 2f) / 16f);

            for (int currentY = y; currentY < y + Height; currentY++)
            {
                if (currentY <= 0 || currentY >= fortress.Height - 1)
                    continue;
                for (int currentX = x; currentX < x + Width; currentX++)
                {
                    if (currentX <= 0 || currentX >= fortress.Width - 1 || fortress[currentX, currentY] == null || !fortress[currentX, currentY].Solid)
                        continue;
                    destroyed = true;
                    fortress.RemoveChain(fortress[currentX, currentY]);
                }
            }
        }

        public bool Destroyed()
        {
            bool wasDestroyed = destroyed;
            destroyed = false;
            return wasDestroyed;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(sprite);
        }
    }
}
