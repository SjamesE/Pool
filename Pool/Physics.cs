using Pool.Scenes;
using Pool.Utilities;
using Utility;

namespace Pool
{
    public static class Physics
    {
        public static void Update()
        {
            for (int i = 0; i < GameScene.gameObjects.Count; i++)
            {
                GameObject ball1 = GameScene.gameObjects[i];
                for (int j = i + 1; j < GameScene.gameObjects.Count; j++)
                {
                    GameObject ball2 = GameScene.gameObjects[j];
                    float radius = ball1.Transform.Size.x * ball1.Transform.Scale.x;

                    if (CircleCollision(ball1.Center, ball2.Center, radius))
                    {
                        PerformCollision(ball1, ball2);
                    }
                }
                ball1.Update();
            }
        }

        private static void PerformCollision(GameObject ball1, GameObject ball2)
        {
            //   A - Ball 2
            //   |\
            //   | \
            // b |  \ c
            //   |   \
            //   |    \
            //   |_____\
            //  C   a   B - Ball 1

            Vector2 A = ball1.Transform.Position;
            Vector2 B = ball2.Transform.Position;
            
            Vector2 vel1 = ball1.Transform.Velocity;
            Vector2 vel2 = ball2.Transform.Velocity;
            

            float a = Math.Abs(A.x - B.x);
            float b = Math.Abs(A.y - B.y);
            float c = JMath.Pythagoras(a, b);

            float beta = MathF.Asin(b / c);
        }

        public static bool CircleCollision(Vector2 pos1, Vector2 pos2, float radius, float? radius2 = null)
        {
            if (radius2 == null) radius2 = radius;

            float w = Math.Abs(pos1.x - pos2.x);
            float h = Math.Abs(pos1.y - pos2.y);

            float distance = JMath.Pythagoras(w, h);
            float totalRadius = radius + (float)radius2;

            return distance < totalRadius;
        }
    }
}
