using Utility;

namespace Pool
{
    public class Transform
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Scale { get; set; }
        public Vector2 ScaledSize;
        public float Rotation { get; set; }
        public Vector2 Velocity { get; set; } = Vector2.zero;
        public Vector2 LastPosition { get; set; }

        private float frictionCOF = 0.94f;

        public Transform(Vector2 position, Vector2 size, Vector2 scale, float rotation = 0)
        {
            Position = position;
            Scale = scale;
            Size = size;
            Rotation = rotation;
            ScaledSize = Size * Scale;
        }

        public Transform(Vector2 position, Vector2 size)
        {
            Position = position;
            Scale = Vector2.one;
            Size = size;
            Rotation = 0;
            ScaledSize = Size * Scale;
        }
        public Transform()
        {
            Position = Vector2.zero;
            Scale = Vector2.one;
            Size = Vector2.one;
            Rotation = 0;
            ScaledSize = Size * Scale;
        }

        public void Update()
        {
            LastPosition = Position;
            Position += Velocity * App.PHYSICS_UPDATE;

            Velocity -= Velocity * frictionCOF * App.PHYSICS_UPDATE;

            if (Velocity != Vector2.zero)
            {
                if (Math.Abs(Velocity.x) < 1f) Velocity.x = 0;
                if (Math.Abs(Velocity.y) < 1f) Velocity.y = 0;
            }

            if (Input.GetKeyState(SFML.Window.Keyboard.Key.Space) == Input.KeyState.downFrame0)
            {
                Velocity = Vector2.zero;
            }
        }
        
        public void AddVelocity(Vector2 velocity)
        {
            Velocity += -velocity;
        }
    }
}
