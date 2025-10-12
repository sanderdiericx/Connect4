using Connect4.src.Game;
using Connect4.src.Graphics.Animations;
using Connect4.src.Graphics.Sprites;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using Connect = Connect4.src.Game.Connect4;

namespace Connect4.src.Graphics
{
    internal class GameLoop
    {
        private static Grid _grid;
        private static Indicator _indicator;

        // Game variables
        private static bool _playerTurn;
        private static Stopwatch _markerCooldownTracker;

        private const float MARKER_COOLDOWN = 0.15f;
        private const float MARKER_DROP_SPEED = 0.5f;
        private const int GRID_COLS = 7;
        private const int GRID_ROWS = 6;
        private const int GRID_GAP = 90;
        private const int GRID_PADDING = 80;
        private const int GRID_OFFSET = 10;
        private const int GRID_BORDERSIZE = 6;

        private static GameCheckResult _gameCheckResult;
        private static bool _gameOverHappened;

        internal static void LoadGame()
        {
            // Setup game grid
            GridLayout gridLayout = new GridLayout(GRID_COLS, GRID_ROWS, GRID_GAP, GRID_PADDING, GRID_OFFSET, Color.Black, Color.WhiteSmoke, GRID_BORDERSIZE, false, true);
            _grid = new Grid(gridLayout);

            _indicator = new Indicator(_grid, gridLayout, 50, Color.Black, Color.Firebrick, 12);

            _gameCheckResult = new GameCheckResult(GameState.Playing, Winner.None, new List<Vector2>());
            _playerTurn = true;

            _gameOverHappened = false;

            _markerCooldownTracker = new Stopwatch();
            _markerCooldownTracker.Start();
        }

        internal static void UpdateGame()
        {
            GameState gameState = _gameCheckResult._gameState;

            if (gameState == GameState.GameOver && !_gameOverHappened)
            {
                GameOver();

                _gameOverHappened = true;
            }

            if (gameState == GameState.Playing)
            {
                UpdateConnect4();
                UpdateUI();
            }

            GraphicsEngine.UpdateAnimations();
        }

        internal static void RenderGame()
        {
            GraphicsEngine.ClearFrame();
            GraphicsEngine.ClearRenderBatch();

            GraphicsEngine.AddSpritesToQueue(_grid.GetSprites());
            GraphicsEngine.AddSpriteToQueue(_indicator._triangle);

            GraphicsEngine.DrawRenderBatch();
        }

        private static void GameOver()
        {
            _indicator._triangle._isVisible = false;

            // Unhighlight selected cell
            Vector2 lastHighlight = _grid._lastHighlight;
            _grid._gridCells[(int) lastHighlight.X, (int) lastHighlight.Y]._cellRectangle.SetBorderColor(_grid._gridLayout._borderColor);

            if (_gameCheckResult._winner != Winner.Draw)
            {
                // Create a rainbow animation for all winning markers
                foreach (var winningPosition in _gameCheckResult._winningMarkers)
                {
                    // Create a chainAnimation to endlessly change the markers colors
                    Circle winningSprite = _grid._gridCells[(int)winningPosition.X, (int)winningPosition.Y]._cellMarker;
                    AnimationTarget animationTarget = new AnimationTarget(winningSprite, 1f, x => x);
                    List<ColorAnimation> animations = DefaultChainAnimations.GetRainbowAnimation(animationTarget);

                    GraphicsEngine.StartAnimationChain(animations, true);
                }
            }
        }

        private static void UpdateUI()
        {
            if (GraphicsEngine._isMouseInside)
            {
                _indicator.UpdatePosition();
            }

            _grid.HighlightSelectedCell(_playerTurn ? Color.Firebrick : Color.Gold);
            _indicator.SetFillColor(_playerTurn ? Color.Firebrick : Color.Gold);
        }

        private static void UpdateConnect4()
        {
            if (_playerTurn)
            {
                DropMarker(CellType.Red);
            }
            else // TEMPORARY MANUAL MARKER PLACING UNTIL COMPUTER LOGIC IS IMPLEMENTED
            {
                DropMarker(CellType.Yellow);
            }
        }

        private static void DropMarker(CellType cellType)
        {
            int closestCol = _grid.GetClosestIndex();
            int furthestCell = _grid.FindFurthestCell(closestCol);

            // Drop a red marker
            if (GraphicsEngine._isMouseDown && _markerCooldownTracker.Elapsed.TotalSeconds > MARKER_COOLDOWN && furthestCell != -1)
            {
                _grid.SetGridCell(closestCol, furthestCell, cellType, EasingFunctions.GetEaseOutBounce(), MARKER_DROP_SPEED);

                _playerTurn = _playerTurn ? false : true;

                // Check if any player won
                _gameCheckResult = Connect.CheckWinCondition(_grid);

                // Restart cooldown
                _markerCooldownTracker.Restart();
            }
        }
    }
}
