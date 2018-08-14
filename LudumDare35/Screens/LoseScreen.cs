using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using System;

namespace LudumDare35.Screens
{
    internal sealed class LoseScreen : IScreen
    {
        private readonly LD35Game game;
        private readonly Text fail, text, nextText;
        private int phase = 0;
        private bool restarting = false;
        private float timer = 0f;
        private readonly Sound play;

        public LoseScreen(LD35Game game, float time)
        {
            this.game = game;
            play = new Sound(game.SoundBuffers.Load("Data/Sounds/play.wav"));

            game.RenderTarget.SetView(game.RenderTarget.DefaultView);

            Font font = game.Fonts.Load("Data/Fonts/upheavtt.ttf");
            fail = new Text("Operation Failure", font, 20);
            fail.Position = Position(fail, -(game.RenderTarget.GetView().Size.Y / 2f) - 64f);
            fail.Color = game.Palette;

            text = new Text("Your facility ran for " + Math.Floor(time) + " seconds.", game.Fonts.Load("Data/Fonts/TourDeForce.ttf"), 12);
            text.Position = Position(text, -(game.RenderTarget.GetView().Size.Y / 2f) - 64f);
            text.Color = game.Palette;

            nextText = new Text("Push enter to continue, or space to restart.", game.Fonts.Load("Data/Fonts/TourDeForce.ttf"), 12);
            nextText.Position = Position(nextText, -(game.RenderTarget.GetView().Size.Y / 2f) - 64f);
            nextText.Color = game.Palette;
        }

        public void Update(float delta)
        {
            if (phase == 0 && game.Buttons["confirm"].JustHeld)
            {
                phase++;
                play.Play();
            }
            else if (phase == 0 && game.Buttons["restart"].JustHeld)
            {
                phase++;
                restarting = true;
                play.Play();
            }

            switch (phase)
            {
                case 0:
                    {
                        Vector2f targetPosition = Position(fail, -32f);
                        fail.Position += (targetPosition - fail.Position) * 10f * delta;
                        fail.Position = fail.Position.Round();

                        targetPosition = Position(text, 12f);
                        text.Position += (targetPosition - text.Position) * 10f * delta;
                        text.Position = text.Position.Round();

                        targetPosition = Position(nextText, 28f);
                        nextText.Position += (targetPosition - nextText.Position) * 10f * delta;
                        nextText.Position = nextText.Position.Round();
                    }
                    break;
                default:
                    {
                        Vector2f targetPosition = Position(fail, -256f);
                        fail.Position += (targetPosition - fail.Position) * 10f * delta;
                        fail.Position = fail.Position.Round();

                        targetPosition = Position(text, -256f);
                        text.Position += (targetPosition - text.Position) * 10f * delta;
                        text.Position = text.Position.Round();

                        targetPosition = Position(nextText, -256f);
                        nextText.Position += (targetPosition - nextText.Position) * 10f * delta;
                        nextText.Position = nextText.Position.Round();

                        timer += delta;
                        if (timer >= 0.75f)
                        {
                            game.Screens.Remove<LoseScreen>();
                            if (restarting)
                                game.Screens.Add(new GameScreen(game));
                            else
                                game.Screens.Add(new MenuScreen(game));
                        }
                    }
                    break;
            }
        }

        public void Draw()
        {
            game.RenderTarget.Draw(fail);
            game.RenderTarget.Draw(text);
            game.RenderTarget.Draw(nextText);
        }

        private Vector2f Position(Text text, float offset)
        {
            return new Vector2f((int)(game.RenderTarget.GetView().Size.X / 2f - text.GetLocalBounds().Width / 2f),
                game.RenderTarget.GetView().Size.Y / 2f + offset);
        }
    }
}
