using SFML.Audio;

namespace LudumDare35.Content
{
    internal sealed class SoundBufferLoader : ContentLoader<SoundBuffer>
    {
        protected override SoundBuffer FromFile(string filename) => new SoundBuffer(filename);
    }
}
