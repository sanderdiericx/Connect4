using Connect4.src.Logs;
using System;
using System.Drawing;
using System.Drawing.Imaging;

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

            _bytesPerPixel = 4; // Standard for argb value

            return _instance;
        }

        internal static Bitmap _frame;
        internal static RenderBatch _renderBatch;

        internal static int _windowWidth;
        internal static int _windowHeight;

        internal static int _bytesPerPixel;

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
            Rectangle rect = new Rectangle(0, 0, _frame.Width, _frame.Height);
            var bmpData = _frame.LockBits(rect, ImageLockMode.ReadWrite, _frame.PixelFormat);

            for (int x = 0; x < _frame.Width; x++)
            {
                for (int y = 0; y < _frame.Height; y++)
                {
                    // Find the position in memory of this pixel
                    int index = y * bmpData.Stride + x * _bytesPerPixel;

                    byte[] pixel = { 0, 0, 0, 0 };

                    IntPtr startIndex = bmpData.Scan0; // Get a pointer to the start position of this pixel in memory
                    System.Runtime.InteropServices.Marshal.Copy(pixel, 0, startIndex + index, 4); // Copy the pixel color data into the right position in memory
                }
            }

            _frame.UnlockBits(bmpData);
        }
    }
}
