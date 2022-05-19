using SFML.Graphics;
using Pool.Assets;
using Utility;

namespace Pool.Scenes
{
    public class GameScene
    {
        public static List<GameObject> gameObjects { get; private set; } = new();
        public static List<Line> lines { get; private set; } = new();
        private CueBall cueBall;

        public GameScene()
        {
            Vector2 size = new Vector2(Textures.BlackBall.Size);
            Vector2 scale = new Vector2(0.421875f);
            float w = scale.x * size.x;
            float y = w / 2f;
            float x = (float)Math.Sqrt(w * w - y * y);

            string name = "BlackBall";
            int tblW = (int)Window.WINDOW_WIDTH  - 60;
            int tblH = (int)Window.WINDOW_HEIGHT - 60;

            cueBall = new CueBall("CueBall", new Transform(new Vector2((tblW - 30) / 4 + 30, Window.WINDOW_HEIGHT / 2), new Vector2(Textures.BlackBall.Size), scale), Textures.WhiteBall);
            gameObjects.Add(cueBall);

            Instantiate(name, new Transform(new Vector2(tblW / 1.333f,         Window.WINDOW_HEIGHT / 2        ), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f +     x, Window.WINDOW_HEIGHT / 2 -     y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f +     x, Window.WINDOW_HEIGHT / 2 +     y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 2 * x, Window.WINDOW_HEIGHT / 2 - 2 * y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 2 * x, Window.WINDOW_HEIGHT / 2        ), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 2 * x, Window.WINDOW_HEIGHT / 2 + 2 * y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 3 * x, Window.WINDOW_HEIGHT / 2 - 3 * y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 3 * x, Window.WINDOW_HEIGHT / 2 + 3 * y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 3 * x, Window.WINDOW_HEIGHT / 2 -     y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 3 * x, Window.WINDOW_HEIGHT / 2 +     y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 4 * x, Window.WINDOW_HEIGHT / 2 - 4 * y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 4 * x, Window.WINDOW_HEIGHT / 2 - 2 * y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 4 * x, Window.WINDOW_HEIGHT / 2        ), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 4 * x, Window.WINDOW_HEIGHT / 2 + 2 * y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 4 * x, Window.WINDOW_HEIGHT / 2 + 4 * y), size, scale), Textures.BlackBall);

            lines.Add(new Line(new Vector2(90                          , 60), 
                               new Vector2(Window.WINDOW_WIDTH / 2 - 35, 60)));
            lines.Add(new Line(new Vector2(Window.WINDOW_WIDTH / 2 + 35, 60), 
                               new Vector2(Window.WINDOW_WIDTH     - 90, 60)));
            lines.Add(new Line(new Vector2(90                          , tblH), 
                               new Vector2(Window.WINDOW_WIDTH / 2 - 35, tblH)));
            lines.Add(new Line(new Vector2(Window.WINDOW_WIDTH / 2 + 35, tblH), 
                               new Vector2(Window.WINDOW_WIDTH     - 90, tblH)));
            lines.Add(new Line(new Vector2(60  ,                        90),
                               new Vector2(60  , Window.WINDOW_HEIGHT - 90)));
            lines.Add(new Line(new Vector2(tblW,                        90),
                               new Vector2(tblW, Window.WINDOW_HEIGHT - 90)));

            lines.Add(new Line(new Vector2(60, 90), new Vector2(21, 50)));
            lines.Add(new Line(new Vector2(90, 60), new Vector2(50, 21)));
            
            lines.Add(new Line(new Vector2(Window.WINDOW_WIDTH - 60, 90), new Vector2(Window.WINDOW_WIDTH - 21, 50)));
            lines.Add(new Line(new Vector2(Window.WINDOW_WIDTH - 90, 60), new Vector2(Window.WINDOW_WIDTH - 50, 21)));


            lines.Add(new Line(new Vector2(60, Window.WINDOW_HEIGHT - 90), new Vector2(21, Window.WINDOW_HEIGHT - 50)));
            lines.Add(new Line(new Vector2(90, Window.WINDOW_HEIGHT - 60), new Vector2(50, Window.WINDOW_HEIGHT - 21)));

            lines.Add(new Line(new Vector2(Window.WINDOW_WIDTH - 60, Window.WINDOW_HEIGHT - 90), new Vector2(Window.WINDOW_WIDTH - 21, Window.WINDOW_HEIGHT - 50)));
            lines.Add(new Line(new Vector2(Window.WINDOW_WIDTH - 90, Window.WINDOW_HEIGHT - 60), new Vector2(Window.WINDOW_WIDTH - 50, Window.WINDOW_HEIGHT - 21)));
            
            lines.Add(new Line(new Vector2(Window.WINDOW_WIDTH / 2 - 35, 60), new Vector2(Window.WINDOW_WIDTH / 2 - 19, 43)));
            lines.Add(new Line(new Vector2(Window.WINDOW_WIDTH / 2 + 35, 60), new Vector2(Window.WINDOW_WIDTH / 2 + 19, 43)));

            lines.Add(new Line(new Vector2(Window.WINDOW_WIDTH / 2 - 35, Window.WINDOW_HEIGHT - 60), new Vector2(Window.WINDOW_WIDTH / 2 - 19, Window.WINDOW_HEIGHT - 43)));
            lines.Add(new Line(new Vector2(Window.WINDOW_WIDTH / 2 + 35, Window.WINDOW_HEIGHT - 60), new Vector2(Window.WINDOW_WIDTH / 2 + 19, Window.WINDOW_HEIGHT - 43)));
        }

        public void Instantiate(string name, Transform transform, Texture texture)
        {
            gameObjects.Add(new GameObject(name, transform, texture));
        }

        public void Update()
        {
            foreach (var item in gameObjects)
            {
                item.Update();
            }
            cueBall.Update();
        }

        public static GameObject? Raycast(Vector2 position)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.Transform.Position <= position && gameObject.BottomRight >= position)
                {
                    return gameObject;
                }
            }
            return null;
        }

        public GameObject? Find(string name)
        {
            foreach (var gameObject in gameObjects)
            {
                if (gameObject.Name == name)
                    return gameObject;
            }
            
            return null;
        }
    }
}
