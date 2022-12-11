using GameEngine.Maths.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public static class Util
    {
        public static Vector2I Normalized(this Vector2I v)
        {
            float lenght = v.Lenght();
            return new Vector2I((int)((float)v.X / lenght), (int)((float)v.Y / lenght));
        }

        public static float Lenght(this Vector2I v)
            => (float)Math.Sqrt((float)v.X * (float)v.X + (float)v.Y * (float)v.Y);

        public static float Lenght(this Vector2F v)
            => (float)Math.Sqrt(v.X * v.X + v.Y * v.Y);

        public static float Distance(this Vector2F a, Vector2F b)
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        public static Vector2F Normalized(this Vector2F v)
            => v / Lenght(v);
    }
}
