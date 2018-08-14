using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace LudumDare35.Screens
{
    internal sealed class MenuScreen : IScreen
    {
        private readonly LD35Game game;
        private readonly Text title, help, paragraph1, paragraph2, paragraph3, paragraph4, paragraph5, paragraph6;
        private readonly Sound play;
        private readonly Music loop;
        private int phase = 0;

        public MenuScreen(LD35Game game)
        {
            this.game = game;

            Font font = game.Fonts.Load("Data/Fonts/upheavtt.ttf");
            title = new Text("Space Junk", font, 20);
            title.Position = Position(title, -(game.RenderTarget.GetView().Size.Y / 2f) - 64f);
            title.Color = game.Palette;

            help = new Text("Push enter to continue.", game.Fonts.Load("Data/Fonts/TourDeForce.ttf"), 12);
            help.Position = Position(help, -(game.RenderTarget.GetView().Size.Y / 2f) - 64f);
            help.Color = game.Palette;

            paragraph1 = new Text("Use keys 1 - 4 to buy modules of their respective sizes.", 
                game.Fonts.Load("Data/Fonts/TourDeForce.ttf"), 12);
            paragraph1.Position = Position(paragraph1, game.RenderTarget.GetView().Size.Y / 2f);
            paragraph1.Color = game.Palette;

            paragraph2 = new Text("Place modules with a left click, adjacent to existing modules.",
                game.Fonts.Load("Data/Fonts/TourDeForce.ttf"), 12);
            paragraph2.Position = Position(paragraph2, game.RenderTarget.GetView().Size.Y / 2f);
            paragraph2.Color = game.Palette;

            paragraph3 = new Text("Pick up existing modules with a left click.",
                game.Fonts.Load("Data/Fonts/TourDeForce.ttf"), 12);
            paragraph3.Position = Position(paragraph3, game.RenderTarget.GetView().Size.Y / 2f);
            paragraph3.Color = game.Palette;

            paragraph4 = new Text("Earn enough cash to pay off your orbit-space rent (larger modules = more income).",
                game.Fonts.Load("Data/Fonts/TourDeForce.ttf"), 12);
            paragraph4.Position = Position(paragraph4, game.RenderTarget.GetView().Size.Y / 2f);
            paragraph4.Color = game.Palette;

            paragraph5 = new Text("Press shift to pause the game.",
                game.Fonts.Load("Data/Fonts/TourDeForce.ttf"), 12);
            paragraph5.Position = Position(paragraph5, game.RenderTarget.GetView().Size.Y / 2f);
            paragraph5.Color = game.Palette;

            paragraph6 = new Text("Watch out for space junk!",
                game.Fonts.Load("Data/Fonts/TourDeForce.ttf"), 12);
            paragraph6.Position = Position(paragraph6, game.RenderTarget.GetView().Size.Y / 2f);
            paragraph6.Color = game.Palette;

            play = new Sound(game.SoundBuffers.Load("Data/Sounds/play.wav"));

            loop = new Music("Data/Sounds/menu_loop.wav");
            loop.Loop = true;
            loop.Play();
        }

        public void Update(float delta)
        {
            if (game.Buttons["confirm"].JustHeld)
            {
                phase++;
                play.Play();
            }

            switch (phase)
            {
                case 0:
                    {
                        Vector2f targetPosition = Position(title, -32f);
                        title.Position += (targetPosition - title.Position) * 10f * delta;
                        title.Position = title.Position.Round();

                        targetPosition = Position(help, 12f);
                        help.Position += (targetPosition - help.Position) * 10f * delta;
                        help.Position = help.Position.Round();
                    }
                    break;
                case 1:
                    {
                        Vector2f targetPosition = Position(title, -128f);
                        title.Position += (targetPosition - title.Position) * 10f * delta;
                        title.Position = title.Position.Round();

                        targetPosition = Position(help, -84f);
                        help.Position += (targetPosition - help.Position) * 10f * delta;
                        help.Position = help.Position.Round();

                        targetPosition = Position(paragraph1, -32f);
                        paragraph1.Position += (targetPosition - paragraph1.Position) * 10f * delta;
                        paragraph1.Position = paragraph1.Position.Round();
                    }
                    break;
                case 2:
                    {
                        Vector2f targetPosition = Position(title, -128f);
                        title.Position += (targetPosition - title.Position) * 10f * delta;
                        title.Position = title.Position.Round();

                        targetPosition = Position(help, -84f);
                        help.Position += (targetPosition - help.Position) * 10f * delta;
                        help.Position = help.Position.Round();

                        targetPosition = Position(paragraph1, -32f);
                        paragraph1.Position += (targetPosition - paragraph1.Position) * 10f * delta;
                        paragraph1.Position = paragraph1.Position.Round();

                        targetPosition = Position(paragraph2, -16f);
                        paragraph2.Position += (targetPosition - paragraph2.Position) * 10f * delta;
                        paragraph2.Position = paragraph2.Position.Round();
                    }
                    break;
                case 3:
                    {
                        Vector2f targetPosition = Position(title, -128f);
                        title.Position += (targetPosition - title.Position) * 10f * delta;
                        title.Position = title.Position.Round();

                        targetPosition = Position(help, -84f);
                        help.Position += (targetPosition - help.Position) * 10f * delta;
                        help.Position = help.Position.Round();

                        targetPosition = Position(paragraph1, -32f);
                        paragraph1.Position += (targetPosition - paragraph1.Position) * 10f * delta;
                        paragraph1.Position = paragraph1.Position.Round();

                        targetPosition = Position(paragraph2, -16f);
                        paragraph2.Position += (targetPosition - paragraph2.Position) * 10f * delta;
                        paragraph2.Position = paragraph2.Position.Round();

                        targetPosition = Position(paragraph3, 0f);
                        paragraph3.Position += (targetPosition - paragraph3.Position) * 10f * delta;
                        paragraph3.Position = paragraph3.Position.Round();
                    }
                    break;
                case 4:
                    {
                        Vector2f targetPosition = Position(title, -128f);
                        title.Position += (targetPosition - title.Position) * 10f * delta;
                        title.Position = title.Position.Round();

                        targetPosition = Position(help, -84f);
                        help.Position += (targetPosition - help.Position) * 10f * delta;
                        help.Position = help.Position.Round();

                        targetPosition = Position(paragraph1, -32f);
                        paragraph1.Position += (targetPosition - paragraph1.Position) * 10f * delta;
                        paragraph1.Position = paragraph1.Position.Round();

                        targetPosition = Position(paragraph2, -16f);
                        paragraph2.Position += (targetPosition - paragraph2.Position) * 10f * delta;
                        paragraph2.Position = paragraph2.Position.Round();

                        targetPosition = Position(paragraph3, 0f);
                        paragraph3.Position += (targetPosition - paragraph3.Position) * 10f * delta;
                        paragraph3.Position = paragraph3.Position.Round();

                        targetPosition = Position(paragraph4, 16f);
                        paragraph4.Position += (targetPosition - paragraph4.Position) * 10f * delta;
                        paragraph4.Position = paragraph4.Position.Round();
                    }
                    break;
                case 5:
                    {
                        Vector2f targetPosition = Position(title, -128f);
                        title.Position += (targetPosition - title.Position) * 10f * delta;
                        title.Position = title.Position.Round();

                        targetPosition = Position(help, -84f);
                        help.Position += (targetPosition - help.Position) * 10f * delta;
                        help.Position = help.Position.Round();

                        targetPosition = Position(paragraph1, -32f);
                        paragraph1.Position += (targetPosition - paragraph1.Position) * 10f * delta;
                        paragraph1.Position = paragraph1.Position.Round();

                        targetPosition = Position(paragraph2, -16f);
                        paragraph2.Position += (targetPosition - paragraph2.Position) * 10f * delta;
                        paragraph2.Position = paragraph2.Position.Round();

                        targetPosition = Position(paragraph3, 0f);
                        paragraph3.Position += (targetPosition - paragraph3.Position) * 10f * delta;
                        paragraph3.Position = paragraph3.Position.Round();

                        targetPosition = Position(paragraph4, 16f);
                        paragraph4.Position += (targetPosition - paragraph4.Position) * 10f * delta;
                        paragraph4.Position = paragraph4.Position.Round();

                        targetPosition = Position(paragraph5, 32f);
                        paragraph5.Position += (targetPosition - paragraph5.Position) * 10f * delta;
                        paragraph5.Position = paragraph5.Position.Round();
                    }
                    break;
                case 6:
                    {
                        Vector2f targetPosition = Position(title, -128f);
                        title.Position += (targetPosition - title.Position) * 10f * delta;
                        title.Position = title.Position.Round();

                        targetPosition = Position(help, -84f);
                        help.Position += (targetPosition - help.Position) * 10f * delta;
                        help.Position = help.Position.Round();

                        targetPosition = Position(paragraph1, -32f);
                        paragraph1.Position += (targetPosition - paragraph1.Position) * 10f * delta;
                        paragraph1.Position = paragraph1.Position.Round();

                        targetPosition = Position(paragraph2, -16f);
                        paragraph2.Position += (targetPosition - paragraph2.Position) * 10f * delta;
                        paragraph2.Position = paragraph2.Position.Round();

                        targetPosition = Position(paragraph3, 0f);
                        paragraph3.Position += (targetPosition - paragraph3.Position) * 10f * delta;
                        paragraph3.Position = paragraph3.Position.Round();

                        targetPosition = Position(paragraph4, 16f);
                        paragraph4.Position += (targetPosition - paragraph4.Position) * 10f * delta;
                        paragraph4.Position = paragraph4.Position.Round();

                        targetPosition = Position(paragraph5, 32f);
                        paragraph5.Position += (targetPosition - paragraph5.Position) * 10f * delta;
                        paragraph5.Position = paragraph5.Position.Round();

                        targetPosition = Position(paragraph6, 48f);
                        paragraph6.Position += (targetPosition - paragraph6.Position) * 10f * delta;
                        paragraph6.Position = paragraph6.Position.Round();
                    }
                    break;
                default:
                    {
                        Vector2f targetPosition = Position(title, -256f);
                        title.Position += (targetPosition - title.Position) * 10f * delta;
                        title.Position = title.Position.Round();

                        targetPosition = Position(help, -256f);
                        help.Position += (targetPosition - help.Position) * 10f * delta;
                        help.Position = help.Position.Round();

                        targetPosition = Position(paragraph1, game.RenderTarget.GetView().Size.Y);
                        paragraph1.Position += (targetPosition - paragraph1.Position) * 10f * delta;
                        paragraph1.Position = paragraph1.Position.Round();

                        targetPosition = Position(paragraph2, game.RenderTarget.GetView().Size.Y);
                        paragraph2.Position += (targetPosition - paragraph2.Position) * 10f * delta;
                        paragraph2.Position = paragraph2.Position.Round();

                        targetPosition = Position(paragraph3, game.RenderTarget.GetView().Size.Y);
                        paragraph3.Position += (targetPosition - paragraph3.Position) * 10f * delta;
                        paragraph3.Position = paragraph3.Position.Round();

                        targetPosition = Position(paragraph4, game.RenderTarget.GetView().Size.Y);
                        paragraph4.Position += (targetPosition - paragraph4.Position) * 10f * delta;
                        paragraph4.Position = paragraph4.Position.Round();

                        targetPosition = Position(paragraph5, game.RenderTarget.GetView().Size.Y);
                        paragraph5.Position += (targetPosition - paragraph5.Position) * 10f * delta;
                        paragraph5.Position = paragraph5.Position.Round();

                        targetPosition = Position(paragraph6, game.RenderTarget.GetView().Size.Y);
                        paragraph6.Position += (targetPosition - paragraph6.Position) * 10f * delta;
                        paragraph6.Position = paragraph6.Position.Round();

                        loop.Volume -= 100f * delta;
                        if (loop.Volume <= 0.1f)
                        {
                            loop.Stop();
                            game.Screens.Remove<MenuScreen>();
                            game.Screens.Add(new GameScreen(game));
                        }
                    }
                    break;
            }
        }

        public void Draw()
        {
            game.RenderTarget.Draw(title);
            game.RenderTarget.Draw(help);
            game.RenderTarget.Draw(paragraph1);
            game.RenderTarget.Draw(paragraph2);
            game.RenderTarget.Draw(paragraph3);
            game.RenderTarget.Draw(paragraph4);
            game.RenderTarget.Draw(paragraph5);
            game.RenderTarget.Draw(paragraph6);
        }

        private Vector2f Position(Text text, float offset)
        {
            return new Vector2f((int)(game.RenderTarget.GetView().Size.X / 2f - text.GetLocalBounds().Width / 2f),
                game.RenderTarget.GetView().Size.Y / 2f + offset);
        }
    }
}
