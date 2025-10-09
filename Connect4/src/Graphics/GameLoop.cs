using Connect4.src.Game;
using System;
using System.Diagnostics;
using System.Drawing;

namespace Connect4.src.Graphics
{
    internal class GameLoop
    {
        private static Grid _grid;
        private static Indicator _indicator;

        // Game variables
        private static bool _playerTurn;
        private static readonly float _markerCooldown = 0.15f;
        private static Stopwatch _markerCooldownTracker;

        internal static void LoadGame()
        {
            GridLayout gridLayout = new GridLayout(7, 6, 90, 80, 10, Color.Black, Color.WhiteSmoke, 6, false, true);
            _grid = new Grid(gridLayout);

            _indicator = new Indicator(_grid, gridLayout, 50, Color.Black, Color.Firebrick, 12);

            _playerTurn = true;

            _markerCooldownTracker = new Stopwatch();
            _markerCooldownTracker.Start();
        }

        internal static void UpdateGame()
        {
            // Render updates
            if (GraphicsEngine._isMouseInside)
            {
                _indicator.UpdatePosition();
            }

            _grid.HighlightSelectedCell(_playerTurn ? Color.Firebrick : Color.Gold);
            _indicator.SetFillColor(_playerTurn ? Color.Firebrick : Color.Gold);

            GraphicsEngine.UpdateAnimations();

            // Game logic
            int closestCol = _grid.GetClosestIndex();

            if (_playerTurn)
            {
                // Drop a red marker
                if (GraphicsEngine._isMouseDown && _markerCooldownTracker.Elapsed.TotalSeconds > _markerCooldown)
                {
                    _grid.SetGridCell(closestCol, _grid.FindFurthestCell(closestCol), CellType.Red, EasingFunctions.GetEaseOutBounce(), 0.5f);

                    _playerTurn = false;

                    // Restart cooldown
                    _markerCooldownTracker.Restart();
                }
            }
            else // Computer turn
            {
                // Drop a yellow marker
                if (GraphicsEngine._isMouseDown && _markerCooldownTracker.Elapsed.TotalSeconds >_markerCooldown)
                {
                    _grid.SetGridCell(closestCol, _grid.FindFurthestCell(closestCol), CellType.Yellow, EasingFunctions.GetEaseOutBounce(), 0.5f);

                    _playerTurn = true;

                    // Restart cooldown
                    _markerCooldownTracker.Restart();
                }
            }
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
