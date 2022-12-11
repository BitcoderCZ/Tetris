using GameEngine.Maths.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public struct CollisionInfo
    {
        public Vector2I Pos;
        public bool Collided;

        public static CollisionInfo CreateNull() => new CollisionInfo() { Pos = -Vector2I.One, Collided = false };
        public static CollisionInfo Create(Vector2I _pos) => new CollisionInfo() { Pos = _pos, Collided = true };
    }
}
