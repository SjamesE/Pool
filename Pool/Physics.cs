using Pool.Graphics;
using Pool.Scenes;
using Pool.Utilities;
using Utility;

namespace Pool
{
    public static class Physics
    {
        public static readonly float radius = GameScene.gameObjects[0].Transform.ScaledSize.x / 2;

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
                            Vector2 betweenPos  = JMath.Lerp(lastPos,  pos,  k / 50f);
                            Vector2 betweenPos2 = JMath.Lerp(lastPos2, pos2, k / 50f);

                            if (CircleCollision(betweenPos, betweenPos2, radius))
                            {
                                ball1.Transform.Position = betweenPos  - new Vector2(radius);
                                ball2.Transform.Position = betweenPos2 - new Vector2(radius);
                                PerformCollision(ball1, ball2);
                                break;
                            }
                        }
                        Vector2 v1 = ball1.Transform.Velocity * ((100f - k) / 100f) * App.PHYSICS_UPDATE;
                        Vector2 v2 = ball2.Transform.Velocity * ((100f - k) / 100f) * App.PHYSICS_UPDATE;

                        ball1.Transform.Position += v1;
                        ball2.Transform.Position += v2;
                    }
                }
            }
            
            // Do wall collisions

            // Go though each game object
            for (int i = 0; i < GameScene.gameObjects.Count; i++)
            {
                GameObject go = GameScene.gameObjects[i];
                if (!go.Active) continue; // Skip if not active

                // Go though each line (wall)
                for (int j = 0; j < GameScene.lines.Count; j++)
                {
                    Line wall = GameScene.lines[j];

                    // Check for collision
                    if (CheckLineCircle(wall, go.Transform.Position, radius))
                    {
                        // TODO: Replace this with a binary search to find the collision quicker

                        int k;
                        Vector2 pos = go.Transform.Position;
                        Vector2 lastPos = go.Transform.LastPosition;

                        for (k = 0; k < 51; k++)
                        {
                            Vector2 betweenPos = JMath.Lerp(lastPos, pos, k / 50f);

                            if (CheckLineCircle(wall, betweenPos, radius))
                            {
                                go.Transform.Position = betweenPos;
                                PerformLineCircleCollision(go, wall);
                                break;
                            }
                        }
                        Vector2 v1 = go.Transform.Velocity * ((100f - k) / 100f) * App.PHYSICS_UPDATE;

                        go.Transform.Position += v1;
                    }
                }
            }
        }

        private static void PerformCollision(GameObject ball1, GameObject ball2)
        {
            float collisionCoef = 0.95f;

            // get the mtd
            Vector2 distVec = ball1.Transform.Position - ball2.Transform.Position;
            float radius = ball1.Transform.ScaledSize.x / 2;

            float dist = JMath.Pythagoras(distVec.x, distVec.y);

            Vector2 mtd;
            if (dist == 0.0f) // Special case. Balls are exactly on top of eachother.  Don't want to divide by zero.
            {
                dist = radius + radius - 1.0f;
                distVec = new Vector2(radius + radius, 0.0f);
            }
            mtd = distVec * (radius + radius - dist) / dist;

            // resolve intersection

            // push-pull them apart
            ball1.Transform.Position += mtd / 2;
            ball2.Transform.Position -= mtd / 2;

            // impact speed
            Vector2 vel = ball1.Transform.Velocity - ball2.Transform.Velocity;
            Vector2 vn1 = vel * mtd.Normalize();
            float vn = vn1.x + vn1.y;

            // sphere intersecting but moving away from each other already
            if (vn > 0.0f) return;

            // collision impulse
            float imp = -1.985f * vn / 2f;
            Vector2 impulse = mtd * imp;

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

        public static bool CircleCollision(Vector2 pos1, Vector2 pos2, float r)
        {
            var distVec = pos1 - pos2;

            float dist = JMath.Pythagoras(distVec.x, distVec.y);

            return dist < r + r;
        }

        // LINE/CIRCLE
        public static bool CheckLineCircle(Line line, Vector2 circle, float r)
        {
            float minX = Math.Min(line.pos1.x, line.pos2.x);
            float maxX = Math.Max(line.pos1.x, line.pos2.x);
            
            float minY = Math.Min(line.pos1.y, line.pos2.y);
            float maxY = Math.Max(line.pos1.y, line.pos2.y);
            
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
            var dist = line.pos1 - line.pos2;
            float lenPow2 = (dist.x * dist.x) + (dist.y * dist.y);

            // get dot product of the line and circle
            float dot = (float)(( (circle.x - line.pos1.x) * (line.pos2.x - line.pos1.x) +
                                  (circle.y - line.pos1.y) * (line.pos2.y - line.pos1.y) ) /
                                  lenPow2);

            // find the closest point on the line
            Vector2 closest = new Vector2(line.pos1.x + dot * (line.pos2.x - line.pos1.x),
                                          line.pos1.y + dot * (line.pos2.y - line.pos1.y));

            // is this point actually on the line segment?
            // if so keep going, but if not, return false
            bool onSegment = PointOnLine(line.pos1, line.pos2, closest);
            if (!onSegment) return false;

            // get distance to closest point
            dist = closest - circle;
            float distance = (dist.x * dist.x) + (dist.y * dist.y);

            // note the distance is not sqrt'ed(sqrt = slow), so I did r^2
            return distance <= r * r;
        }

        // POINT/CIRCLE
        private static bool PointInsideCircle(Vector2 point, Vector2 circle, float r)
        {
            // get distance between the point and circle's center
            var dist = point - circle;
            float distancePow2 = (dist.x * dist.x) + (dist.y * dist.y);

            // if the distance is less than the circle's
            // radius the point is inside!
            // note the distance is not sqrt'ed(sqrt = slow), so I did r^2
            return distancePow2 <= r * r;
        }

        // LINE/POINT
        private static bool PointOnLine(Vector2 v1, Vector2 v2, Vector2 point)
        {

            // get distance from the point to the two ends of the line
            float d1 = Vector2.Distance(point, v1);
            float d2 = Vector2.Distance(point, v2);

            // get the length of the line
            float lineLen = Vector2.Distance(v1, v2);
            float buffer = 0.01f;

            // if the two distances are equal to the line's
            // length, the point is on the line!
            // note I use the buffer here to give a +-range
            return d1 + d2 >= lineLen - buffer &&
                   d1 + d2 <= lineLen + buffer;
        }

        public static object? CheckAllColistions(Vector2 pos, float r = -1, object? ignore = null)
        {
            if (r == -1) r = radius;

            foreach (var ball in GameScene.gameObjects)
            {
                if (!ball.Active) continue;
                if (CircleCollision(pos, ball.Center, r))
                    if (ignore == null || ignore != null && ignore != ball) return ball;
            }

            foreach (var hole in GameScene.holes)
            {                                                      // r/2 so that the cue ball overlapps with the hole a little
                if (CircleCollision(pos, hole + new Vector2(26), 13 + r / 2))
                    if (ignore == null || ignore != null && ignore != (object)hole) return hole;
            }

            foreach (var line in GameScene.lines)
            {
                if (CheckLineCircle(line, pos - new Vector2(r), r))
                    if (ignore == null || ignore != null && ignore != line) return line;
            }

            return null;
        }

        public static float SphereDistance(Vector2 origin, Vector2 target, float r)
        {
            return JMath.Pythagoras(origin, target) - r;
        }

        public static float LineDistance(Vector2 origin, Line target)
        {
            Vector2 lineVec = new (target.pos2.x - target.pos1.x, target.pos2.y - target.pos1.y);
            Vector2 originToLineVec = new (origin.x - target.pos1.x, origin.y - target.pos1.y);
            //Console.WriteLine($"LineVec: {lineVec}, originTolineVec: {originToLineVec}");
            float lineLen = JMath.Pythagoras(target.pos1, target.pos2);

            // Get the dot product
            float dot = (lineVec.x * originToLineVec.x + lineVec.y * originToLineVec.y) / lineLen;

            // find the closest point on the line
            Vector2 closest = new Vector2(target.pos1.x + dot * lineVec.Normalize().x,
                                          target.pos1.y + dot * lineVec.Normalize().y);

            if (PointOnLine(target.pos1, target.pos2, closest))
            {
                //Draw.circlesPos.Add(closest);
                //Draw.circles.Add(radius);
                return JMath.Pythagoras(origin, closest);
            }
            return Math.Min(JMath.Pythagoras(origin, target.pos1), JMath.Pythagoras(origin, target.pos2));
        }

        public static RaycastHit RayMarch(Vector2 origin, Vector2 direction, float r = -1, int maxSteps = 500)
        {
            if (r == -1) r = radius;

            float traveledDist = 0;
            float minDist = 0;

            for (int i = 0; i < maxSteps; i++)
            {
                // Update traveled distance
                traveledDist += minDist;
                
                // Draw each ray-marching circle
                //if (minDist != 0)
                //{
                //    Draw.circlesPos.Add(origin + direction * (traveledDist - minDist));
                //    Draw.circles.Add(minDist + r);
                //}

                // Upate curr pos
                var currPos = origin + direction * traveledDist;


                minDist = float.MaxValue;

                // Check GameObjects
                foreach (var item in GameScene.gameObjects)
                {
                    // Skip origin object
                    if (item.Center == origin)
                        continue;

                    var distVec = currPos - item.Center;

                    var dist = JMath.Pythagoras(distVec.x, distVec.y) - r - r;

                    if (dist < minDist)
                    {
                        minDist = dist;
                        // if dist under limit, then return
                        if (minDist < 0.001f)
                        {
                            return new RaycastHit(origin, direction, origin + direction * (traveledDist + minDist), item);
                        }
                    }
                }

                // Check lines
                foreach (var item in GameScene.lines)
                {
                    var dist = LineDistance(currPos, item) - r;

                    if (dist < minDist)
                    {
                        minDist = dist;
                        // if dist under limit, then return
                        if (minDist < 0.001f)
                        {
                            return new RaycastHit(origin, direction, origin + direction * (traveledDist + minDist), null);
                        }
                    }
                }

                // Check holes
                foreach (var hole in GameScene.holes)
                {

                    var distVec = currPos - hole - new Vector2(26);

                    var dist = JMath.Pythagoras(distVec.x, distVec.y) - (r + r + 13);

                    if (dist < minDist)
                    {
                        minDist = dist;
                        // if dist under limit, then return
                        if (minDist < 0.001f)
                        {
                            return new RaycastHit(origin, direction, origin + direction * (traveledDist + minDist), null);
                        }
                    }
                }
            }

            return new(origin, direction);
        }

        public struct RaycastHit
        {
            public bool hasHit { get; private set; } = false;
            public Vector2 origin { get; private set; }
            public Vector2? point { get; private set; } = null;
            public Vector2 rayDir { get; private set; }
            public Vector2? hitNormal
            {
                get
                {
                    if (hasHit)
                        return (other.Center - point).Normalize();
                    return null;
                }
            }
            public float? distance
            {
                get
                {
                    if (hasHit)
                        return Vector2.Distance(origin, point);
                    return null;
                }
            }
            public GameObject? other { get; private set; } = null;

            public RaycastHit(Vector2 origin, Vector2 rayDir) 
            {
                this.origin = origin;
                this.rayDir = rayDir;
                hasHit = false;
            }

            public RaycastHit(Vector2 origin, Vector2 rayDir, Vector2 point, GameObject? other)
            {
                hasHit = true;

                this.origin = origin;
                this.rayDir = rayDir;
                this.point = point;
                this.other = other;
            }
        }
    }
}
