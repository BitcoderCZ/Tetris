using GameEngine;
using GameEngine.Font;
using GameEngine.Inputs;
using GameEngine.Maths.Vectors;
using GameEngine.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

using Font = GameEngine.Font.Font;
using MouseButtons = GameEngine.Inputs.MouseButtons;

namespace Tetris
{
    public class Game : Engine
    {
        // Const
        public const int fieldWidth = 10;
        public const int fieldHeight = 20;
        public const int blockSize = 20;
        public const int spaceBetweenBlocks = 1;
        public const int fieldPixWidth = fieldWidth * (blockSize + spaceBetweenBlocks);
        public const int fieldPixHeight = fieldHeight * (blockSize + spaceBetweenBlocks);

        private const float moveDownMaxTime = 0.5f;
        private const float moveDownMaxTimeFast = 0.1f;

        private static readonly Vector2I blockSizeV = new Vector2I(blockSize, blockSize);

        private static Block[] blocks;
        private static float moveDownTimer = 0f;
        private static ulong score;

        private static List<Shape> placedShapes = new List<Shape>();
        private static int gameState;

        private static UIManager ui;
        public static Font font_pixel;
        public static Font font_roboto_medium;
        public static Font font_roboto_thin;

        private static Shape currentShape;
        private static RandomBundle<int> bundle;

        protected override void Initialize()
        {
            blocks = new Block[fieldWidth * fieldHeight];
            for (int i = 0; i < blocks.Length; i++)
                blocks[i] = new Block(Block.DefaultColor, new Vector2I(i % fieldWidth, i / fieldWidth));

            int[] array = new int[Shape.Shapes.Length];
            for (int i = 0; i < array.Length; i++)
                array[i] = i;

            bundle = new RandomBundle<int>(array, (int i) => i, Environment.TickCount, true);

            #region WindowSetUp
            GameWindow.WindowStyle = WindowStyle.SingleBorderWindow;
            GameWindow.ResizeMode = ResizeMode.CanResizeWithGrip;
            GameWindow.ReInit();

            GameWindow.position = new Vector2I(
                    Screen.PrimaryScreen.Bounds.Width / 2 - GameWindow.Width / 2,
                    Screen.PrimaryScreen.Bounds.Height / 2 - GameWindow.Height / 2
                );

            GameWindow.Input.KeyDown += OnKeyDown;
            GameWindow.Input.SizeChanged += (object sender, ISizeEventArgs args) =>
            {
                Console.WriteLine($"Res {GameWindow.Width} {GameWindow.Height} {args.NewSize}");
                GameWindow.ReInit(args.NewSize.Width, args.NewSize.Height);
                UpdateGameState();
            };
            #endregion

            font_pixel = FontLibrary.GetOrCreateFromFile(Environment.CurrentDirectory + "/Data/Silkscreen.ttf");
            font_roboto_medium = FontLibrary.GetOrCreateFromFile(Environment.CurrentDirectory + "/Data/Roboto-Medium.ttf");
            font_roboto_thin = FontLibrary.GetOrCreateFromFile(Environment.CurrentDirectory + "/Data/Roboto-Thin.ttf");
            ui = new UIManager(this, font_pixel, font_roboto_medium, font_roboto_thin);

            gameState = 0;
            UpdateGameState();
        }

        private void UpdateGameState()
        {
            ui.ClearCaschedText();
            ui.ClearUI();

            int playBtnWidth = 300;
            int playBtnHeight = 80;


            int restartBtnWidth = 500;
            int restartBtnHeight = 80;

            switch (gameState) {
                case 0:
                    ui.AddElement(new UIButton("Play", (UIManager _ui, MouseButtons btns) =>
                    {
                        gameState = 1;
                        UpdateGameState();
                    }, GameWindow.Width / 2 - playBtnWidth / 2, GameWindow.Height / 2 - playBtnHeight / 2, playBtnWidth, playBtnHeight, 10, 5, 10, 5, 128));
                    break;
                case 1:
                    for (int i = 0; i < blocks.Length; i++)
                        blocks[i].Empty();
                    placedShapes.Clear();
                    currentShape = null;
                    GenShape();
                    moveDownTimer = 0f;
                    score = 0;
                    break;
                case 2:
                    uint id = ui.AddElement(new UIText($"Score: {score}", 0, 0, Color.Black.ToArgb(), 80));
                    IUIElement text = ui.GetElement(id);
                    text.X = GameWindow.Width / 2 - FontRender.GetTextSize(font_pixel, $"Score: {score}", 80, 2).Width / 2;
                    text.Y = GameWindow.Height / 3 - FontRender.GetTextSize(font_pixel, $"Score: {score}", 80, 2).Height / 2;
                    ui.AddElement(new UIButton("Restart", (UIManager _ui, MouseButtons btns) =>
                    {
                        gameState = 1;
                        UpdateGameState();
                    }, GameWindow.Width / 2 - restartBtnWidth / 2, (GameWindow.Height / 3) * 2 - restartBtnHeight / 2, restartBtnWidth, restartBtnHeight, 10, 5, 10, 5, 128));
                    break;
                default:
                    break;
            }
        }

        protected override void drawInternal()
        {
            // Update
            GameWindow.Input.Update();
            float delta = FpsCounter.DeltaTimeF;

            if (gameState == 1) {
                moveDownTimer += delta;

                bool holdingFastDown = GameWindow.Input.GetKey(Key.Down).pressed;

                if (holdingFastDown && moveDownTimer >= moveDownMaxTimeFast) {
                    moveDownTimer %= moveDownMaxTimeFast;
                    MoveDown();
                }
                else if (!holdingFastDown && moveDownTimer >= moveDownMaxTime) {
                    moveDownTimer %= moveDownMaxTime;
                    MoveDown();
                }
            }

            ui.Update(delta);

            // Render
            Clear(Color.White);

            if (gameState == 1) {
                Vector2I offset = new Vector2I(GameWindow.Width / 2 - fieldPixWidth / 2, GameWindow.Height / 2 - fieldPixHeight / 2);
                for (int x = 0; x < fieldWidth; x++)
                    for (int y = 0; y < fieldHeight; y++) {
                        Vector2I pos = new Vector2I(x * (blockSize + spaceBetweenBlocks), y * (blockSize + spaceBetweenBlocks)) + offset;
                        Fill(pos, pos + blockSizeV, blocks[y * fieldWidth + x].Color);
                    }
            } 

            ui.Draw();

            DrawFPS();
        }

        private void MoveDown(bool genNewShape = true)
        {
            List<(Shape cantMove, Shape blocked)> couldntMove = new List<(Shape cantMove, Shape blocked)>();
            for (int i = 0; i < placedShapes.Count; i++) {
                CollisionInfo cInfo = placedShapes[i].Move(Vector2I.UnitY);
                if (cInfo.Collided && IsBlockInField(cInfo.Pos))
                    couldntMove.Add((placedShapes[i], GetBlocksShape(cInfo.Pos)));
            }

            for (int j = 0; j < 10; j++)
                for (int i = 0; i < couldntMove.Count; i++) {
                    CollisionInfo cInfo = couldntMove[i].cantMove.Move(Vector2I.UnitY);
                    if (!cInfo.Collided || !IsBlockInField(cInfo.Pos)) {
                        couldntMove.RemoveAt(i);
                        i--;
                    }
                }

            CollisionInfo info = currentShape.Move(Vector2I.UnitY);
            if (info.Collided) {
                List<Shape> canConnectTo = new List<Shape>();
                Block[] currentBlocks = currentShape.blocks;
                for (int i = 0; i < currentBlocks.Length; i++)
                    for (int j = 0; j < Block.SideChecks.Length; j++) {
                        Vector2I pos = currentBlocks[i].Pos + Block.SideChecks[j];
                        if (IsBlockInField(pos) && GetBlock(pos).Ocupied) {
                            Shape s = GetBlocksShape(pos);
                            if (s != null && s != currentShape && !canConnectTo.Contains(s))
                                canConnectTo.Add(s);
                        }
                    }

                if (canConnectTo.Count > 0) {
                    for (int i = 0; i < placedShapes.Count; i++)
                        for (int j = 0; j < canConnectTo.Count; j++)
                            if (placedShapes[i] == canConnectTo[j]) {
                                placedShapes.RemoveAt(i);
                                i--;
                                break;
                            }
                    canConnectTo.Add(currentShape);
                    Shape combinedShape = Shape.Combine(canConnectTo.ToArray());
                    Console.WriteLine($"Combined {canConnectTo.Count} shapes");
                    placedShapes.Add(combinedShape);
                }
                else {
                    Console.WriteLine($"Combined 0 shapes");
                    placedShapes.Add(currentShape);
                }

                int cleared = CheckFullLine();

                double multiplyer = genNewShape ? 1.0 : 1.25;
                switch (cleared) {
                    case 1:
                        score += (ulong)(100.0 * multiplyer);
                        break;
                    case 2:
                        score += (ulong)(300.0 * multiplyer);
                        break;
                    case 3:
                        score += (ulong)(500.0 * multiplyer);
                        break;
                    case 4:
                        score += (ulong)(800.0 * multiplyer);
                        break;
                    default:
                        break;
                }

                if (genNewShape)
                    if (GenShape())
                        GameOver();
            }
        }

        private void GameOver()
        {
            gameState = 2;
            UpdateGameState();
        }

        private static int CheckFullLine()
        {
            int cleared = 0;

            for (int y = fieldHeight - 1; y >= 0; y--) {
                bool all = true;
                for (int x = 0; x < fieldWidth; x++)
                    if (!GetBlock(x, y).Ocupied)
                        all = false;

                if (!all)
                    continue;

                cleared++;

                List<Shape> newPlacedShapes = new List<Shape>();
                for (int i = 0; i < placedShapes.Count; i++)
                    newPlacedShapes.AddRange(placedShapes[i].Split(y));

                placedShapes = new List<Shape>(newPlacedShapes);
            }

            return cleared;
        }

        public static Shape GetBlocksShape(Vector2I pos)
        {
            if (currentShape.IsShapesBlock(pos))
                return currentShape;

            for (int i = 0; i < placedShapes.Count; i++)
                if (placedShapes[i].IsShapesBlock(pos))
                    return placedShapes[i];

            return null;
        }

        /// <returns>True if there is a collision</returns>
        private static bool GenShape()
        {
            List<Block> _blocks = new List<Block>();
            int blockIndex = bundle.Next();
            int blockRotation = bundle.rng.Next(4);
            Vector2I[] _shape = Shape.Shapes[blockIndex][blockRotation];
            Color shapeColor = Block.ValidColors[blockIndex];
            Vector2I spawPos = new Vector2I(4, 0);

            for (int i = 0; i < blocks.Length; i++) {
                for (int j = 0; j < _shape.Length; j++)
                    if (blocks[i].Pos == spawPos + _shape[j]) {
                        if (blocks[i].Ocupied)
                            return true;
                        _blocks.Add(blocks[i]);
                        break;
                    }
                if (_blocks.Count == _shape.Length)
                    break;
            }
            currentShape = new Shape(_blocks.ToArray(), shapeColor, new ShapeInfo((ShapeType)blockIndex, shapeColor, spawPos, blockRotation));
            moveDownTimer = 0f;
            return false;
        }

        private void OnKeyDown(object sender, IKeyEventArgs args)
        {
            Key key = args.Key;

            if (gameState == 1)
                switch (key) {
                    case Key.Left:
                        currentShape?.Move(-Vector2I.UnitX);
                        break;
                    case Key.Right:
                        currentShape?.Move(Vector2I.UnitX);
                        break;
                    case Key.Q:
                        currentShape?.Rotate(-1);
                        break;
                    case Key.E:
                        currentShape?.Rotate(1);
                        break;
                    case Key.Space:
                        for (int i = 0; i < fieldHeight; i++)
                            MoveDown(false);

                        GenShape();
                        break;
                    default:
                        break;
                }
        }

        public static Block GetBlock(Vector2I pos)
            => GetBlock(pos.X, pos.Y);
        public static Block GetBlock(int x, int y)
        {
            if (!IsBlockInField(x, y))
                return null;
            return blocks[y * fieldWidth + x];
        }

        public static bool IsBlockInField(Vector2I pos)
            => IsBlockInField(pos.X, pos.Y);
        public static bool IsBlockInField(int x, int y)
            => x >= 0 && y >= 0 && x < fieldWidth && y < fieldHeight;

        private void DrawFPS()
        {
            // Window title
            ui.selectedFontIndex = 1;
            if (gameState == 1)
                ui.RenderAndCaschText($"FPS: {SystemPlus.MathPlus.Round(FpsCounter.FpsGlobal, 1)}, Score: {score}", 30, Color.Black.ToArgb(), 10, 5);
            else
                ui.RenderAndCaschText($"FPS: {SystemPlus.MathPlus.Round(FpsCounter.FpsGlobal, 1)}", 30, Color.Black.ToArgb(), 10, 5);
            ui.selectedFontIndex = 0;
        }

        private void Close()
        {
            ui?.ClearCaschedText();
            base.Exit();
            Environment.Exit(0);
        }
    }
}
