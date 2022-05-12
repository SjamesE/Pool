using Pool.Graphics;
using Pool.Scenes;
using Pool.Utilities;
using SFML.Graphics;
using Utility;

namespace Pool
{
    public static class Physics
    {
        private static bool flag = false;
        private static GameObject ball1;
        private static GameObject ball2;

        public static void Update()
        {
            if (!flag)
            {
                for (int i = 0; i < GameScene.gameObjects.Count; i++)
                {
                    GameObject ball1 = GameScene.gameObjects[i];
                    for (int j = i + 1; j < GameScene.gameObjects.Count; j++)
                    {
                        GameObject ball2 = GameScene.gameObjects[j];
                        float radius = ball1.Transform.Size.x * ball1.Transform.Scale.x / 2;

                        if (CircleCollision(ball1.Center, ball2.Center, radius))
                        {
                            //flag = true;
                            PerformCollision(ball1, ball2);
                            Physics.ball1 = ball1;
                            Physics.ball2 = ball2;
                        }
                    }
                    if (!flag)
                        ball1.Update();
                }
            }
            else
            {
                Draw.Line((Vector2i)ball1.Center, (Vector2i)ball1.Center + (Vector2i)ball1.Transform.Velocity, Color.Green);
                Draw.Line((Vector2i)ball2.Center, (Vector2i)ball2.Center + (Vector2i)ball2.Transform.Velocity, Color.Green);
            }
        }

        private static void PerformCollision(GameObject ball1, GameObject ball2)
        {
            #region Comment
            /*//   A - Ball 2
            //   |\
            //   | \
            // c |  \ b
            //   |   \
            //   |    \
            //   |_____\
            //  B   a   C - Ball 1

            Vector2 A = ball1.Transform.Position;
            Vector2 B = ball2.Transform.Position;

            Vector2 vel1 = ball1.Transform.Velocity;
            Vector2 vel2 = ball2.Transform.Velocity;

            bool dirX = vel1.x < 0;
            bool dirY = vel1.y < 0;

            float a = B.x - A.x;
            float b = B.y - A.y;
            float c = JMath.Pythagoras(a, b);

            float colisionAngle = JMath.ATan(b / a);

            float totalVel1 = JMath.Pythagoras(ball1.Transform.Velocity.x, ball1.Transform.Velocity.y);

            Vector2 v1 = new Vector2(JMath.Sin(colisionAngle) * totalVel1,
                                     JMath.Cos(colisionAngle) * totalVel1);
            ball1.Transform.Velocity = v1 / 90f * colisionAngle;
            ball2.Transform.Velocity = v1 / 90f * (90f - colisionAngle);// * new Vector2(1, -1);*/
            #endregion

            // get the mtd
            Vector2 delta = ball1.Transform.Position - ball2.Transform.Position;
            float radius = 16;

            float d = JMath.Pythagoras(delta.x, delta.y);

            Vector2 mtd;
            if (d != 0.0f)
            {
                mtd = delta * (radius + radius - d) / d; // minimum translation distance to push balls apart after intersecting

            }
            else // Special case. Balls are exactly on top of eachother.  Don't want to divide by zero.
            {
                d = radius + radius - 1.0f;
                delta = new Vector2(radius + radius, 0.0f);

                mtd = delta * (radius + radius - d) / d;
            }

            // resolve intersection
            float im1 = 1; // inverse mass quantities
            float im2 = 1;

            // push-pull them apart
            ball1.Transform.Position += mtd;
            ball2.Transform.Position -= mtd;

            // impact speed
            Vector2 v = ball1.Transform.Velocity - ball2.Transform.Velocity;
            Vector2 vn1 = v * mtd.Normalize();
            float vn = vn1.x + vn1.y;

            // sphere intersecting but moving away from each other already
            if (vn > 0.0f) return;

            // collision impulse
            float i = (-(1.0f + .985f) * vn) / (im1 + im2);
            Vector2 impulse = mtd * i;

            // change in momentum
            ball1.Transform.Velocity += impulse * im1;
            ball2.Transform.Velocity -= impulse * im2;

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
