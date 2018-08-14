namespace LudumDare35.Input
{
    internal abstract class Button
    {
        public bool Held { get; private set; } = false;
        public bool WasHeld { get; private set; } = false;
        public bool JustHeld => Held && !WasHeld;
        public bool JustReleased => !Held && WasHeld;

        public void Update()
        {
            WasHeld = Held;
            Held = GetHeldState();
        }

        protected abstract bool GetHeldState();
    }
}
