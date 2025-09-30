using Connect4.src.Game;
using Connect4.src.Graphics.Sprites;
using System.Drawing;

namespace Connect4.src.Graphics
{
    internal class GameLoop
    {
        private static Grid _grid;
        private static Circle _marker;
        private static Circle _marker2;

        internal static void LoadGame()
        {
            GridLayout gridLayout = new GridLayout(7, 6, 90, 80, 10, Color.Black, Color.WhiteSmoke, 6, false, true);
            _grid = new Grid(gridLayout);

            _marker = ConnectFour.SetGridCell(_grid, 4, 4, CellType.Red);
            _marker2 = ConnectFour.SetGridCell(_grid, 4, 2, CellType.Yellow);
        }

        internal static void UpdateGame()
        {

        }

        internal static void RenderGame()
        {
            GraphicsEngine.ClearFrame();
            GraphicsEngine.ClearRenderBatch();

            GraphicsEngine._renderBatch.AddSprite(_marker);
            GraphicsEngine._renderBatch.AddSprite(_marker2);
            GraphicsEngine._renderBatch.AddGrid(_grid);

            GraphicsEngine.DrawRenderBatch();
        }
    }
}
