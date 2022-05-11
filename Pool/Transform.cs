using Pool.Graphics;
using Utility;

namespace Pool
{
    internal class Transform
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Scale { get; set; }
        public float Rotation { get; set; }
        public Vector2 Velocity { get;  set; } = Vector2.zero;

        private float frictionCOF = 0.985f;

        public Transform(Vector2 position, Vector2 size, Vector2 scale, float rotation = 0)
        {
            Position = position;
            Scale = scale;
            Size = size;
            Rotation = rotation;
        }

        public Transform(Vector2 position, Vector2 size)
        {
            Position = position;
            Scale = Vector2.one;
            Size = size;
            Rotation = 0;
        }
        public Transform()
        {
            Position = Vector2.zero;
            Scale = Vector2.one;
            Size = Vector2.one;
            Rotation = 0;
        }

        public void Update()
        {
            Position += Velocity * App.FRAME_TIME;
            //Check Colision to the right
            if (Position.x + Size.x * Scale.x > Window.WINDOW_WIDTH)
            {
                Position.x = Window.WINDOW_WIDTH - Size.x * Scale.x;
                Velocity.x = -Velocity.x;
            }
            //Check Colision to the left
            if (Position.x < 0)
            {
                Position.x = 0;
                Velocity.x = -Velocity.x;
            }
            //Check Colision Up
            if (Position.y < 0)
            {
                Position.y = 0;
                Velocity.y = -Velocity.y;
            }
            //Check Colision Down
            if (Position.y + Size.y * Scale.y > Window.WINDOW_HEIGHT)
            {
                Position.y = Window.WINDOW_HEIGHT - Size.y * Scale.y;
                Velocity.y = -Velocity.y;
            }
            Velocity *= frictionCOF;

            if (Velocity != Vector2.zero)
            {
                if (Math.Abs(Velocity.x) < 1f) Velocity.x = 0;
                if (Math.Abs(Velocity.y) < 1f) Velocity.y = 0;

                Draw.Line((Vector2i)(Position + Size * Scale / 2), (Vector2i)(Position + Size * Scale / 2 + Velocity), SFML.Graphics.Color.Red);
            }
        }
        
        public void AddVelocity(Vector2 velocity)
        {
            Velocity += -velocity;
        }
    }
}
