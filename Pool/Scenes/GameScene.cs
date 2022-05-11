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
            cueBall = new CueBall("CueBall", new Transform(new Vector2(184, 184), new Vector2(Textures.BlackBall.Size), new Vector2(0.5f, 0.5f)), Textures.WhiteBall);

            gameObjects.Add(cueBall);
            //Instantiate("BlackBall", new Transform(new Vector2(384, 184), new Vector2(Textures.BlackBall.Size), new Vector2(0.5f, 0.5f)), Textures.BlackBall);
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
