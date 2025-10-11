using Connect4.src.Game;
using System.Diagnostics;
using System.Drawing;
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

        private static GameState _gameState;
        private static Winner _winner;

        internal static void LoadGame()
        {
            // Setup game grid
            GridLayout gridLayout = new GridLayout(GRID_COLS, GRID_ROWS, GRID_GAP, GRID_PADDING, GRID_OFFSET, Color.Black, Color.WhiteSmoke, GRID_BORDERSIZE, false, true);
            _grid = new Grid(gridLayout);

            _indicator = new Indicator(_grid, gridLayout, 50, Color.Black, Color.Firebrick, 12);

            _gameState = GameState.Playing;
            _winner = Winner.None;
            _playerTurn = true;

            _markerCooldownTracker = new Stopwatch();
            _markerCooldownTracker.Start();
        }

        internal static void UpdateGame()
        {
            // Render updates
            if (_gameState == GameState.Playing)
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

            if (_gameState == GameState.Playing) // TEMPORARY
            {
                GraphicsEngine.AddSpritesToQueue(_grid.GetSprites());
                GraphicsEngine.AddSpriteToQueue(_indicator._triangle);
            }

            GraphicsEngine.DrawRenderBatch();
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
            int closestCol = _grid.GetClosestIndex();
            int furthestCell = _grid.FindFurthestCell(closestCol);

            if (_playerTurn)
            {
                // Drop a red marker
                if (GraphicsEngine._isMouseDown && _markerCooldownTracker.Elapsed.TotalSeconds > MARKER_COOLDOWN && furthestCell != -1)
                {
                    _grid.SetGridCell(closestCol, furthestCell, CellType.Red, EasingFunctions.GetEaseOutBounce(), MARKER_DROP_SPEED);

                    _playerTurn = false;

                    // Check if any player won
                    (GameState gameState, Winner winner) gameOutput = Connect.CheckWinCondition(_grid);

                    _gameState = gameOutput.gameState;
                    _winner = gameOutput.winner;

                    // Restart cooldown
                    _markerCooldownTracker.Restart();
                }
            }
            else // TEMPORARY MANUAL MARKER PLACING UNTIL COMPUTER IS IMPLEMENTED
            {
                // Drop a yellow marker
                if (GraphicsEngine._isMouseDown && _markerCooldownTracker.Elapsed.TotalSeconds > MARKER_COOLDOWN && furthestCell != -1)
                {
                    _grid.SetGridCell(closestCol, furthestCell, CellType.Yellow, EasingFunctions.GetEaseOutBounce(), MARKER_DROP_SPEED);

                    _playerTurn = true;

                    // Check if anyone won
                    (GameState gameState, Winner winner) gameOutput = Connect.CheckWinCondition(_grid);

                    _gameState = gameOutput.gameState;
                    _winner = gameOutput.winner;

                    // Restart cooldown
                    _markerCooldownTracker.Restart();
                }
            }
        }
    }
}
