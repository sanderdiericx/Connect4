using Connect4.src.Game;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using Timer = System.Windows.Forms.Timer;

namespace Connect4.src.Graphics
{
    internal class GameLoop
    {
        private static Grid _grid;
        private static Indicator _indicator;

        internal static void LoadGame()
        {
            GridLayout gridLayout = new GridLayout(7, 6, 90, 80, 10, Color.Black, Color.WhiteSmoke, 6, false, true);
            _grid = new Grid(gridLayout);

            _grid.SetGridCell(3, 5, CellType.Red, EasingFunctions.GetEaseOutBounce(), 0.5f);

            _indicator = new Indicator(gridLayout, 75, Color.Black, Color.DimGray, 12);
        }

        internal static void UpdateGame()
        {
            if (GraphicsEngine._isMouseInside)
            {
                _indicator.UpdatePosition(Cursor.Position.X);
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
