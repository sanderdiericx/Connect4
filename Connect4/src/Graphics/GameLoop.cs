using Connect4.src.Game;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Connect = Connect4.src.Game.Connect;

namespace Connect4.src.Graphics
{
    internal class GameLoop
    {
        private const int GRID_COLS = 7;
        private const int GRID_ROWS = 6;
        private const int GRID_GAP = 90;
        private const int GRID_PADDING = 80;
        private const int GRID_OFFSET = 10;
        private const int GRID_BORDERSIZE = 6;

        private static GridLayout _gridLayout;
        private static Connect _game;
        private static GameCheckResult _gameCheckResult;
        private static bool _gameOverHappened;

        internal static void LoadGame()
        {
            // Setup first game
            _gridLayout = new GridLayout(GRID_COLS, GRID_ROWS, GRID_GAP, GRID_PADDING, GRID_OFFSET, Color.Black, Color.WhiteSmoke, GRID_BORDERSIZE, false, true);
            _game = new Connect(_gridLayout);

            _gameCheckResult = new GameCheckResult(GameState.Playing, Winner.None, new List<Vector2>());
            _gameOverHappened = false;
        }

        internal static void UpdateGame()
        {
            GameState gameState = _gameCheckResult._gameState;

            if (gameState == GameState.GameOver)
            {
                if (!_gameOverHappened) // Run first time game over code
                {
                    _game.GameOver(_gameCheckResult);

                    _game.ShowWinUI(_gameCheckResult);

                    _gameOverHappened = true;
                }

                // Reset game
                if (GraphicsEngine._btnNewGameClicked)
                {
                    _game.HideWinUI();

                    _game = new Connect(_gridLayout);
                    _gameOverHappened = false;
                    _gameCheckResult = new GameCheckResult(GameState.Playing, Winner.None, new List<Vector2>());

                    GraphicsEngine.StopAllAnimations();
                    GraphicsEngine.StopAllAnimationChains();
                }
            }

            if (gameState == GameState.Playing)
            {
                // Try drop a player marker
                if (_game._playerTurn == true)
                {
                    if (_game.TryDropMarker(CellType.Red))
                    {
                        _gameCheckResult = _game.CheckWinCondition();

                        _game.NextTurn();
                    }
                }
                else // Computer turn
                {
                    if (_game.TryDropMarker(CellType.Yellow))
                    {
                        _gameCheckResult = _game.CheckWinCondition();

                        _game.NextTurn();
                    }
                }

                _game.UpdateUI();
            }

            GraphicsEngine.UpdateAnimations();
        }

        internal static void RenderGame()
        {
            GraphicsEngine.ClearFrame();
            GraphicsEngine.ClearRenderBatch();

            GraphicsEngine.AddSpritesToQueue(_game._grid.GetSprites());
            GraphicsEngine.AddSpriteToQueue(_game._indicator._triangle);

            GraphicsEngine.DrawRenderBatch();
        }
    }
}
