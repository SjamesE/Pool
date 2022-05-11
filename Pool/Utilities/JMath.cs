using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Pool.Utilities
{
    internal static class JMath
    {
        public static bool IsInRange(Vector2 position, Vector2 position2, int width, int height)
        {
            if (position.x >= position2.x - width && position.x <= position2.x + width)
            {
                return (position.y >= position2.y - height && position.y <= position2.y + height);
            }
            return false;
        }

        public static float Pythagoras(float first, float second)
        {
            return (float)Math.Sqrt(first * first + second * second);
        }
    }
}
