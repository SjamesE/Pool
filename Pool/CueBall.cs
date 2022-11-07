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

                object? obj = Physics.CheckAllColistions(pos, ignore: GameScene.gameObjects[0]);
                if (obj != null)
                {
                    var a = obj.GetType();
                    if (obj.GetType() == typeof(GameObject))
                    {
                        GameObject go = (GameObject)obj;
                        otherBall = go.Center;
                    }
                    hasCollided = true;
                    collisionPos = pos;
                }

                k++;
                
                if (k == 10000)
                {
                    //break;
                    throw new Exception("how??ㅠㅠ");
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
                //Physics.CircleCollision((Vector2)Input.GetMousePos() - new Vector2(Transform.ScaledSize.x / 2))
                Transform.Position = (Vector2)Input.GetMousePos() - new Vector2(Transform.ScaledSize.x / 2);
                Transform.LastPosition = Transform.Position;
                Transform.Velocity = Vector2.zero;
                Draw.predictionCircle = new Vector2(-100);
                Active = true;
            }
        }
    }
}
