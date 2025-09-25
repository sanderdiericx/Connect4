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
        private static Stopwatch _stopWatch;

        internal static void LoadGame()
        {
            GridLayout gridLayout = new GridLayout(7, 6, 90, 80, 10, Color.Black, Color.WhiteSmoke, 6, true, true);
            _grid = new Grid(gridLayout);

            _triangle = new Triangle(new SpriteView(500, 600, Color.Black, Color.Red, 18), 200, 200);
            _triangle.Initialize();

            _circle = new Circle(new SpriteView(700, 600, Color.Black, Color.Yellow, 9), 70);
            _circle.Initialize();

            _stopWatch = new Stopwatch();
        }

        internal static void UpdateGame()
        {
            _triangle.Transform(new Vector2(5, -1));
        }

        internal static void RenderGame()
        {
            _stopWatch.Restart();

            GraphicsEngine.ClearFrame();

            _stopWatch.Stop();
            Logger.LogInfo($"Frame cleared in {_stopWatch.ElapsedMilliseconds}ms");
            _stopWatch.Restart();

            GraphicsEngine.ClearRenderBatch();

            _stopWatch.Stop();
            Logger.LogInfo($"RenderBatch cleared in {_stopWatch.ElapsedMilliseconds}ms");
            _stopWatch.Restart();


            // Run render code here!


            GraphicsEngine._renderBatch.AddGrid(_grid);
            GraphicsEngine._renderBatch.AddSprite(_circle);
            GraphicsEngine._renderBatch.AddSprite(_triangle);

            _stopWatch.Stop();
            Logger.LogInfo($"Added sprites to renderbatch in {_stopWatch.ElapsedMilliseconds}ms");
            _stopWatch.Restart();


            GraphicsEngine.DrawRenderBatch();

            _stopWatch.Stop();
            Logger.LogInfo($"drew renderbatch in {_stopWatch.ElapsedMilliseconds}ms");
        }
    }
}
