using SFML.Window;

namespace LudumDare35.Input
{
    internal sealed class KeyboardButton : Button
    {
        private readonly Keyboard.Key[] keys;

        public KeyboardButton(params Keyboard.Key[] keys)
        {
            this.keys = keys;
        }

        protected override bool GetHeldState()
        {
            foreach (Keyboard.Key key in keys)
                if (Keyboard.IsKeyPressed(key))
                    return true;
            return false;
        }
    }
}
