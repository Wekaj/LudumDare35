using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace LudumDare35.Modules
{
    internal sealed class ModuleLink
    {
        public ModuleLink(IModule module, int x, int y)
        {
            Module = module;
            X = x;
            Y = y;
        }

        public IModule Module { get; }
        public int X { get; }
        public int Y { get; }
        public List<ModuleLink> Links { get; } = new List<ModuleLink>();

        public int Left
        {
            get
            {
                int left = X;
                foreach (ModuleLink link in Links)
                    left = Math.Min(left, link.Left);
                return left;
            }
        }

        public int Top
        {
            get
            {
                int top = Y;
                foreach (ModuleLink link in Links)
                    top = Math.Min(top, link.Top);
                return top;
            }
        }

        public int Right
        {
            get
            {
                int right = X + Module.Width;
                foreach (ModuleLink link in Links)
                    right = Math.Max(right, link.Right);
                return right;
            }
        }

        public int Bottom
        {
            get
            {
                int bottom = Y + Module.Height;
                foreach (ModuleLink link in Links)
                    bottom = Math.Max(bottom, link.Bottom);
                return bottom;
            }
        }

        public void Draw(RenderTarget target, RenderStates states, Color palette, Vector2f position)
        {
            Module.Draw(target, states, palette, position + new Vector2f(X, Y) * 16f);

            Sprite glow = new Sprite(Module.Glow);
            glow.Position = position + new Vector2f(X, Y) * 16f;
            glow.Color = palette;
            target.Draw(glow);

            foreach (ModuleLink link in Links)
                link.Draw(target, states, palette, position);
        }
        
        public List<Sprite> Split(Color palette, Vector2f position, List<Sprite> sprites)
        {
            sprites.Add(new Sprite(Module.Texture) { Position = position + new Vector2f(X, Y) * 16f,
                Color = palette });
            foreach (ModuleLink link in Links)
                link.Split(palette, position, sprites);
            return sprites;
        }

        public bool Add(Fortress fortress, int x, int y)
        {
            if (!fortress.AddModule(Module, x + X, y + Y))
                return false;
            foreach (ModuleLink link in Links)
                if (!link.Add(fortress, x, y))
                {
                    fortress.RemoveModule(Module);
                    return false;
                }
            return true;
        }

        public void Remove(Fortress fortress)
        {
            fortress.RemoveModule(Module);
            foreach (ModuleLink link in Links)
                link.Remove(fortress);
        }
    }
}
