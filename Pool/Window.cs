using Pool.Graphics;
using Pool.Scenes;
using SFML.Graphics;
using SFML.Window;
using Utility;

namespace Pool
{
    public class Window
    {
        public static RenderWindow RenderWindow { get; private set; }

        public static uint WINDOW_HEIGHT = 560;
        public static uint WINDOW_WIDTH = 960;

        private bool frameByFrame = false;

        GameScene GameScene;
        Draw Draw;

        public Window()
        {
            ContextSettings cs = new ContextSettings();
            cs.AntialiasingLevel = 4;

            RenderWindow = new RenderWindow(new VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT), " ", Styles.Titlebar | Styles.Close, cs);
            RenderWindow.Closed += Close;
            Draw = new Draw();
            GameScene = new GameScene();
        }
        
        public void Update()
        {
            RenderWindow.DispatchEvents();
            Input.Update();

            if (Input.GetKeyState(Keyboard.Key.F1) == Input.KeyState.downFrame0)
            {
                frameByFrame = !frameByFrame;
            }
            if (!frameByFrame || (frameByFrame && Input.GetKeyState(Keyboard.Key.Enter) == Input.KeyState.downFrame0))
            {
                GameScene.Update();
                Physics.Update();
            }

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
