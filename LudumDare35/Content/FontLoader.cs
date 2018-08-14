using SFML.Graphics;

namespace LudumDare35.Content
{
    internal sealed class FontLoader : ContentLoader<Font>
    {
        protected override Font FromFile(string filename) => new Font(filename);
    }
}
