using LudumDare35.Hazards;
using LudumDare35.Modules;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace LudumDare35.Screens
{
    internal sealed class GameScreen : IScreen
    {
        private readonly LD35Game game;
        private readonly ModuleRegistry registry;
        private readonly Fortress fortress;
        private readonly Sprite planet;
        private readonly Text text, text2;
        private readonly Texture top, right, bottom, left, topRight, topLeft, bottomRight, bottomLeft;
        private readonly List<Sprite> particles = new List<Sprite>();
        private readonly List<Sprite> stars = new List<Sprite>();
        private readonly List<Projectile> projectiles = new List<Projectile>();
        private readonly List<Sprite> destroyed = new List<Sprite>();
        private readonly Random random = new Random();
        private readonly Sprite flash;
        private readonly Sound buy, explosion, payment, pickup, place, play, pause, nope;
        private readonly Music loop;
        private ModuleLink heldModule;
        private Vector2f heldModulePosition;
        private Vector2f heldModuleOffset;
        private Vector2f shake;
        private float shakeTimer = 0f, flashTimer = 0f;
        private float shakeDirection = 50f;
        private float obstacleTimer = 0f;
        private float obstacleTime = 2f;
        private int obstacleSpeed = 10;
        private float particleTimer = 0f;
        private float borderDelay = 0f;
        private int cash = 500;
        private float refreshTimer = 0f;
        private float paymentTimer = 10f;
        private int paymentAmount = 100;
        private bool paused = false;
        private readonly Sprite pauseSprite;
        private ModuleLink nextSingle, nextScaffold, nextTriple, nextQuadruple;
        private Vector2f singlePosition, scaffoldPosition, triplePosition, quadruplePosition;
        private Text singleText, scaffoldText, tripleText, quadrupleText;
        private float timer = 0f;

        public GameScreen(LD35Game game)
        {
            this.game = game;
            registry = new ModuleRegistry(game.Textures);
            fortress = new Fortress(registry);
            planet = new Sprite(game.Textures.Load("Data/Textures/planet.png"));

            buy = new Sound(game.SoundBuffers.Load("Data/Sounds/buy.wav"));
            explosion = new Sound(game.SoundBuffers.Load("Data/Sounds/explosion.wav"));
            payment = new Sound(game.SoundBuffers.Load("Data/Sounds/payment.wav"));
            pickup = new Sound(game.SoundBuffers.Load("Data/Sounds/pickup.wav"));
            place = new Sound(game.SoundBuffers.Load("Data/Sounds/place.wav"));
            play = new Sound(game.SoundBuffers.Load("Data/Sounds/play.wav"));
            pause = new Sound(game.SoundBuffers.Load("Data/Sounds/pause.wav"));
            nope = new Sound(game.SoundBuffers.Load("Data/Sounds/nope.wav"));

            loop = new Music("Data/Sounds/loop.wav");
            loop.Loop = true;
            loop.Play();

            top = game.Textures.Load("Data/Textures/border_top.png");
            right = game.Textures.Load("Data/Textures/border_right.png");
            bottom = game.Textures.Load("Data/Textures/border_bottom.png");
            left = game.Textures.Load("Data/Textures/border_left.png");
            topRight = game.Textures.Load("Data/Textures/border_top_right.png");
            topLeft = game.Textures.Load("Data/Textures/border_top_left.png");
            bottomRight = game.Textures.Load("Data/Textures/border_bottom_right.png");
            bottomLeft = game.Textures.Load("Data/Textures/border_bottom_left.png");

            View view = game.RenderTarget.GetView();
            view.Center = fortress.Position + new Vector2f(fortress.Width + 0.5f, fortress.Height + 0.5f) * 8f;
            game.RenderTarget.SetView(view);

            text = new Text("Cash:", game.Fonts.Load("Data/Fonts/TourDeForce.ttf"), 12);
            text.Position = game.RenderTarget.GetView().TopLeft() + new Vector2f(-24f + game.RenderTarget.GetView().Size.X / 2f - text.GetLocalBounds().Width / 2f,
                4f);
            text.Color = game.Palette;

            text2 = new Text("Next Payment:", game.Fonts.Load("Data/Fonts/TourDeForce.ttf"), 12);
            text2.Position = game.RenderTarget.GetView().TopLeft() + new Vector2f(-32f + game.RenderTarget.GetView().Size.X / 2f - text2.GetLocalBounds().Width / 2f,
                20f);
            text2.Color = game.Palette;

            singlePosition = new Vector2f(game.RenderTarget.GetView().Center.X - 8f, game.RenderTarget.GetView().Bottom());
            scaffoldPosition = new Vector2f(game.RenderTarget.GetView().Center.X - 8f, game.RenderTarget.GetView().Bottom());
            triplePosition = new Vector2f(game.RenderTarget.GetView().Center.X - 8f, game.RenderTarget.GetView().Bottom());
            quadruplePosition = new Vector2f(game.RenderTarget.GetView().Center.X - 8f, game.RenderTarget.GetView().Bottom());

            singleText = new Text("1: $25b", game.Fonts.Load("Data/Fonts/TourDeForce.ttf"), 12) { Color = game.Palette };
            scaffoldText = new Text("2: $50b", game.Fonts.Load("Data/Fonts/TourDeForce.ttf"), 12) { Color = game.Palette };
            tripleText = new Text("3: $100b", game.Fonts.Load("Data/Fonts/TourDeForce.ttf"), 12) { Color = game.Palette };
            quadrupleText = new Text("4: $200b", game.Fonts.Load("Data/Fonts/TourDeForce.ttf"), 12) { Color = game.Palette };

            singleText.Position = singlePosition - new Vector2f(16f, 16f);
            scaffoldText.Position = singlePosition - new Vector2f(16f, 16f);
            tripleText.Position = singlePosition - new Vector2f(16f, 16f);
            quadrupleText.Position = singlePosition - new Vector2f(16f, 16f);

            pauseSprite = new Sprite(game.Textures.Load("Data/Textures/pause.png"));
            pauseSprite.Position = game.RenderTarget.GetView().TopLeft() + new Vector2f(game.RenderTarget.GetView().Size.X - 48f, 16f);
            pauseSprite.Color = game.Palette;

            flash = new Sprite(game.Textures.Load("Data/Textures/palette.png"), new IntRect(3, 0, 1, 1));
            flash.Position = game.RenderTarget.GetView().TopLeft() - new Vector2f(8f, 0f);
            flash.Scale = game.RenderTarget.GetView().Size + new Vector2f(16f, 0f);
            flash.Color = game.Palette;

            for (int i = 0; i < 500; i++)
            {
                Sprite star = new Sprite(game.Textures.Load("Data/Textures/palette.png"), new IntRect(random.Next(1, 4), 0, 1, 1));
                star.Position = game.RenderTarget.GetView().TopLeft() + new Vector2f((float)(random.NextDouble() * game.RenderTarget.Size.X),
                    (float)(random.NextDouble() * game.RenderTarget.Size.X));
                star.Color = game.Palette;
                stars.Add(star);
            }

            fortress.Palette = game.Palette;

            planet.Position = new Vector2f(game.RenderTarget.Size.X / 2f, 0f);
            planet.Color = game.Palette;
        }

        public float FallSpeed { get; set; } = 300f;

        public void Update(float delta)
        {
            if (nextSingle == null)
                nextSingle = new ModuleLink(fortress.CreateModule(registry.One[random.Next(registry.One.Length)]), 0, 0);
            if (nextScaffold == null)
                nextScaffold = new ModuleLink(fortress.CreateModule(registry.Two[random.Next(registry.Two.Length)]), 0, 0);
            if (nextTriple == null)
                nextTriple = new ModuleLink(fortress.CreateModule(registry.Three[random.Next(registry.Three.Length)]), 0, 0);
            if (nextQuadruple == null)
                nextQuadruple = new ModuleLink(fortress.CreateModule(registry.Four[random.Next(registry.Four.Length)]), 0, 0);

            Vector2f targetPosition = (new Vector2f(game.RenderTarget.GetView().Left() + game.RenderTarget.GetView().Size.X / 5f,
                game.RenderTarget.GetView().Size.Y - 64f) - new Vector2f(nextSingle.Right - nextSingle.Left, nextSingle.Bottom - nextSingle.Top) * 8f);
            if (cash < 25)
                targetPosition.Y += 100f;
            singlePosition += (targetPosition - singlePosition) * 10f * delta;

            targetPosition = (new Vector2f(game.RenderTarget.GetView().Left() + game.RenderTarget.GetView().Size.X * 2f / 5f,
                game.RenderTarget.GetView().Size.Y - 64f) - new Vector2f(nextScaffold.Right - nextScaffold.Left, nextScaffold.Bottom - nextScaffold.Top) * 8f);
            if (cash < 50)
                targetPosition.Y += 100f;
            scaffoldPosition += (targetPosition - scaffoldPosition) * 10f * delta;

            targetPosition = (new Vector2f(game.RenderTarget.GetView().Left() + game.RenderTarget.GetView().Size.X * 3f / 5f,
                game.RenderTarget.GetView().Size.Y - 64f) - new Vector2f(nextTriple.Right - nextTriple.Left, nextTriple.Bottom - nextTriple.Top) * 8f);
            if (cash < 100)
                targetPosition.Y += 100f;
            triplePosition += (targetPosition - triplePosition) * 10f * delta;

            targetPosition = (new Vector2f(game.RenderTarget.GetView().Left() + game.RenderTarget.GetView().Size.X * 4f / 5f,
                game.RenderTarget.GetView().Size.Y - 64f) - new Vector2f(nextQuadruple.Right - nextQuadruple.Left, nextQuadruple.Bottom - nextQuadruple.Top) * 8f);
            if (cash < 200)
                targetPosition.Y += 100f;
            quadruplePosition += (targetPosition - quadruplePosition) * 10f * delta;

            singleText.Position = singlePosition.Round() - new Vector2f(singleText.GetLocalBounds().Width / 2f, 16f);
            scaffoldText.Position = scaffoldPosition.Round() - new Vector2f(singleText.GetLocalBounds().Width / 2f, 16f);
            tripleText.Position = triplePosition.Round() - new Vector2f(singleText.GetLocalBounds().Width / 2f, 16f);
            quadrupleText.Position = quadruplePosition.Round() - new Vector2f(singleText.GetLocalBounds().Width / 2f, 16f);

            if (shakeTimer > 0f)
            {
                shakeTimer -= delta;
                shake.X += shakeDirection * delta;
                if (shake.X >= 4f)
                    shakeDirection = -200f;
                if (shake.X <= -4f)
                    shakeDirection = 200f;
            }
            else
                shake = new Vector2f(0f, 0f);

            if (flashTimer > 0f)
                flashTimer -= delta;

            View view = game.RenderTarget.GetView();
            view.Center = fortress.Position + new Vector2f(fortress.Width + 0.5f, fortress.Height + 0.5f) * 8f + new Vector2f((float)Math.Round(shake.X), (float)Math.Round(shake.Y));
            game.RenderTarget.SetView(view);

            if (heldModule == null && !paused)
            {
                timer += delta;

                obstacleTimer += delta;
                if (obstacleTimer > obstacleTime)
                {
                    obstacleTimer -= obstacleTime;
                    obstacleTime *= 0.99f;
                    obstacleTime = Math.Max(obstacleTime, 0.5f);
                    obstacleSpeed += 1;
                    obstacleSpeed = Math.Min(obstacleSpeed, 100);

                    bool horizontal = random.Next(2) == 0;
                    bool positive = random.Next(2) == 0;
                    Vector2f position = new Vector2f(horizontal ?
                        (positive ? game.RenderTarget.GetView().Left() - 48f : game.RenderTarget.GetView().Right() + 48f)
                        : game.RenderTarget.GetView().Left() + (float)(game.RenderTarget.GetView().Size.X * random.NextDouble()),
                        !horizontal ?
                        (positive ? game.RenderTarget.GetView().Top() - 48f : game.RenderTarget.GetView().Bottom() + 48f)
                        : game.RenderTarget.GetView().Top() + (float)(game.RenderTarget.GetView().Size.Y * random.NextDouble()));
                    if (position.X > fortress.Width * 16f / 2f - 48f && position.X <= fortress.Width * 16f / 2f)
                        position.X = fortress.Width * 16f / 2f - 48f;
                    if (position.X < fortress.Width * 16f / 2f + 32f && position.X >= fortress.Width * 16f / 2f)
                        position.X = fortress.Width * 16f / 2f + 32f;
                    if (position.Y > fortress.Height * 16f / 2f - 48f && position.Y <= fortress.Height * 16f / 2f)
                        position.Y = fortress.Height * 16f / 2f - 48f;
                    if (position.Y < fortress.Height * 16f / 2f + 32f && position.Y >= fortress.Height * 16f / 2f)
                        position.Y = fortress.Height * 16f / 2f + 32f;
                    Vector2f velocity = new Vector2f(horizontal ? (positive ? random.Next(10, obstacleSpeed) : -random.Next(10, obstacleSpeed)) : 0f,
                        !horizontal ? (positive ? random.Next(10, obstacleSpeed) : -random.Next(10, obstacleSpeed)) : 0f);
                    //if (horizontal)
                    //{
                    //    if (position.Y < fortress.Height * 16f / 2f)
                    //        velocity.Y -= random.Next(obstacleSpeed);
                    //    else
                    //        velocity.Y += random.Next(obstacleSpeed);
                    //}
                    //else
                    //{
                    //    if (position.X < fortress.Width * 16f / 2f)
                    //        velocity.X -= random.Next(obstacleSpeed);
                    //    else
                    //        velocity.X += random.Next(obstacleSpeed);
                    //}
                    string texture = "Data/Textures/";
                    int width = 2;
                    int height = 2;
                    switch (random.Next(4))
                    {
                        case 0:
                            texture += "junk_barrel";
                            break;
                        case 1:
                            texture += "junk_doohicky";
                            break;
                        case 2:
                            texture += "junk_plate";
                            break;
                        case 3:
                            texture += "junk_large_plate";
                            width = 3;
                            height = 3;
                            break;
                    }

                    projectiles.Add(new Projectile(position,
                        velocity,
                        game.Textures.Load(texture += ".png"),
                        game.Palette,
                        1,
                        1,
                        width,
                        height,
                        fortress));
                }

                foreach (Projectile projectile in projectiles)
                {
                    projectile.Update(delta);
                    if (projectile.Destroyed())
                    {
                        shakeTimer = 0.2f;
                        flashTimer = 0.1f;
                        explosion.Play();
                    }
                }

                refreshTimer += delta;
                if (refreshTimer >= 0.5f)
                {
                    refreshTimer -= 0.5f;
                    foreach (IModule module in fortress)
                        cash += module.Income;
                }

                paymentTimer -= delta;
                if (paymentTimer <= 0f)
                {
                    paymentTimer = 10f;
                    cash -= paymentAmount;
                    paymentAmount += 100;
                    flashTimer = 0.1f;
                    if (cash >= -100)
                        payment.Play();
                    else
                    {
                        game.Screens.Remove<GameScreen>();
                        game.Screens.Add(new LoseScreen(game, timer));
                        explosion.Play();
                        loop.Stop();
                    }
                }
            }

            text.DisplayedString = "Cash: $" + cash + "b";
            text2.DisplayedString = "Next Payment ($" + paymentAmount + "b): " + Math.Round(paymentTimer) + "s";

            float particleDelta = (heldModule != null || paused) ? delta / 64f : delta;
            particleTimer += particleDelta;
            while (particleTimer >= 0.01f)
            {
                particleTimer -= 0.01f;
                Sprite particle = new Sprite(game.Textures.Load("Data/Textures/palette.png"), new IntRect(random.Next(1, 4), 0, 1, 1));
                particle.Position = game.RenderTarget.GetView().TopLeft() + new Vector2f(game.RenderTarget.GetView().Size.X,
                    (float)(random.NextDouble() * game.RenderTarget.Size.Y));
                particle.Color = game.Palette;
                particles.Add(particle);
            }

            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Position += new Vector2f(-FallSpeed * particleDelta, 0f);
                if (particles[i].Position.X < game.RenderTarget.GetView().Left())
                {
                    particles.RemoveAt(i);
                    i--;
                }
            }

            if (borderDelay > 0f)
                borderDelay -= delta;

            if (game.Buttons["left_click"].JustHeld)
            {
                if (heldModule != null)
                {
                    Vector2i mousePosition = Mouse.GetPosition(game.Window);
                    Vector2f gamePosition = game.RenderTarget.MapPixelToCoords(mousePosition / 2);

                    Vector2i modulePosition = new Vector2i((int)Math.Round((gamePosition.X + heldModuleOffset.X) / 16f), 
                        (int)Math.Round((gamePosition.Y + heldModuleOffset.Y) / 16f));
                    while (modulePosition.X + heldModule.Left <= 0)
                        modulePosition.X++;
                    while (modulePosition.X + heldModule.Right >= fortress.Width)
                        modulePosition.X--;
                    while (modulePosition.Y + heldModule.Top <= 0)
                        modulePosition.Y++;
                    while (modulePosition.Y + heldModule.Bottom >= fortress.Height)
                        modulePosition.Y--;
                    if (heldModule.Add(fortress, modulePosition.X, modulePosition.Y))
                    {
                        if (fortress.ConnectedToSource(heldModule.Module))
                        {
                            heldModule = null;
                            place.Play();
                        }
                        else
                            heldModule.Remove(fortress);
                    }
                }
                else
                {
                    Vector2i mousePosition = Mouse.GetPosition(game.Window);
                    Vector2f gamePosition = game.RenderTarget.MapPixelToCoords(mousePosition / 2);

                    Vector2i targetPosition2 = new Vector2i((int)(gamePosition.X / 16f), (int)(gamePosition.Y / 16f));
                    if (targetPosition2.X > 0 && targetPosition2.Y > 0 && targetPosition2.X < fortress.Width - 1 && targetPosition2.Y < fortress.Height - 1)
                    {
                        IModule target = fortress[targetPosition2.X, targetPosition2.Y];
                        if (target != null && target != fortress.Source)
                        {
                            targetPosition2 = fortress.GetPosition(target);
                            heldModule = fortress.RemoveChain(target);
                            heldModuleOffset = new Vector2f(targetPosition2.X, targetPosition2.Y) * 16f - gamePosition;
                            heldModulePosition = heldModuleOffset + gamePosition;
                            pickup.Play();
                        }
                    }
                }
            }

            if (game.Buttons["pause"].JustHeld)
            {
                paused = !paused;
                if (paused)
                    pause.Play();
                else
                    play.Play();
            }

            if (heldModule == null)
            {
                if (game.Buttons["1"].JustHeld)
                {
                    if (cash >= 25)
                    {
                        heldModule = nextSingle;
                        nextSingle = null;
                        heldModulePosition = singlePosition;
                        singlePosition = new Vector2f(game.RenderTarget.GetView().Center.X - 8f, game.RenderTarget.GetView().Bottom() + 16f);
                        cash -= 25;
                    }
                    else
                        nope.Play();
                }
                else if (game.Buttons["2"].JustHeld)
                {
                    if (cash >= 50)
                    {
                        heldModule = nextScaffold;
                        nextScaffold = null;
                        heldModulePosition = scaffoldPosition;
                        scaffoldPosition = new Vector2f(game.RenderTarget.GetView().Center.X - 8f, game.RenderTarget.GetView().Bottom() + 16f);
                        cash -= 50;
                    }
                    else
                        nope.Play();
                }
                else if (game.Buttons["3"].JustHeld)
                {
                    if (cash >= 100)
                    {
                        heldModule = nextTriple;
                        nextTriple = null;
                        heldModulePosition = triplePosition;
                        triplePosition = new Vector2f(game.RenderTarget.GetView().Center.X - 8f, game.RenderTarget.GetView().Bottom() + 16f);
                        cash -= 100;
                    }
                    else
                        nope.Play();
                }
                else if (game.Buttons["4"].JustHeld)
                {
                    if (cash >= 200)
                    {
                        heldModule = nextQuadruple;
                        nextQuadruple = null;
                        heldModulePosition = quadruplePosition;
                        quadruplePosition = new Vector2f(game.RenderTarget.GetView().Center.X - 8f, game.RenderTarget.GetView().Bottom() + 16f);
                        cash -= 200;
                    }
                    else
                        nope.Play();
                }

                if (heldModule != null)
                {
                    heldModuleOffset = new Vector2f(-(heldModule.Right - heldModule.Left) * 16f / 2f, 
                        (heldModule.Top - heldModule.Bottom) * 16f / 2f);
                    borderDelay = 0.2f;
                    buy.Play();
                }
            }

            if (heldModule != null)
            {
                Vector2i mousePosition = Mouse.GetPosition(game.Window);
                Vector2f gamePosition = game.RenderTarget.MapPixelToCoords(mousePosition / 2);

                Vector2f modulePosition = gamePosition + heldModuleOffset;
                modulePosition.X = (float)Math.Round(modulePosition.X / 16f);
                modulePosition.Y = (float)Math.Round(modulePosition.Y / 16f);
                while (modulePosition.X + heldModule.Left <= 0)
                    modulePosition.X++;
                while (modulePosition.X + heldModule.Right >= fortress.Width)
                    modulePosition.X--;
                while (modulePosition.Y + heldModule.Top <= 0)
                    modulePosition.Y++;
                while (modulePosition.Y + heldModule.Bottom >= fortress.Height)
                    modulePosition.Y--;
                modulePosition *= 16f;
                heldModulePosition += (modulePosition - heldModulePosition) * 20f * delta;
            }
        }

        public void Draw()
        {
            foreach (Sprite star in stars)
                game.RenderTarget.Draw(star);

            game.RenderTarget.Draw(planet);

            foreach (Sprite module in destroyed)
                game.RenderTarget.Draw(module);

            game.RenderTarget.Draw(fortress);

            foreach (Projectile projectile in projectiles)
                projectile.Draw(game.RenderTarget, RenderStates.Default);

            foreach (Sprite particle in particles)
                game.RenderTarget.Draw(particle);

            if (heldModule != null)
            {
                heldModule.Draw(game.RenderTarget, RenderStates.Default, game.Palette, heldModulePosition);

                Vector2i mousePosition = Mouse.GetPosition(game.Window);
                Vector2f gamePosition = game.RenderTarget.MapPixelToCoords(mousePosition / 2);

                Vector2i modulePosition = new Vector2i((int)Math.Round(heldModulePosition.X / 16f) + heldModule.Left,
                    (int)Math.Round(heldModulePosition.Y / 16f) + heldModule.Top);

                if (borderDelay <= 0f)
                {
                    for (int y = modulePosition.Y - 1; y < modulePosition.Y + (heldModule.Bottom - heldModule.Top) + 1; y++)
                        for (int x = modulePosition.X - 1; x < modulePosition.X + (heldModule.Right - heldModule.Left) + 1; x++)
                        {
                            if (x <= 0 || y <= 0 || x >= fortress.Width - 1 || y >= fortress.Height - 1)
                                continue;

                            Sprite sprite = new Sprite();
                            sprite.Position = new Vector2f(x, y) * 16f;
                            sprite.Color = game.Palette;
                            if (y == 1)
                                sprite.Texture = x == 1 ? topLeft : (x == fortress.Width - 2 ? topRight : top);
                            else if (y == fortress.Height - 2)
                                sprite.Texture = x == 1 ? bottomLeft : (x == fortress.Width - 2 ? bottomRight : bottom);
                            else if (x == 1)
                                sprite.Texture = left;
                            else if (x == fortress.Width - 2)
                                sprite.Texture = right;
                            if (sprite.Texture != null)
                                game.RenderTarget.Draw(sprite);
                        }
                }
            }

            if (nextSingle != null)
                nextSingle.Draw(game.RenderTarget, RenderStates.Default, game.Palette, singlePosition);
            if (nextScaffold != null)
                nextScaffold.Draw(game.RenderTarget, RenderStates.Default, game.Palette, scaffoldPosition);
            if (nextTriple != null)
                nextTriple.Draw(game.RenderTarget, RenderStates.Default, game.Palette, triplePosition);
            if (nextQuadruple != null)
                nextQuadruple.Draw(game.RenderTarget, RenderStates.Default, game.Palette, quadruplePosition);

            game.RenderTarget.Draw(singleText);
            game.RenderTarget.Draw(scaffoldText);
            game.RenderTarget.Draw(tripleText);
            game.RenderTarget.Draw(quadrupleText);

            if (paused)
                game.RenderTarget.Draw(pauseSprite);

            if (flashTimer > 0f)
                game.RenderTarget.Draw(flash);

            game.RenderTarget.Draw(text);
            game.RenderTarget.Draw(text2);
        }
    }
}
