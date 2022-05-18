using SFML.Graphics;
using Pool.Assets;
using Utility;
using Pool.Utilities;

namespace Pool.Scenes
{
    internal class GameScene
    {
        public static List<GameObject> gameObjects { get; private set; } = new();
        private CueBall cueBall;

        public GameScene()
        {
            Vector2 size = new Vector2(Textures.BlackBall.Size);
            Vector2 scale = new Vector2(0.40625f);
            float w = scale.x * size.x;
            float y = w / 2f;
            float x = (float)Math.Sqrt(w * w - y * y);
            //Console.Write($"w: {w}, y: {y}, x: {x}");

            string name = "BlackBall";
            int tblWidth = (int)Window.WINDOW_WIDTH - 30;
            int tblHeight = (int)Window.WINDOW_HEIGHT - 30;

            cueBall = new CueBall("CueBall", new Transform(new Vector2((tblWidth - 30) / 4 + 30, Window.WINDOW_HEIGHT / 2), new Vector2(Textures.BlackBall.Size), scale), Textures.WhiteBall);
            gameObjects.Add(cueBall);

            Instantiate(name, new Transform(new Vector2(tblWidth / 1.333f,         tblHeight / 2        ), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblWidth / 1.333f +     x, tblHeight / 2 -     y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblWidth / 1.333f +     x, tblHeight / 2 +     y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblWidth / 1.333f + 2 * x, tblHeight / 2 - 2 * y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblWidth / 1.333f + 2 * x, tblHeight / 2        ), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblWidth / 1.333f + 2 * x, tblHeight / 2 + 2 * y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblWidth / 1.333f + 3 * x, tblHeight / 2 - 3 * y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblWidth / 1.333f + 3 * x, tblHeight / 2 + 3 * y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblWidth / 1.333f + 3 * x, tblHeight / 2 -     y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblWidth / 1.333f + 3 * x, tblHeight / 2 +     y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblWidth / 1.333f + 4 * x, tblHeight / 2 - 4 * y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblWidth / 1.333f + 4 * x, tblHeight / 2 - 2 * y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblWidth / 1.333f + 4 * x, tblHeight / 2        ), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblWidth / 1.333f + 4 * x, tblHeight / 2 + 2 * y), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(tblWidth / 1.333f + 4 * x, tblHeight / 2 + 4 * y), size, scale), Textures.BlackBall);
            
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
