using Pool.Graphics;
using Pool.Scenes;
using SFML.Graphics;
using Utility;

namespace Pool
{
    public class CueBall : GameObject
    {
        private bool isDragged;

        private bool canBeDragged;


        public CueBall(string name, Transform transform, Texture texture) : base(name, transform, texture)
        {
            isDragged = false;
            canBeDragged = true;
        }

        private void StartDrag()
        {
            if (canBeDragged)
                isDragged = true;
        }

        private void UpdateDrag()
        {
            Vector2i mousePos = Input.GetMousePos();
            Draw.Line(mousePos, (Vector2i)Center, Color.White);

            if ((Vector2)mousePos == Center) return;

            Vector2 dragVector = (Center - (Vector2)mousePos).Normalize();
            int k = 0;
            bool hasCollided = false;
            Vector2 otherBall = new Vector2(-1);
            float radius = Transform.ScaledSize.x / 2;
            Vector2 collisionPos = new Vector2();

            while (!hasCollided) 
            {
                Vector2 pos = Center + dragVector * k;

                for (int i = 1; i < GameScene.gameObjects.Count; i++)
                {
                    GameObject go = GameScene.gameObjects[i];
                    if (Physics.CircleCollision(pos, go.Center, radius))
                    {
                        hasCollided = true;
                        collisionPos = pos;
                        otherBall = go.Center;
                        break;
                    }
                }
                if (hasCollided) break;

                foreach (var wall in GameScene.lines)
                {
                    if (Physics.CheckLineCircle(wall.pos1, wall.pos2, pos - new Vector2(radius), radius))
                    {
                        hasCollided = true;
                        collisionPos = pos;
                        break;
                    }
                }
                if (hasCollided) break;

                foreach (var hole in GameScene.holes)
                {
                    if (Physics.CircleCollision(pos, hole + new Vector2(26), 13 + radius / 2))
                    {
                        hasCollided = true;
                        collisionPos = pos;
                        break;
                    }
                }
                
                k++;
                
                if (k == 10000)
                {
                    break;
                    //throw new Exception("how??ㅠㅠ");
                }
            }

            Draw.predictionCircle = collisionPos;
            Draw.Line((Vector2i)Center, (Vector2i)collisionPos, Color.White);

            if (otherBall != new Vector2(-1))
            {
                Vector2 vector = otherBall - collisionPos;

                Draw.Line((Vector2i)otherBall, (Vector2i)(otherBall + vector), Color.White);
            }
        }
        
        private void StopDrag()
        {
            isDragged = false;
            canBeDragged = false;
            Draw.predictionCircle = new Vector2(-100);

            var deltaPos = (Vector2)Input.GetMousePos() - Center;
            Transform.AddVelocity(deltaPos * 4);
        }

        public new void Update()
        {
            if (Input.GetMouseState(0) == Input.KeyState.downFrame0)
            {
                var hit = GameScene.Raycast((Vector2)Input.GetMousePos());
                if (hit != null)
                {
                    if (hit.Name == "CueBall")
                    {
                        StartDrag();
                    }
                }
            }
            else if (Input.GetMouseState(0) == Input.KeyState.down)
            {
                if (isDragged)
                    UpdateDrag();
            }
            else if (Input.GetMouseState(0) == Input.KeyState.upFrame0)
            {
                if (isDragged)
                    StopDrag();
            }
            else if (Input.GetMouseState(0) == Input.KeyState.up)
            {
                if (!canBeDragged)
                {
                    if (Transform.Velocity == Vector2.zero)
                    {
                        canBeDragged = true;
                    }
                }
            }

            if (Input.GetMouseState(1) == Input.KeyState.downFrame0)
            {
                Transform.Position = (Vector2)Input.GetMousePos() - new Vector2(Transform.ScaledSize.x / 2);
                Transform.LastPosition = Transform.Position;
                Transform.Velocity = Vector2.zero;
                Draw.predictionCircle = new Vector2(-100);
            }
        }
    }
}
