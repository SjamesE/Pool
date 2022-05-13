using SFML.Graphics;
using Pool.Assets;
using Utility;

namespace Pool.Scenes
{
    internal class GameScene
    {
        public static List<GameObject> gameObjects { get; private set; } = new();
        private CueBall cueBall;

        public GameScene()
        {
            cueBall = new CueBall("CueBall", new Transform(new Vector2(Window.WINDOW_WIDTH /4, Window.WINDOW_HEIGHT / 2), new Vector2(Textures.BlackBall.Size), new Vector2(0.5f, 0.5f)), Textures.WhiteBall);

            gameObjects.Add(cueBall);
            Vector2 size = new Vector2(Textures.BlackBall.Size);
            Vector2 scale = new Vector2(0.5f, 0.5f);
            string name = "BlackBall";
            Instantiate(name, new Transform(new Vector2(Window.WINDOW_WIDTH / 4 * 3, Window.WINDOW_HEIGHT / 2), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(Window.WINDOW_WIDTH / 4 * 3 + 16,  Window.WINDOW_HEIGHT / 2 - 16), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(Window.WINDOW_WIDTH / 4 * 3 + 16,  Window.WINDOW_HEIGHT / 2 + 16), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(Window.WINDOW_WIDTH / 4 * 3 + 32,  Window.WINDOW_HEIGHT / 2 - 32), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(Window.WINDOW_WIDTH / 4 * 3 + 32,  Window.WINDOW_HEIGHT / 2     ), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(Window.WINDOW_WIDTH / 4 * 3 + 32,  Window.WINDOW_HEIGHT / 2 + 32), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(Window.WINDOW_WIDTH / 4 * 3 + 64,  Window.WINDOW_HEIGHT / 2 - 48), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(Window.WINDOW_WIDTH / 4 * 3 + 64,  Window.WINDOW_HEIGHT / 2 + 48), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(Window.WINDOW_WIDTH / 4 * 3 + 64,  Window.WINDOW_HEIGHT / 2 - 16), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(Window.WINDOW_WIDTH / 4 * 3 + 64,  Window.WINDOW_HEIGHT / 2 + 16), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(Window.WINDOW_WIDTH / 4 * 3 + 96, Window.WINDOW_HEIGHT / 2 - 64), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(Window.WINDOW_WIDTH / 4 * 3 + 96, Window.WINDOW_HEIGHT / 2 - 32), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(Window.WINDOW_WIDTH / 4 * 3 + 96, Window.WINDOW_HEIGHT / 2     ), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(Window.WINDOW_WIDTH / 4 * 3 + 96, Window.WINDOW_HEIGHT / 2 + 32), size, scale), Textures.BlackBall);
            Instantiate(name, new Transform(new Vector2(Window.WINDOW_WIDTH / 4 * 3 + 96, Window.WINDOW_HEIGHT / 2 + 64), size, scale), Textures.BlackBall);
            //Instantiate(name, new Transform(new Vector2(100, 100), size, scale), Textures.BlackBall);
            //for (int x = 50; x < 950; x += 60)
            //{
            //    for (int y = 0; y < 600; y += 60)
            //    {
            //        Instantiate(name, new Transform(new Vector2(x, y), size, scale), Textures.BlackBall);
            //    }
            //}
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
