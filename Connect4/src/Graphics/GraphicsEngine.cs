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
        public static GraphicsEngine Start(int width, int height)
        {
            if (_instance == null)
            {
                _instance = new GraphicsEngine();
            }

            Logger.ClearLogs();

            Frame = new Bitmap(width, height);

            RenderBatch = new RenderBatch();

            WindowWidth = width;
            WindowHeight = height;

            return _instance;
        }

        public static Bitmap Frame;
        public static RenderBatch RenderBatch;

        public static int WindowWidth;
        public static int WindowHeight;

        public static void DrawRenderBatch()
        {
            RenderBatch.Draw();
        }

        public static void ClearRenderBatch()
        {
            RenderBatch.Clear();
        }

        public static void ClearFrame()
        {
            for (int x = 0; x < Frame.Width; x++)
            {
                for (int y = 0; y < Frame.Height; y++)
                {
                    Frame.SetPixel(x, y, Color.White);
                }
            }
        }
    }
}
