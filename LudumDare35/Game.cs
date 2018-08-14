using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace LudumDare35
{
    internal abstract class Game
    {
        protected Game(string windowTitle = "Game", uint windowWidth = 640, uint windowHeight = 480, Styles windowStyle = Styles.Default)
        {
            RenderWindow = new RenderWindow(new VideoMode(windowWidth, windowHeight), windowTitle, windowStyle);
            
            RenderWindow.Closed += Window_Closed;
        }
        
        protected RenderWindow RenderWindow { get; }
        protected Time FrameTime { get; set; } = Time.FromSeconds(1f / 120f);
        
        public void Run()
        {
            Clock clock = new Clock();
            Time timeSinceLastUpdate = Time.Zero;
            
            while (RenderWindow.IsOpen)
            {
                RenderWindow.DispatchEvents();

                timeSinceLastUpdate += clock.Restart();
                while (timeSinceLastUpdate > FrameTime)
                {
                    timeSinceLastUpdate -= FrameTime;

                    RenderWindow.DispatchEvents();
                    Update(FrameTime);
                }

                Draw();
            }
            
            Close();
            clock.Dispose();
        }
        
        protected abstract void Update(Time delta);
        protected abstract void Draw();
        protected abstract void Close();
        private void Window_Closed(object sender, EventArgs e) => RenderWindow.Close();
    }
}
