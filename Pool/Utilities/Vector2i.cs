using SFML.System;

namespace Utility
{
    public class Vector2i
    {
        public static readonly Vector2i zero = new Vector2i(0, 0);
        public static readonly Vector2i one = new Vector2i(1, 1);
        public int x { get; set; }
        public int y { get; set; }

        public Vector2i()
        {
            x = 0;
            y = 0;
        }

        public Vector2i(int val)
        {
            x = val;
            y = val;
        }

        public Vector2i(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2i(Vector2f vector)
        {
            x = (int)vector.X;
            y = (int)vector.Y;
        }

        public Vector2i(SFML.System.Vector2i vector)
        {
            x = vector.X;
            y = vector.Y;
        }
        public Vector2i(Vector2u vector)
        {
            x = (int)vector.X;
            y = (int)vector.Y;
        }

        public static Vector2i operator +(Vector2i left) => left;

        public static Vector2i operator -(Vector2i left) => new Vector2i(-left.x, -left.y);

        public static Vector2i operator +(Vector2i left, Vector2i right)
        {
            return new Vector2i(left.x + right.x, left.y + right.y);
        }

        public static Vector2i operator -(Vector2i left, Vector2i right)
        {
            return new Vector2i(left.x - right.x, left.y - right.y);
        }

        public static Vector2i operator *(Vector2i left, Vector2i right)
        {
            return new Vector2i(left.x * right.x, left.y * right.y);
        }

        public static Vector2i operator *(Vector2i left, int right)
        {
            return new Vector2i(left.x * right, left.y * right);
        }

        public static Vector2i operator /(Vector2i left, Vector2i right)
        {
            if (right.x == 0 || right.y == 0)
            {
                throw new DivideByZeroException();
            }
            return new Vector2i(left.x / right.x, left.y / right.y);
        }

        public static Vector2i operator /(Vector2i left, int right)
        {
            return new Vector2i(left.x / right, left.y / right);
        }

        public static bool operator <=(Vector2i left, Vector2i right)
        {
            if (left.x <= right.x)
            {
                return left.y <= right.y;
            }

            return false;
        }
        public static bool operator >=(Vector2i left, Vector2i right)
        {
            if (left.x >= right.x)
            {
                return left.y >= right.y;
            }

            return false;
        }

        public static bool operator <(Vector2i left, Vector2i right)
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
        public static bool operator >(Vector2i left, Vector2i right)
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

        public static bool operator ==(Vector2i left, Vector2i right)
        {
            if (left.x == right.x)
            {
                return (left.y == right.y);
            }
            return false;
        }

        public static bool operator !=(Vector2i left, Vector2i right)
        {
            return (left.x != right.x || left.x != right.y);
        }

        public static explicit operator SFML.System.Vector2i(Vector2i obj)
        {
            return new SFML.System.Vector2i(obj.x, obj.y);
        }

        public static explicit operator Vector2f(Vector2i obj)
        {
            return new Vector2f(obj.x, obj.y);
        }

        public static explicit operator Vector2i(Vector2 obj)
        {
            return new Vector2i((int)obj.x, (int)obj.y);
        }

        public override string ToString() => $"X: {x}, Y: {y}";
    }
}
