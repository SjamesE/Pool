using Pool.Graphics;
using Pool.Scenes;
using Pool.Utilities;
using SFML.Graphics;
using Utility;

namespace Pool
{
    public static class Physics
    {
        public static void DoCollisionWithWall(Transform transform, int wall)
        {
            // wall: 0 left, 1 up, 2 right, 3 down
            Vector2 currPos = transform.Position;
            Vector2 lastPos = transform.LastPosition;
            float point;

            switch (wall)
            {
                case 0:
                    point = FindPointCollisionWall(lastPos.x, currPos.x, 0) / 100;
                    currPos = new Vector2(JMath.Lerp(lastPos.x, currPos.x, point),
                                          JMath.Lerp(lastPos.y, currPos.y, point));
                    transform.Velocity.x *= -1f;
                    transform.Position = currPos + transform.Velocity * App.FRAME_TIME * (100f - point) / 100;
                    break;
                case 1:
                    point = FindPointCollisionWall(lastPos.y, currPos.y, 0) / 100;
                    currPos = new Vector2(JMath.Lerp(lastPos.x, currPos.x, point),
                                          JMath.Lerp(lastPos.y, currPos.y, point));
                    transform.Velocity.y *= -1f;
                    transform.Position = currPos + transform.Velocity * App.FRAME_TIME * (100f - point) / 100;
                    break;
                case 2:
                    point = FindPointCollisionWall(lastPos.x, currPos.x, Window.WINDOW_WIDTH) / 100;
                    currPos = new Vector2(JMath.Lerp(lastPos.x, currPos.x, point),
                                          JMath.Lerp(lastPos.y, currPos.y, point));
                    transform.Velocity.x *= -1f;
                    transform.Position = currPos + transform.Velocity * App.FRAME_TIME * (100f - point) / 100;
                    break;
                case 3:
                    point = FindPointCollisionWall(lastPos.x, currPos.x, Window.WINDOW_HEIGHT) / 100;
                    currPos = new Vector2(JMath.Lerp(lastPos.x, currPos.x, point),
                                          JMath.Lerp(lastPos.y, currPos.y, point));
                    transform.Velocity.y *= -1f;
                    transform.Position = currPos + transform.Velocity * App.FRAME_TIME * (100f - point) / 100;
                    break;
            }
        }

        public static float FindPointCollisionWall(float first, float second, float wall)
        {
            bool a = first < second;
            for (int i = 0; i < 101; i++)
            {
                float lerpedVal = JMath.Lerp(first, second, (float)i / 100);
                if (a)
                {
                    if (first < wall) continue;
                    else return i;
                }
                else
                {
                    if (first > wall) continue;
                    else return i;
                }
            }
            return -1;
        }

        public static void Update()
        {
            // Do wall colisions
            for (int i = 0; i < GameScene.gameObjects.Count; i++)
            {
                GameObject gameObject = GameScene.gameObjects[i];
                Transform transform = gameObject.Transform;

                //Check Colision to the left
                if (transform.Position.x < 0)
                {
                    DoCollisionWithWall(gameObject.Transform, 0);
                }
                //Check Colision Up
                if (transform.Position.y < 0)
                {
                    DoCollisionWithWall(gameObject.Transform, 1);
                }
                //Check Colision to the right
                if (transform.Position.x + transform.Size.x * transform.Scale.x > Window.WINDOW_WIDTH)
                {
                    DoCollisionWithWall(gameObject.Transform, 2);
                }
                //Check Colision Down
                if (transform.Position.y + transform.Size.y * transform.Scale.y > Window.WINDOW_HEIGHT)
                {
                    DoCollisionWithWall(gameObject.Transform, 3);
                }
            }

            // Do ball colisions
            for (int i = 0; i < GameScene.gameObjects.Count; i++)
            {
                GameObject ball1 = GameScene.gameObjects[i];
                for (int j = i + 1; j < GameScene.gameObjects.Count; j++)
                {
                    //Console.WriteLine($"Last Pos: {ball1.Transform.LastPosition}, Curr Pos: {ball1.Transform.Position}");
                    GameObject ball2 = GameScene.gameObjects[j];
                    float radius = ball1.Transform.Size.x * ball1.Transform.Scale.x / 2;

                    if (CircleCollision(ball1.Center, ball2.Center, radius))
                    {
                        int k = 0;
                        Vector2 pos = ball1.Center;
                        Vector2 lastPos = ball1.Transform.LastPosition + new Vector2(16, 16);

                        Vector2 pos2 = ball2.Center;
                        Vector2 lastPos2 = ball2.Transform.LastPosition + new Vector2(16, 16);

                        for (k = 0; k < 51; k++)
                        {
                            Vector2 betweenPos = JMath.Lerp(lastPos, pos, (float)k / 50f);
                            Vector2 betweenPos2 = JMath.Lerp(lastPos2, pos2, (float)k / 50f);

                            if (CircleCollision(betweenPos, betweenPos2, radius))
                            {
                                ball1.Transform.Position = betweenPos - new Vector2(16, 16);
                                ball2.Transform.Position = betweenPos2 - new Vector2(16, 16);
                                PerformCollision(ball1, ball2);
                                break;
                            }
                        }
                        Vector2 v1 = ball1.Transform.Velocity * ((100f - (float)k) / 100f) * App.FRAME_TIME;
                        Vector2 v2 = ball2.Transform.Velocity * ((100f - (float)k) / 100f) * App.FRAME_TIME;

                        ball1.Transform.Position += v1;
                        ball2.Transform.Position += v2;
                    }
                }
            }
        }

        private static void PerformCollision(GameObject ball1, GameObject ball2)
        {
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
            ball1.Transform.Position += mtd / 2;
            ball2.Transform.Position -= mtd / 2;

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

        public static bool CircleCollision(Vector2 pos1, Vector2 pos2, float radius)
        {
            float w = pos1.x - pos2.x;
            float h = pos1.y - pos2.y;

            float distance = JMath.Pythagoras(w, h);
            float totalRadius = radius + radius;

            return distance < totalRadius;
        }
    }
}
