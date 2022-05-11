using SFML.System;

namespace Utility
{
    public class Vector2
    {
        public static readonly Vector2 zero = new Vector2(0, 0);
        public static readonly Vector2 one = new Vector2(1, 1);
        public float x { get; set; }
        public float y { get; set; }

        public Vector2()
        {
            x = 0;
            y = 0;
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(Vector2f vector)
        {
            x = vector.X;
            y = vector.Y;
        }

        public Vector2(SFML.System.Vector2i vector)
        {
            x = vector.X;
            y = vector.Y;
        }
        public Vector2(Vector2u vector)
        {
            x = vector.X;
            y = vector.Y;
        }

        public static Vector2 operator +(Vector2 left) => left;

        public static Vector2 operator -(Vector2 left) => new Vector2(-left.x, -left.y);

        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x + right.x, left.y + right.y);
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x - right.x, left.y - right.y);
        }

        public static Vector2 operator *(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x * right.x, left.y * right.y);
        }

        public static Vector2 operator *(Vector2 left, float right)
        {
            return new Vector2(left.x * right, left.y * right);
        }

        public static Vector2 operator /(Vector2 left, Vector2 right)
        {
            if (right.x == 0 || right.y == 0)
            {
                throw new System.DivideByZeroException();
            }
            return new Vector2(left.x / right.x, left.y / right.y);
        }

        public static Vector2 operator /(Vector2 left, float right)
        {
            return new Vector2(left.x / right, left.y / right);
        }

        public static bool operator <=(Vector2 left, Vector2 right)
        {
            if (left.x <= right.x)
            {
                return left.y <= right.y;
            }

            return false;
        }
        public static bool operator >=(Vector2 left, Vector2 right)
        {
            if (left.x >= right.x)
            {
                return left.y >= right.y;
            }

            return false;
        }

        public static bool operator <(Vector2 left, Vector2 right)
        {
            if (left.x < right.x)
            {
                return left.y <= right.y;
            }
            else if (left.y > right.y)
            {
                return left.x <= right.x;
            }
            return false;
        }
        public static bool operator >(Vector2 left, Vector2 right)
        {
            if (left.x > right.x)
            {
                return left.y >= right.y;
            }
            else if (left.y > right.y)
            {
                return left.x >= right.x;
            }
            return false;
        }

        public static bool operator ==(Vector2 left, Vector2 right)
        {
            if (left.x == right.x)
            {
                return (left.y == right.y);
            }
            return false;
        }

        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return (left.x != right.x || left.y != right.y);
        }

        public static explicit operator Vector2f(Vector2 obj)
        {
            return new Vector2f(obj.x, obj.y);
        }

        public static explicit operator SFML.System.Vector2i(Vector2 obj)
        {
            return new SFML.System.Vector2i((int)obj.x, (int)obj.y);
        }

        public static explicit operator Vector2(Vector2i obj)
        {
            return new Vector2(obj.x, obj.y);
        }

        public override string ToString() => $"X: {x}, Y: {y}";
    }
}
