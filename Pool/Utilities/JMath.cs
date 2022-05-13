﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Pool.Utilities
{
    internal static class JMath
    {
        public static readonly float DegToRad = (float)Math.PI / 180f;
        public static readonly float RadToDeg = 180f / (float)Math.PI;

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

        public static float Sin(float value)
        {
            return (float)Math.Round(Math.Sin(value * DegToRad), 8);
        }

        public static float Cos(float value)
        {
            return (float)Math.Round(Math.Cos(value * DegToRad), 8);
        }

        public static float Tan(float value)
        {
            return (float)Math.Round(Math.Tan(value * DegToRad), 8);
        }

        public static float ASin(float value)
        {
            return (float)Math.Round(Math.Asin(value) * RadToDeg, 8);
        }

        public static float ACos(float value)
        {
            return (float)Math.Round(Math.Acos(value) * RadToDeg, 8);
        }

        public static float ATan(float value)
        {
            return (float)Math.Round(Math.Atan(value) * RadToDeg, 8);
        }

        public static float Normalize(float value, int max, int min = 0)
        {
            return value / max + min;
        }
        public static Vector2 Normalize(Vector2 value, int max, int min = 0)
        {
            return new Vector2(value.x / max + min, value.y);
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            return a + (b - a) * t;
        }

        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }
    }
}
