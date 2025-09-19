using Connect4.src.Game;
using Connect4.src.Logs;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace Connect4.src.Graphics
{
    internal class GraphicsEngine
    {
        // Singleton pattern so only 1 instance may be created
        private GraphicsEngine() { }

        private static GraphicsEngine _instance = null;
        internal static GraphicsEngine Start(int width, int height)
        {
            if (_instance == null)
            {
                _instance = new GraphicsEngine();
            }

            Logger.ClearLogs();

            _frame = new Bitmap(width, height);

            _renderBatch = new RenderBatch();

            _windowWidth = width;
            _windowHeight = height;

            return _instance;
        }

        internal static Bitmap _frame;
        internal static RenderBatch _renderBatch;

        internal static int _windowWidth;
        internal static int _windowHeight;

        internal static void DrawRenderBatch()
        {
            _renderBatch.Draw();
        }

        internal static void ClearRenderBatch()
        {
            _renderBatch.Clear();
        }

        internal static void ClearFrame()
        {
            for (int x = 0; x < _frame.Width; x++)
            {
                for (int y = 0; y < _frame.Height; y++)
                {
                    _frame.SetPixel(x, y, Color.White);
                }
            }
        }
    }
}
