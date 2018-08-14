using SFML.Window;

namespace LudumDare35.Input
{
    internal sealed class MouseButton : Button
    {
        private readonly Mouse.Button[] buttons;

        public MouseButton(params Mouse.Button[] buttons)
        {
            this.buttons = buttons;
        }

        protected override bool GetHeldState()
        {
            foreach (Mouse.Button button in buttons)
                if (Mouse.IsButtonPressed(button))
                    return true;
            return false;
        }
    }
}
