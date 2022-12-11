using GameEngine.Maths.Vectors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public struct ShapeInfo
    {
        public ShapeType sType;
        public Color Color; 
        public Vector2I Pos;
        public int Rotation;

        public ShapeInfo(ShapeType _sType, Color _color, Vector2I _pos, int _rot)
        {
            sType = _sType;
            Color = _color;
            Pos = _pos;
            Rotation = _rot;
        }
    }
}
