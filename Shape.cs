using GameEngine.Maths.Vectors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Shape
    {
        public static readonly Vector2I[][][] Shapes = new Vector2I[][][] // Shape, Rotation, Block  https://tetris.fandom.com/wiki/SRS
        {
            new Vector2I[][] { // Line
                new Vector2I[] {
                    new Vector2I(0, 1),
                    new Vector2I(1, 1),
                    new Vector2I(2, 1),
                    new Vector2I(3, 1),
                },
                new Vector2I[] {
                    new Vector2I(2, 0),
                    new Vector2I(2, 1),
                    new Vector2I(2, 2),
                    new Vector2I(2, 3),
                },
                new Vector2I[] {
                    new Vector2I(0, 2),
                    new Vector2I(1, 2),
                    new Vector2I(2, 2),
                    new Vector2I(3, 2),
                },
                new Vector2I[] {
                    new Vector2I(1, 0),
                    new Vector2I(1, 1),
                    new Vector2I(1, 2),
                    new Vector2I(1, 3),
                },
            },
            new Vector2I[][] { // Square
                new Vector2I[] {
                    new Vector2I(0, 0),
                    new Vector2I(0, 1),
                    new Vector2I(1, 0),
                    new Vector2I(1, 1),
                },
                new Vector2I[] {
                    new Vector2I(0, 0),
                    new Vector2I(0, 1),
                    new Vector2I(1, 0),
                    new Vector2I(1, 1),
                },
                new Vector2I[] {
                    new Vector2I(0, 0),
                    new Vector2I(0, 1),
                    new Vector2I(1, 0),
                    new Vector2I(1, 1),
                },
                new Vector2I[] {
                    new Vector2I(0, 0),
                    new Vector2I(0, 1),
                    new Vector2I(1, 0),
                    new Vector2I(1, 1),
                },
            },
            new Vector2I[][] { // T
                new Vector2I[] {
                    new Vector2I(0, 1),
                    new Vector2I(1, 1),
                    new Vector2I(2, 1),
                    new Vector2I(1, 0),
                },
                new Vector2I[] {
                    new Vector2I(1, 0),
                    new Vector2I(1, 1),
                    new Vector2I(1, 2),
                    new Vector2I(2, 1),
                },
                new Vector2I[] {
                    new Vector2I(0, 1),
                    new Vector2I(1, 1),
                    new Vector2I(2, 1),
                    new Vector2I(1, 2),
                },
                new Vector2I[] {
                    new Vector2I(1, 0),
                    new Vector2I(1, 1),
                    new Vector2I(1, 2),
                    new Vector2I(0, 1),
                },
            },
            new Vector2I[][] { // backwards Z
                new Vector2I[] {
                    new Vector2I(0, 1),
                    new Vector2I(1, 0),
                    new Vector2I(1, 1),
                    new Vector2I(2, 0),
                },
                new Vector2I[] {
                    new Vector2I(1, 0),
                    new Vector2I(1, 1),
                    new Vector2I(2, 1),
                    new Vector2I(2, 2),
                },
                new Vector2I[] {
                    new Vector2I(0, 2),
                    new Vector2I(1, 1),
                    new Vector2I(1, 2),
                    new Vector2I(2, 1),
                },
                new Vector2I[] {
                    new Vector2I(0, 0),
                    new Vector2I(0, 1),
                    new Vector2I(1, 1),
                    new Vector2I(1, 2),
                },
            },
            new Vector2I[][] { // Z
                new Vector2I[] {
                    new Vector2I(0, 0),
                    new Vector2I(1, 0),
                    new Vector2I(1, 1),
                    new Vector2I(2, 1),
                },
                new Vector2I[] {
                    new Vector2I(2, 0),
                    new Vector2I(1, 1),
                    new Vector2I(2, 1),
                    new Vector2I(1, 2),
                },
                new Vector2I[] {
                    new Vector2I(0, 1),
                    new Vector2I(1, 1),
                    new Vector2I(1, 2),
                    new Vector2I(2, 2),
                },
                new Vector2I[] {
                    new Vector2I(1, 0),
                    new Vector2I(0, 1),
                    new Vector2I(1, 1),
                    new Vector2I(0, 2),
                },
            },
            new Vector2I[][] { // backwards L blue
                new Vector2I[] {
                    new Vector2I(0, 0),
                    new Vector2I(0, 1),
                    new Vector2I(1, 1),
                    new Vector2I(2, 1),
                },
                new Vector2I[] {
                    new Vector2I(2, 0),
                    new Vector2I(1, 0),
                    new Vector2I(1, 1),
                    new Vector2I(1, 2),
                },
                new Vector2I[] {
                    new Vector2I(0, 1),
                    new Vector2I(1, 1),
                    new Vector2I(2, 1),
                    new Vector2I(2, 2),
                },
                new Vector2I[] {
                    new Vector2I(1, 0),
                    new Vector2I(1, 1),
                    new Vector2I(1, 2),
                    new Vector2I(0, 2),
                },
            },
            new Vector2I[][] { // L orange
                new Vector2I[] {
                    new Vector2I(0, 1),
                    new Vector2I(1, 1),
                    new Vector2I(2, 1),
                    new Vector2I(2, 0),
                },
                new Vector2I[] {
                    new Vector2I(1, 0),
                    new Vector2I(1, 1),
                    new Vector2I(1, 2),
                    new Vector2I(2, 2),
                },
                new Vector2I[] {
                    new Vector2I(0, 2),
                    new Vector2I(0, 1),
                    new Vector2I(1, 1),
                    new Vector2I(2, 1),
                },
                new Vector2I[] {
                    new Vector2I(0, 0),
                    new Vector2I(1, 0),
                    new Vector2I(1, 1),
                    new Vector2I(1, 2),
                },
            },
        };
        private static readonly Vector2I[][][] RotTestForJLTSZ = new Vector2I[][][]
        {
            new Vector2I[][] // from 0
            {
                new Vector2I[] // to 1
                {
                    new Vector2I(0, 0),
                    new Vector2I(-1, 0),
                    new Vector2I(-1, 1),
                    new Vector2I(0, -2),
                    new Vector2I(-1, 2),
                },
                new Vector2I[] // to 3
                {
                    new Vector2I(0, 0),
                    new Vector2I(1, 0),
                    new Vector2I(1, 1),
                    new Vector2I(0, -2),
                    new Vector2I(1, -2),
                },
            },
            new Vector2I[][] // from 1
            {
                new Vector2I[] // to 2
                {
                    new Vector2I(0, 0),
                    new Vector2I(1, 0),
                    new Vector2I(1, -1),
                    new Vector2I(0, 2),
                    new Vector2I(1, 2),
                },
                new Vector2I[] // to 0
                {
                    new Vector2I(0, 0),
                    new Vector2I(1, 0),
                    new Vector2I(1, -1),
                    new Vector2I(0, 2),
                    new Vector2I(1, 2),
                },
            },
            new Vector2I[][] // from 2
            {
                new Vector2I[] // to 3
                {
                    new Vector2I(0, 0),
                    new Vector2I(1, 0),
                    new Vector2I(1, 1),
                    new Vector2I(0, -2),
                    new Vector2I(1, -2),
                },
                new Vector2I[] // to 1
                {
                    new Vector2I(0, 0),
                    new Vector2I(-1, 0),
                    new Vector2I(-1, 1),
                    new Vector2I(0, -2),
                    new Vector2I(-1, -2),
                },
            },
            new Vector2I[][] // from 3
            {
                new Vector2I[] // to 0
                {
                    new Vector2I(0, 0),
                    new Vector2I(-1, 0),
                    new Vector2I(-1, -1),
                    new Vector2I(0, 2),
                    new Vector2I(-1, 2),
                },
                new Vector2I[] // to 2
                {
                    new Vector2I(0, 0),
                    new Vector2I(-1, 0),
                    new Vector2I(-1, -1),
                    new Vector2I(0, 2),
                    new Vector2I(-1, 2),
                },
            },
        };
        private static readonly Vector2I[][][] RotTestForI = new Vector2I[][][]
        {
            new Vector2I[][] // from 0
            {
                new Vector2I[] // to 1
                {
                    new Vector2I(0, 0),
                    new Vector2I(-2, 0),
                    new Vector2I(1, 0),
                    new Vector2I(-2, -1),
                    new Vector2I(1, 2),
                },
                new Vector2I[] // to 3
                {
                    new Vector2I(0, 0),
                    new Vector2I(-1, 0),
                    new Vector2I(2, 0),
                    new Vector2I(-1, 2),
                    new Vector2I(2, -1),
                },
            },
            new Vector2I[][] // from 1
            {
                new Vector2I[] // to 2
                {
                    new Vector2I(0, 0),
                    new Vector2I(-1, 0),
                    new Vector2I(2, 0),
                    new Vector2I(-1, 2),
                    new Vector2I(2, -1),
                },
                new Vector2I[] // to 0
                {
                    new Vector2I(0, 0),
                    new Vector2I(2, 0),
                    new Vector2I(-1, 0),
                    new Vector2I(2, 1),
                    new Vector2I(-1, -2),
                },
            },
            new Vector2I[][] // from 2
            {
                new Vector2I[] // to 3
                {
                    new Vector2I(0, 0),
                    new Vector2I(2, 0),
                    new Vector2I(-1, 0),
                    new Vector2I(2, 1),
                    new Vector2I(-1, -2),
                },
                new Vector2I[] // to 1
                {
                    new Vector2I(0, 0),
                    new Vector2I(1, 0),
                    new Vector2I(-2, 0),
                    new Vector2I(1, -2),
                    new Vector2I(-2, 1),
                },
            },
            new Vector2I[][] // from 3
            {
                new Vector2I[] // to 0
                {
                    new Vector2I(0, 0),
                    new Vector2I(1, 0),
                    new Vector2I(-2, 0),
                    new Vector2I(1, -2),
                    new Vector2I(-2, 1),
                },
                new Vector2I[] // to 2
                {
                    new Vector2I(0, 0),
                    new Vector2I(-2, 0),
                    new Vector2I(1, 0),
                    new Vector2I(-2, -1),
                    new Vector2I(1, 2),
                },
            },
        };

        public Block[] blocks;

        private ShapeInfo info;

        private Vector2I mostLeftPos;
        private Vector2I mostRightPos;
        private Vector2I mostTopPos;
        private Vector2I mostBottomPos;

        public Shape(Block[] _blocks)
        {
            blocks = _blocks;
            info = new ShapeInfo(ShapeType.Custom, Block.DefaultColor, -Vector2I.One, 0);
            UpdatePoses();
        }

        /// <summary>
        /// Creates Shape and Assings _color to _blocks
        /// </summary>
        /// <param name="_blocks"></param>
        /// <param name="_color"></param>
        public Shape(Block[] _blocks, Color color, ShapeInfo _info)
        {
            blocks = _blocks;
            info = _info;
            for (int i = 0; i < blocks.Length; i++)
                blocks[i].Set(color);
            UpdatePoses();
        }

        private void UpdatePoses()
        {
            mostLeftPos = new Vector2I(int.MaxValue, -1);
            mostRightPos = new Vector2I(int.MinValue, -1);
            mostTopPos = new Vector2I(-1, int.MinValue);
            mostBottomPos = new Vector2I(-1, int.MaxValue);
            for (int i = 0; i < blocks.Length; i++) {
                if (blocks[i].X < mostLeftPos.X)
                    mostLeftPos = blocks[i].Pos;
                if (blocks[i].X > mostRightPos.X)
                    mostRightPos = blocks[i].Pos;
                if (blocks[i].Y < mostBottomPos.Y)
                    mostBottomPos = blocks[i].Pos;
                if (blocks[i].Y > mostTopPos.Y)
                    mostTopPos = blocks[i].Pos;
            }
        }

        public void AddBlock(Block b)
        {
            if (!blocks.Contains(b)) {
                Block[] bs = new Block[blocks.Length + 1];
                Array.Copy(blocks, bs, blocks.Length);
                bs[bs.Length - 1] = b;
                blocks = bs;
            }
        }

        /// <summary>
        /// Moves the shape
        /// </summary>
        /// <param name="move">Move "directio"</param>
        /// <returns>Info about collision, if it occured</returns>
        public CollisionInfo Move(Vector2I move)
        {
            if (mostLeftPos.X + move.X < 0)
                return CollisionInfo.Create(mostLeftPos + move);
            else if (mostRightPos.X + move.X >= Game.fieldWidth)
                return CollisionInfo.Create(mostRightPos + move);

            for (int i = 0; i < blocks.Length; i++) {
                Vector2I p = blocks[i].Pos + move;
                if (!IsShapesBlock(p) && (!Game.IsBlockInField(p) || Game.GetBlock(p).Ocupied))
                    return CollisionInfo.Create(p);
            }

            if (info.sType != ShapeType.Custom)
                info.Pos += move;

            Array.Sort(blocks, GetComparison(move.Normalized()));

            for (int i = 0; i < blocks.Length; i++) {
                Block b = Game.GetBlock(blocks[i].Pos + move);
                b.Set(blocks[i].Empty());
                blocks[i] = b;
            }

            UpdatePoses();
            return CollisionInfo.CreateNull();
        }

        public void Rotate(int _rot)
        {
            if (info.sType == ShapeType.Custom)
                return;

            int rot = info.Rotation + _rot;
            if (rot >= 4)
                rot = 0;
            else if (rot < 0)
                rot = 3;

            Console.WriteLine($"Rot From: {info.Rotation}, To: {rot}");

            Vector2I[] newBlocks = Shapes[(int)info.sType][rot];
            Vector2I offset = -Vector2I.One;

            int from = info.Rotation;
            int to = rot;
            Vector2I[] tests;
            if (info.sType == ShapeType.J || info.sType == ShapeType.L || info.sType == ShapeType.T || info.sType == ShapeType.S || info.sType == ShapeType.Z)
                tests = RotTestForJLTSZ[from][to > from ? 0 : 1];
            else if (info.sType == ShapeType.I)
                tests = RotTestForI[from][to > from ? 0 : 1];
            else
                tests = new Vector2I[] { new Vector2I(0, 0) };
            for (int i = 0; i < tests.Length; i++) {
                Vector2I _off = tests[i];
                _off.Y = -_off.Y;

                if (RotationPossible(from, to, i, newBlocks, _off)) {
                    offset = _off;
                    break;
                }
            }

            if (offset == -Vector2I.One)
                return;

            info.Rotation = rot;

            for (int i = 0; i < blocks.Length; i++)
                blocks[i].Empty();

            for (int i = 0; i < newBlocks.Length; i++) {
                blocks[i] = Game.GetBlock(newBlocks[i] + info.Pos + offset);
                blocks[i].Set(info.Color);
            }
        }

        private bool RotationPossible(int from, int to, int test, Vector2I[] _blocks, Vector2I offset)
        {
            for (int i = 0; i < _blocks.Length; i++) {
                Vector2I pos = _blocks[i] + info.Pos + offset;
                if (!Game.IsBlockInField(pos) || (Game.GetBlock(pos).Ocupied && Game.GetBlocksShape(pos) != this))
                    return false;
            }

            return true;
        }

        private Vector2I GetCenterOffsetForRotation(int rot)
        {
            switch (info.sType) {
                case ShapeType.J:
                case ShapeType.L:
                case ShapeType.T:
                case ShapeType.S:
                case ShapeType.Z: 
                    {
                        if (rot == 1)
                            return -Vector2I.UnitX;
                        else if (rot == 3)
                            return Vector2I.UnitX;

                        return Vector2I.Zero;
                    }
                case ShapeType.I: 
                    {
                        if (rot == 1)
                            return Vector2I.UnitX;
                        else if (rot == 3)
                            return -Vector2I.UnitX;

                        return Vector2I.Zero;
                    }
                default:
                    return Vector2I.Zero;
            }
        }

        public bool IsShapesBlock(Vector2I pos)
        {
            for (int i = 0; i < blocks.Length; i++)
                if (blocks[i].Pos == pos)
                    return true;

            return false;
        }

        public void SetColor(Color _color)
        {
            for (int i = 0; i < blocks.Length; i++)
                blocks[i].Color = _color;
        }

        public static Shape Combine(params Shape[] shapes)
        {
            List<Block> allBlocks = new List<Block>();
            for (int i = 0; i < shapes.Length; i++)
                allBlocks.AddRange(shapes[i].blocks);

            return new Shape(allBlocks.Distinct().ToArray());
        }

        public List<Shape> Split(int y)
        {
            if (y < mostBottomPos.Y || y > mostTopPos.Y)
                return new List<Shape>() { new Shape(blocks) };
            else {
                List<Block> _blocks = new List<Block>();
                for (int i = 0; i < blocks.Length; i++)
                    if (blocks[i].Y != y)
                        _blocks.Add(blocks[i]);
                    else
                        blocks[i].Empty();

                List<Shape> newShapes = new List<Shape>();
                List<Shape> canConnectTo = new List<Shape>();
                for (int i = 0; i < _blocks.Count; i++) {
                    if (newShapes.Count == 0)
                        newShapes.Add(new Shape(new Block[] { _blocks[i] }));
                    else {
                        canConnectTo.Clear();
                        for (int j = 0; j < newShapes.Count; j++) {
                            for (int k = 0; k < Block.SideChecks.Length; k++) {
                                Vector2I pos = _blocks[i].Pos + Block.SideChecks[k];
                                if (Game.IsBlockInField(pos) && newShapes[j].IsShapesBlock(pos) && !canConnectTo.Contains(newShapes[j]))
                                    canConnectTo.Add(newShapes[j]);
                            }
                        }

                        if (canConnectTo.Count == 0)
                            newShapes.Add(new Shape(new Block[] { _blocks[i] }));
                        else {
                            for (int j = 0; j < canConnectTo.Count; j++)
                                for (int k = 0; k < newShapes.Count; k++)
                                    if (newShapes[k] == canConnectTo[j]) {
                                        newShapes.RemoveAt(k);
                                        break;
                                    }
                            canConnectTo.Add(new Shape(new Block[] { _blocks[i] }));
                            newShapes.Add(Combine(canConnectTo.ToArray()));
                        }
                    }
                }

                return newShapes;
            }
        }

        private Comparison<Block> GetComparison(Vector2I move)
        {
            if (move == -Vector2I.UnitY)
                return compUp;
            else if (move == Vector2I.UnitY)
                return compDown;
            else if (move == Vector2I.UnitX)
                return compRight;
            else if (move == -Vector2I.UnitX)
                return compLeft;
            else
                return null;
        }

        private static Comparison<Block> compUp = new Comparison<Block>((Block a, Block b) =>
        {
            if (a.Y > b.Y)
                return 1;
            else if (a.Y < b.Y)
                return -1;
            else
                return 0;
        });
        private static Comparison<Block> compDown = new Comparison<Block>((Block a, Block b) =>
        {
            if (a.Y < b.Y)
                return 1;
            else if (a.Y > b.Y)
                return -1;
            else
                return 0;
        });
        private static Comparison<Block> compLeft = new Comparison<Block>((Block a, Block b) =>
        {
            if (a.X > b.X)
                return 1;
            else if (a.X < b.X)
                return -1;
            else
                return 0;
        }); 
        private static Comparison<Block> compRight = new Comparison<Block>((Block a, Block b) =>
        {
            if (a.X < b.X)
                return 1;
            else if (a.X > b.X)
                return -1;
            else
                return 0;
        });
    }
}
