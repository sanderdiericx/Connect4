using Connect4.src.Game;
using Connect4.src.Graphics.Animations;
using Connect4.src.Graphics.Sprites;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using Connect = Connect4.src.Game.Connect;

namespace Connect4.src.Graphics
{
    internal class GameLoop
    {
        internal static Grid _grid;
        internal static Indicator _indicator;

        internal static Stopwatch _markerCooldownTracker;

        internal const float MARKER_COOLDOWN = 0.15f;
        internal const float MARKER_DROP_SPEED = 0.5f;
        private const int GRID_COLS = 7;
        private const int GRID_ROWS = 6;
        private const int GRID_GAP = 90;
        private const int GRID_PADDING = 80;
        private const int GRID_OFFSET = 10;
        private const int GRID_BORDERSIZE = 6;

        internal static bool _playerTurn;
        internal static GameCheckResult _gameCheckResult;
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
                Connect.GameOver();

                _gameOverHappened = true;
            }

            if (gameState == GameState.Playing)
            {
                Connect.UpdateConnect4();
                Connect.UpdateUI();
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
    }
}
