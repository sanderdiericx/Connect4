using Connect4.src.Game;
using Connect4.src.Graphics.Sprites;
using Connect4.src.Logs;
using System.Drawing;

namespace Connect4.src.Graphics
{
    internal class GameLoop
    {
        private static Grid grid;


        internal static void LoadGame()
        {
            GridLayout gridLayout = new GridLayout(7, 6, 90, 80, 10, Color.Black, Color.WhiteSmoke, 6, true, true);
            grid = new Grid(gridLayout);
        }

        internal static void UpdateGame()
        {
            

            
        }

        internal static void RenderGame()
        {
            GraphicsEngine.ClearFrame();
            GraphicsEngine.ClearRenderBatch();

            // Run render code here!
            GraphicsEngine.RenderBatch.AddGrid(grid);

            Circle circle = new Circle(new SpriteView(700, 600, Color.Black, Color.LightYellow, 6), 70);
            circle.Initialize();
            circle.IsVisible = true;
            circle.IsFilled = true;
            circle.HasBorder = true;

            GraphicsEngine.RenderBatch.AddSprite(circle);

            GraphicsEngine.DrawRenderBatch();
        }
    }
}
