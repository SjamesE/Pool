using Pool.Graphics;
using Pool.Scenes;
using Pool.Utilities;
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

            // Draw line from mouse to cue ball
            Draw.Line(mousePos, (Vector2i)Center, Color.White);

            if ((Vector2)mousePos == Center) return;
            Vector2 dragVector = (Center - (Vector2)mousePos).Normalize();

            // Shoot raycast
            var rayHit = Physics.RayMarch(Center, dragVector);

            if (rayHit.hasHit)
            {
                // Set prediction circle position
                Draw.predictionCircle = rayHit.point!;

                Vector2i trajectoryLineOrigin = (Vector2i)rayHit.origin;
                if (rayHit.hasHit && rayHit.distance > 30)
                    trajectoryLineOrigin += (Vector2i)(rayHit.rayDir * 25f);

                // If ray hit another ball
                if (rayHit.other != null)
                {
                    // Get the dot product between the hit normal and the ray dir
                    // to scale the prediction line based on the hit angle. (Result between 0 and 1)
                    float predictionScale = -rayHit.hitNormal.Dot((rayHit.origin - rayHit.point).Normalize());
                    // scale and clamp
                    predictionScale = Math.Clamp(predictionScale * 50f, 10f, 50f);

                    // Draw other ball direction prediction line
                    Draw.Line((Vector2i) rayHit.other.Center,
                              (Vector2i)(rayHit.other.Center + rayHit.hitNormal * predictionScale),
                              Color.White);


                    // Draw line from the center of the cue ball to the collision position
                    Draw.Line((Vector2i)rayHit.point,
                              trajectoryLineOrigin,
                              Color.White);
                }
                else // If ray hit a line or a hole
                {
                    // Draw line from the center of the cue ball to the collision position
                    Draw.Line((Vector2i)rayHit.point,
                              trajectoryLineOrigin,
                              Color.White);
                }
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
            // Check input
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
                Active = true;
            }
        }
    }
}
