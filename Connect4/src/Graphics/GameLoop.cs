using Connect4.src.Game;
using Connect4.src.Graphics.Sprites;
using Connect4.src.Logs;
using System.Drawing;

namespace Connect4.src.Graphics
{
    internal class GameLoop
    {
        private static Grid _grid;

        internal static void LoadGame()
        {
            GridLayout gridLayout = new GridLayout(7, 6, 90, 80, 10, Color.Black, Color.WhiteSmoke, 6, true, true);
            _grid = new Grid(gridLayout);
        }

        internal static void UpdateGame()
        {
            

            
        }

        internal static void RenderGame()
        {
            GraphicsEngine.ClearFrame();
            GraphicsEngine.ClearRenderBatch();

            // Run render code here!
            GraphicsEngine._renderBatch.AddGrid(_grid);

            Circle circle = new Circle(new SpriteView(700, 600, Color.Black, Color.Yellow, 6), 70);
            circle.Initialize();

            Triangle triangle = new Triangle(new SpriteView(500, 600, Color.Black, Color.Red, 9), 200, 200);
            triangle.Initialize();

            GraphicsEngine._renderBatch.AddSprite(circle);
            GraphicsEngine._renderBatch.AddSprite(triangle);

            GraphicsEngine.DrawRenderBatch();
        }
    }
}
