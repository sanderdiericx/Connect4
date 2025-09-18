using Connect4.src.Game;
using Connect4.src.Graphics.Sprites;
using Connect4.src.Logs;
using System.Drawing;

namespace Connect4.src.Graphics
{
    internal class GameLoop
    {
        public static void LoadGame()
        {

        }

        public static void UpdateGame()
        {
            

            
        }

        public static void RenderGame()
        {
            GraphicsEngine.ClearFrame();
            GraphicsEngine.ClearRenderBatch();

            if (GraphicsEngine.Grid == null)
            {
                Logger.LogWarning("Grid must be given to the graphics engine");

                return;
            }

            // Run render code here!

            Circle circle = new Circle(new SpriteView(700, 600, Color.Black, Color.LightYellow, 6), 70);
            circle.Initialize();
            circle.IsVisible = true;
            circle.IsFilled = true;
            circle.HasBorder = true;

            GraphicsEngine.RenderBatch.AddSprite(circle);

            GraphicsEngine.RenderBatch.AddGrid(GraphicsEngine.Grid);

            GraphicsEngine.DrawRenderBatch();
        }
    }
}
