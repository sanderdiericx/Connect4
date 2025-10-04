using Connect4.src.Game;
using System;
using System.Drawing;

namespace Connect4.src.Graphics
{
    internal class GameLoop
    {
        private static Grid _grid;

        internal static void LoadGame()
        {
            GridLayout gridLayout = new GridLayout(7, 6, 90, 80, 10, Color.Black, Color.WhiteSmoke, 6, false, true);
            _grid = new Grid(gridLayout);

            _grid.SetGridCell(6, 5, CellType.Red, EasingFunctions.GetEaseOutBounce(), 0.5f);
        }

        internal static void UpdateGame()
        {
            GraphicsEngine.UpdateAnimations();
        }

        internal static void RenderGame()
        {
            GraphicsEngine.ClearFrame();
            GraphicsEngine.ClearRenderBatch();

            GraphicsEngine.AddSpritesToQueue(_grid.GetSprites());

            GraphicsEngine.DrawRenderBatch();
        }
    }
}
