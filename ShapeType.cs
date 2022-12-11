using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public enum ShapeType : int
    {
        I = 0, // Line
        O = 1, // Square
        T = 2,
        S = 3, // backwards Z
        Z = 4,
        J = 5, // backwards L
        L = 6,
        Custom = 7, // used by placed blocks
    }
}
