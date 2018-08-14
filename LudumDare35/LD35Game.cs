using LudumDare35.Content;
using LudumDare35.Input;
using LudumDare35.Screens;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace LudumDare35
{
    internal sealed class LD35Game : Game
    {
        private const float scale = 2f;
        private readonly Sprite background;
        private RenderTexture renderTexture;

        public LD35Game()
            : base("Space Junk", 960, 640, Styles.Titlebar | Styles.Close)
        {
            View view = RenderWindow.GetView();
            view.Center = new Vector2f(RenderWindow.Size.X * 0.5f / scale, RenderWindow.Size.Y * 0.5f / scale);
            view.Size = new Vector2f(RenderWindow.Size.X / scale, RenderWindow.Size.Y / scale);
            RenderWindow.SetView(view);

            renderTexture = new RenderTexture((uint)(Window.Size.X / scale), (uint)(Window.Size.Y / scale));

            Buttons.Add("force_exit", new KeyboardButton(Keyboard.Key.Escape));
            Buttons.Add("1", new KeyboardButton(Keyboard.Key.Num1));
            Buttons.Add("2", new KeyboardButton(Keyboard.Key.Num2));
            Buttons.Add("3", new KeyboardButton(Keyboard.Key.Num3));
            Buttons.Add("4", new KeyboardButton(Keyboard.Key.Num4));
            Buttons.Add("pause", new KeyboardButton(Keyboard.Key.LShift));
            Buttons.Add("left_click", new MouseButton(Mouse.Button.Left));
            Buttons.Add("confirm", new KeyboardButton(Keyboard.Key.Return));
            Buttons.Add("restart", new KeyboardButton(Keyboard.Key.Space));

            Screens.Add(new MenuScreen(this));

            background = new Sprite(Textures.Load("Data/Textures/palette.png"), new IntRect(0, 0, 1, 1));
            background.Scale = new Vector2f(RenderTarget.Size.X, RenderTarget.Size.Y);
        }

        public Window Window => RenderWindow;
        public RenderTarget RenderTarget => renderTexture;
        public ScreenManager Screens { get; } = new ScreenManager();
        public ButtonManager Buttons { get; } = new ButtonManager();
        public TextureLoader Textures { get; } = new TextureLoader();
        public SoundBufferLoader SoundBuffers { get; } = new SoundBufferLoader();
        public FontLoader Fonts { get; } = new FontLoader();
        public Color Palette { get; } = new Color(100, 120, 100);

        protected override void Update(Time delta)
        {
            Buttons.Update();
            if (Buttons["force_exit"].Held)
                RenderWindow.Close();

            Screens.Update(delta.AsSeconds());

            background.Position = RenderTarget.GetView().Center 
                - new Vector2f(RenderTarget.Size.X, RenderTarget.Size.Y) / 2f;
            background.Color = Palette;
        }

        protected override void Draw()
        {
            renderTexture.Clear();
            renderTexture.Draw(background);
            Screens.Draw();
            renderTexture.Display();

            RenderWindow.Clear();
            RenderWindow.Draw(new Sprite(renderTexture.Texture));
            RenderWindow.Display();
        }

        protected override void Close()
        {
        }
    }
}
