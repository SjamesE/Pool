using Pool.Graphics;
using Pool.Scenes;
using SFML.Graphics;
using Utility;

namespace Pool
{
    internal class CueBall : GameObject
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
            Draw.Line(Input.GetMousePos(), (Vector2i)Center, Color.White);
        }

        private void StopDrag()
        {
            isDragged = false;
            canBeDragged = false;

            var deltaPos = (Vector2)Input.GetMousePos() - Center;
            Transform.AddVelocity(deltaPos * 5);
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
        }
    }
}
