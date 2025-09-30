using Connect4.src.Game;
using Connect4.src.Graphics.Sprites;
using Connect4.src.Logs;
using System.Drawing;
using System.Numerics;
using System.Diagnostics;

namespace Connect4.src.Graphics
{
    internal class GameLoop
    {
        private static Grid _grid;
        private static Triangle _triangle;
        private static Circle _circle;

        internal static void LoadGame()
        {
            GridLayout gridLayout = new GridLayout(7, 6, 90, 80, 10, Color.Black, Color.WhiteSmoke, 6, false, true);
            _grid = new Grid(gridLayout);

            _triangle = new Triangle(new SpriteView(500, 600, Color.Black, Color.Red, 18), 200, 200);
            _triangle.Initialize();

            _circle = new Circle(new SpriteView(515, 75, Color.Black, Color.Yellow, 9), 70);
            _circle.Initialize();
        }

        internal static void UpdateGame()
        {
            _circle.Transform(new Vector2(0, 100 * GraphicsEngine._deltaTime));
        }


        internal static void RenderGame()
        {
            GraphicsEngine.ClearFrame();
            GraphicsEngine.ClearRenderBatch();

            GraphicsEngine._renderBatch.AddSprite(_circle);
            GraphicsEngine._renderBatch.AddGrid(_grid);
            // GraphicsEngine._renderBatch.AddSprite(_triangle);
            
            GraphicsEngine.DrawRenderBatch();
        }
    }
}
