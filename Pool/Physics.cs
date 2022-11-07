using Pool.Scenes;
using Pool.Utilities;
using Utility;

namespace Pool
{
    public static class Physics
    {
        static float radius = GameScene.gameObjects[0].Transform.ScaledSize.x / 2;
        static float FindPointCollisionWall(float first, float second, float wall)
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
            // Do ball collisions
            for (int i = 0; i < GameScene.gameObjects.Count; i++)
            {
                GameObject ball1 = GameScene.gameObjects[i];
                if (!ball1.Active) continue;

                for (int j = i + 1; j < GameScene.gameObjects.Count; j++)
                {
                    //Console.WriteLine($"Last Pos: {ball1.Transform.LastPosition}, Curr Pos: {ball1.Transform.Position}");
                    GameObject ball2 = GameScene.gameObjects[j];
                    if (!ball2.Active) continue;

                    if (CircleCollision(ball1.Center, ball2.Center, radius))
                    {
                        // TODO: Replace this with a binary search to find the collision quicker

                        int k;
                        Vector2 pos     = ball1.Center;
                        Vector2 lastPos = ball1.Transform.LastPosition + new Vector2(radius);

                        Vector2 pos2     = ball2.Center;
                        Vector2 lastPos2 = ball2.Transform.LastPosition + new Vector2(radius);

                        for (k = 0; k < 51; k++)
                        {
                            Vector2 betweenPos  = JMath.Lerp(lastPos,  pos,  (float)k / 50f);
                            Vector2 betweenPos2 = JMath.Lerp(lastPos2, pos2, (float)k / 50f);

                            if (CircleCollision(betweenPos, betweenPos2, radius))
                            {
                                ball1.Transform.Position = betweenPos  - new Vector2(radius);
                                ball2.Transform.Position = betweenPos2 - new Vector2(radius);
                                PerformCollision(ball1, ball2);
                                break;
                            }
                        }
                        Vector2 v1 = ball1.Transform.Velocity * ((100f - (float)k) / 100f) * App.PHYSICS_UPDATE;
                        Vector2 v2 = ball2.Transform.Velocity * ((100f - (float)k) / 100f) * App.PHYSICS_UPDATE;

                        ball1.Transform.Position += v1;
                        ball2.Transform.Position += v2;
                    }
                }
            }
            
            // Do wall collisions
            for (int i = 0; i < GameScene.gameObjects.Count; i++)
            {
                GameObject go = GameScene.gameObjects[i];
                if (!go.Active) continue;

                for (int j = 0; j < GameScene.lines.Count; j++)
                {
                    Line wall = GameScene.lines[j];

                    if (CheckLineCircle(wall, go.Transform.Position, radius))
                    {
                        // TODO: Replace this with a binary search to find the collision quicker

                        int k;
                        Vector2 pos = go.Transform.Position;
                        Vector2 lastPos = go.Transform.LastPosition;

                        for (k = 0; k < 51; k++)
                        {
                            Vector2 betweenPos = JMath.Lerp(lastPos, pos, (float)k / 50f);

                            if (CheckLineCircle(wall, betweenPos, radius))
                            {
                                go.Transform.Position = betweenPos;
                                PerformLineCircleCollision(go, wall);
                                break;
                            }
                        }
                        Vector2 v1 = go.Transform.Velocity * ((100f - (float)k) / 100f) * App.PHYSICS_UPDATE;

                        go.Transform.Position += v1;
                    }
                }
            }
        }

        private static void PerformCollision(GameObject ball1, GameObject ball2)
        {
            float collisionCoef = 0.95f;

            // get the mtd
            Vector2 delta = ball1.Transform.Position - ball2.Transform.Position;
            float radius = ball1.Transform.ScaledSize.x / 2;

            float d = JMath.Pythagoras(delta.x, delta.y);

            Vector2 mtd;
            if (d == 0.0f) // Special case. Balls are exactly on top of eachother.  Don't want to divide by zero.
            {
                d = radius + radius - 1.0f;
                delta = new Vector2(radius + radius, 0.0f);
            }
            mtd = delta * (radius + radius - d) / d;

            // resolve intersection

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
            float i = (-(1.0f + .985f) * vn) / 2;
            Vector2 impulse = mtd * i;

            // change velocity
            ball1.Transform.Velocity += impulse * collisionCoef;
            ball2.Transform.Velocity -= impulse * collisionCoef;
        }

        private static void PerformLineCircleCollision(GameObject ball, Line line)
        {
            Vector2 u = line.normal * (ball.Transform.Velocity.Dot(line.normal) / line.normal.Dot(line.normal));
            Vector2 w = ball.Transform.Velocity - u;

            ball.Transform.Velocity = w - u;
        }

        public static bool CircleCollision(Vector2 pos1, Vector2 pos2, float radius)
        {
            float w = pos1.x - pos2.x;
            float h = pos1.y - pos2.y;

            float distance = JMath.Pythagoras(w, h);
            float totalRadius = radius + radius;

            return distance < totalRadius;
        }

        // LINE/CIRCLE
        public static bool CheckLineCircle(Line line, Vector2 circle, float r)
        {
            float minX, maxX, minY, maxY;

            minX = Math.Min(line.pos1.x, line.pos2.x);
            maxX = Math.Max(line.pos1.x, line.pos2.x);
            
            minY = Math.Min(line.pos1.y, line.pos2.y);
            maxY = Math.Max(line.pos1.y, line.pos2.y);
            
            if (circle.x + 2 * r < minX) return false;
            if (circle.x > maxX) return false;
            if (circle.y + 2 * r < minY) return false;
            if (circle.y > maxY) return false;

            circle += new Vector2(13.5f);

            // is either end INSIDE the circle?
            // if so, return true immediately
            bool inside1 = PointInsideCircle(line.pos1, circle, r);
            bool inside2 = PointInsideCircle(line.pos2, circle, r);
            if (inside1 || inside2) return true;

            // get length of the line
            float distX = line.pos1.x - line.pos2.x;
            float distY = line.pos1.y - line.pos2.y;
            float len = (float)Math.Sqrt((distX * distX) + (distY * distY));

            // get dot product of the line and circle
            float dot = (float)((((circle.x - line.pos1.x) * (line.pos2.x - line.pos1.x)) + ((circle.y - line.pos1.y) * (line.pos2.y - line.pos1.y))) / Math.Pow(len, 2));

            // find the closest point on the line
            Vector2 closest = new Vector2(line.pos1.x + (dot * (line.pos2.x - line.pos1.x)),
                                          line.pos1.y + (dot * (line.pos2.y - line.pos1.y)));

            // is this point actually on the line segment?
            // if so keep going, but if not, return false
            bool onSegment = PointOnLine(line.pos1, line.pos2, closest);
            if (!onSegment) return false;

            // get distance to closest point
            distX = closest.x - circle.x;
            distY = closest.y - circle.y;
            float distance = (float)Math.Sqrt((distX * distX) + (distY * distY));

            if (distance <= r)
            {
                return true;
            }
            return false;
        }

        // POINT/CIRCLE
        private static bool PointInsideCircle(Vector2 point, Vector2 circle, float r)
        {
            // get distance between the point and circle's center
            // using the point.ythagorean Theorem
            float distX = point.x - circle.x;
            float distY = point.y - circle.y;
            float distance = (float)Math.Sqrt((distX * distX) + (distY * distY));

            // if the distance is less than the circle's
            // radius the point is inside!
            if (distance <= r)
            {
                return true;
            }
            return false;
        }

        // LINE/POINT
        private static bool PointOnLine(Vector2 v1, Vector2 v2, Vector2 point)
        {

            // get distance from the point to the two ends of the line
            float d1 = Vector2.Distance(point, v1);
            float d2 = Vector2.Distance(point, v2);

            // get the length of the line
            float lineLen = Vector2.Distance(v1, v2);

            // since floats are so minutely accurate, add
            // a little buffer zone that will give collision
            float buffer = 0.01f;    // higher = less accurate

            // if the two distances are equal to the line's
            // length, the point is on the line!
            // note we use the buffer here to give a range,
            // rather than one 
            if (d1 + d2 >= lineLen - buffer && d1 + d2 <= lineLen + buffer)
            {
                return true;
            }
            return false;
        }

        public static object? CheckAllColistions(Vector2 pos, float r = -1, object? ignore = null)
        {
            if (r == -1) r = radius;

            foreach (var ball in GameScene.gameObjects)
            {
                if (!ball.Active) continue;
                if (CircleCollision(pos, ball.Center, r))
                    if (ignore == null || ignore != null && ignore != (object)ball) return ball;
            }

            foreach (var hole in GameScene.holes)
            {
                if (CircleCollision(pos, hole + new Vector2(26), 13 + r / 2))
                    if (ignore == null || ignore != null && ignore != (object)hole) return hole;
            }

            foreach (var line in GameScene.lines)
            {
                if (CheckLineCircle(line, pos - new Vector2(r), r))
                    if (ignore == null || ignore != null && ignore != (object)line) return line;
            }

            return null;
        }
    }
}
