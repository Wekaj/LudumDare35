using SFML.Graphics;

namespace LudumDare35.Content
{
    internal sealed class TextureLoader : ContentLoader<Texture>
    {
        protected override Texture FromFile(string filename) => new Texture(filename);
    }
}
