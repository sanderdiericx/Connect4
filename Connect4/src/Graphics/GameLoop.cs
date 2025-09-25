using Connect4.src.Game;
using Connect4.src.Graphics.Sprites;
using Connect4.src.Logs;
using System.Drawing;
using System.Numerics;

namespace Connect4.src.Graphics
{
    internal class GameLoop
    {
        private static Grid _grid;
        private static Triangle _triangle;
        private static Circle _circle;

        internal static void LoadGame()
        {
            GridLayout gridLayout = new GridLayout(7, 6, 90, 80, 10, Color.Black, Color.WhiteSmoke, 6, true, true);
            _grid = new Grid(gridLayout);

            _triangle = new Triangle(new SpriteView(500, 600, Color.Black, Color.Red, 9), 200, 200);
            _triangle.Initialize();

            _circle = new Circle(new SpriteView(700, 600, Color.Black, Color.Yellow, 9), 70);
            _circle.Initialize();
        }

        internal static void UpdateGame()
        {
            _triangle.Transform(new Vector2(1, 0));
        }

        internal static void RenderGame()
        {
            GraphicsEngine.ClearFrame();
            GraphicsEngine.ClearRenderBatch();

            // Run render code here!
            
            
            // GraphicsEngine._renderBatch.AddGrid(_grid);
            // GraphicsEngine._renderBatch.AddSprite(circle);
            GraphicsEngine._renderBatch.AddSprite(_triangle);

            GraphicsEngine.DrawRenderBatch();
        }
    }
}
