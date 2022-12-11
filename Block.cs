using GameEngine.Maths.Vectors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Block
    {
        public static readonly Color DefaultColor = Color.Gray;

        public static readonly Color[] ValidColors = new Color[]
        {
            Color.Cyan,
            Color.Yellow,
            Color.Purple,
            Color.Lime,
            Color.Red,
            Color.Blue,
            Color.Orange,
        };

        public static readonly Vector2I[] SideChecks = new Vector2I[]
        {
            Vector2I.UnitY,
            -Vector2I.UnitY,
            -Vector2I.UnitX,
            Vector2I.UnitX,
        };

        public Color Color;
        public Vector2I Pos;
        public int X { get => Pos.X; }
        public int Y { get => Pos.Y; }
        public bool Ocupied { get => Color != DefaultColor; }

        public Block(Color _color, Vector2I _pos)
        {
            Color = _color;
            Pos = _pos;
        }

        public Color Empty()
        {
            Color clr = Color;
            Color = DefaultColor;
            return clr;
        }

        public void Set(Color _color) => Color = _color;

        public static bool operator ==(Block a, Block b)
            => a.Pos == b.Pos;
        public static bool operator !=(Block a, Block b)
            => a.Pos != b.Pos;

        public override bool Equals(object obj)
        {
            if (obj is Block b)
                return this == b;
            else
                return false;
        }

        public override int GetHashCode()
            => X ^ Y;
    }
}
