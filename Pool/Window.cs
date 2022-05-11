using Pool.Graphics;
using Pool.Scenes;
using SFML.Graphics;
using SFML.Window;
using Utility;

namespace Pool
{
    internal class Window
    {
        public static RenderWindow RenderWindow { get; private set; }

        public static uint WINDOW_HEIGHT = 400;
        public static uint WINDOW_WIDTH = 600;

        GameScene GameScene;
        Draw Draw;

        public Window()
        {
            RenderWindow = new RenderWindow(new VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT), " ", Styles.Titlebar | Styles.Close);
            RenderWindow.Closed += Close;
            Draw = new Draw();
            GameScene = new GameScene();
        }
        
        public void Update()
        {
            RenderWindow.DispatchEvents();
            Input.Update();
            GameScene.Update();
            Physics.Update();

            Draw.Update();
        }

        private void Close(object sender, EventArgs e)
        {
            RenderWindow.Close();
        }

        public static bool IsWithinBounds(Vector2i pos)
        {
            if (pos.x >= 0 && pos.y >= 0)
            {
                return (pos.x <= WINDOW_WIDTH && pos.y <= WINDOW_HEIGHT);
            }
            return false;
        }
    }
}
