using SFML.Graphics;
using Pool.Assets;
using Utility;

namespace Pool.Scenes
{
    public class GameScene
    {
        public static List<GameObject> gameObjects { get; private set; } = new();
        public static List<Line> lines { get; private set; } = new();
        public static Vector2[] holes  { get; private set; } = new Vector2[6];
        private CueBall cueBall;

        public GameScene()
        {
            Vector2 size = new Vector2(Textures.RedBall.Size);
            Vector2 scale = new Vector2(0.421875f);
            float w = scale.x * size.x;
            float y = w / 2f;
            float x = (float)Math.Sqrt(w * w - y * y);

            string name = "BlackBall";
            int tblW = (int)Window.WINDOW_WIDTH - 66;
            int tblH = (int)Window.WINDOW_HEIGHT - 68;

            cueBall = new CueBall("CueBall", new Transform(new Vector2((tblW - 30) / 4 + 30, Window.WINDOW_HEIGHT / 2), new Vector2(Textures.RedBall.Size), scale), Textures.WhiteBall);
            gameObjects.Add(cueBall);

            // Red balls
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f,         Window.WINDOW_HEIGHT / 2        ), size, scale), Textures.RedBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f +     x, Window.WINDOW_HEIGHT / 2 -     y), size, scale), Textures.RedBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f +     x, Window.WINDOW_HEIGHT / 2 +     y), size, scale), Textures.RedBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 2 * x, Window.WINDOW_HEIGHT / 2 - 2 * y), size, scale), Textures.RedBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 2 * x, Window.WINDOW_HEIGHT / 2        ), size, scale), Textures.RedBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 2 * x, Window.WINDOW_HEIGHT / 2 + 2 * y), size, scale), Textures.RedBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 3 * x, Window.WINDOW_HEIGHT / 2 - 3 * y), size, scale), Textures.RedBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 3 * x, Window.WINDOW_HEIGHT / 2 + 3 * y), size, scale), Textures.RedBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 3 * x, Window.WINDOW_HEIGHT / 2 -     y), size, scale), Textures.RedBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 3 * x, Window.WINDOW_HEIGHT / 2 +     y), size, scale), Textures.RedBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 4 * x, Window.WINDOW_HEIGHT / 2 - 4 * y), size, scale), Textures.RedBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 4 * x, Window.WINDOW_HEIGHT / 2 - 2 * y), size, scale), Textures.RedBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 4 * x, Window.WINDOW_HEIGHT / 2        ), size, scale), Textures.RedBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 4 * x, Window.WINDOW_HEIGHT / 2 + 2 * y), size, scale), Textures.RedBall);
            Instantiate(name, new Transform(new Vector2(tblW / 1.333f + 4 * x, Window.WINDOW_HEIGHT / 2 + 4 * y), size, scale), Textures.RedBall);

            // Top
            lines.Add(new Line(new Vector2(98, 67), 
                               new Vector2(Window.WINDOW_WIDTH / 2 - 32, 67)));
            lines.Add(new Line(new Vector2(Window.WINDOW_WIDTH / 2 + 28, 67), 
                               new Vector2(Window.WINDOW_WIDTH - 100, 67)));
            // Bottom
            lines.Add(new Line(new Vector2(Window.WINDOW_WIDTH / 2 - 32, tblH),
                               new Vector2(98, tblH)));
            lines.Add(new Line(new Vector2(Window.WINDOW_WIDTH - 100, tblH),
                               new Vector2(Window.WINDOW_WIDTH / 2 + 28, tblH)));
            // Left
            lines.Add(new Line(new Vector2(63, Window.WINDOW_HEIGHT - 104),
                               new Vector2(63, 105)));
            // Right
            lines.Add(new Line(new Vector2(tblW, 104),
                               new Vector2(tblW, Window.WINDOW_HEIGHT - 105)));

            // TOP LEFT
            lines.Add(new Line(new Vector2(70, 40), new Vector2(98, 67)));
            lines.Add(new Line(new Vector2(63, 105), new Vector2(37, 77)));
            
            // TOP RIGHT
            lines.Add(new Line(new Vector2(Window.WINDOW_WIDTH - 100, 66), new Vector2(Window.WINDOW_WIDTH - 75, 40)));
            lines.Add(new Line(new Vector2(Window.WINDOW_WIDTH - 34, 71), new Vector2(Window.WINDOW_WIDTH - 66, 104)));

            // BOTTOM LEFT
            lines.Add(new Line(new Vector2(32, Window.WINDOW_HEIGHT - 71), new Vector2(63, Window.WINDOW_HEIGHT - 104)));
            lines.Add(new Line(new Vector2(98, tblH), new Vector2(64, Window.WINDOW_HEIGHT - 35)));

            // BOTTOM RIGHT
            lines.Add(new Line(new Vector2(tblW, Window.WINDOW_HEIGHT - 105), new Vector2(Window.WINDOW_WIDTH - 34, Window.WINDOW_HEIGHT - 71)));
            lines.Add(new Line(new Vector2(tblW, Window.WINDOW_HEIGHT - 35), new Vector2(Window.WINDOW_WIDTH - 100, Window.WINDOW_HEIGHT - 68)));
            
            // TOP MIDDLE
            lines.Add(new Line(new Vector2(Window.WINDOW_WIDTH / 2 - 32, 67), new Vector2(Window.WINDOW_WIDTH / 2 - 25, 41)));
            lines.Add(new Line(new Vector2(Window.WINDOW_WIDTH / 2 + 23, 41), new Vector2(Window.WINDOW_WIDTH / 2 + 28, 67)));

            // BOTTOM MIDDLE
            lines.Add(new Line(new Vector2(Window.WINDOW_WIDTH / 2 - 27, Window.WINDOW_HEIGHT - 43), new Vector2(Window.WINDOW_WIDTH / 2 - 32, tblH)));
            lines.Add(new Line(new Vector2(Window.WINDOW_WIDTH / 2 + 28, tblH), new Vector2(Window.WINDOW_WIDTH / 2 + 22, Window.WINDOW_HEIGHT - 43)));

            // Hole Positions
            holes = new Vector2[6]
            {
                new Vector2(30, 35),
                new Vector2(Window.WINDOW_WIDTH / 2 - 27, 9),
                new Vector2(Window.WINDOW_WIDTH - 84, 35),
                new Vector2(30, Window.WINDOW_HEIGHT - 88),
                new Vector2(Window.WINDOW_WIDTH / 2 - 27, Window.WINDOW_HEIGHT - 60),
                new Vector2(Window.WINDOW_WIDTH - 84, Window.WINDOW_HEIGHT - 88)
            };
        }

        public void Instantiate(string name, Transform transform, Texture texture)
        {
            gameObjects.Add(new GameObject(name, transform, texture));
        }

        public void Update()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                GameObject go = gameObjects[i];
                if (!go.Active) continue;

                go.Update();
                foreach (var hole in holes)
                {
                    if (Physics.CircleCollision(go.Center, hole + new Vector2(26), 13))
                    {
                        go.Active = false;
                    }
                }
            }
            cueBall.Update();
        }

        /// <summary>
        /// Mouse raycast
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
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

        public static GameObject? Find(string name)
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
