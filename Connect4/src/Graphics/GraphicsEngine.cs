using Connect4.src.Graphics.Sprites;
using Connect4.src.Logs;
using System;
using System.Collections.Generic;
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
            _animationBatch = new AnimationBatch();

            _windowWidth = width;
            _windowHeight = height;

            _bytesPerPixel = 4; // Standard for argb value

            _lastElapsedTime = 0;

            return _instance;
        }

        internal static Bitmap _frame;

        private static RenderBatch _renderBatch;
        private static AnimationBatch _animationBatch;

        internal static int _windowWidth;
        internal static int _windowHeight;

        internal static float _deltaTime;
        private static float _lastElapsedTime;

        private static int _bytesPerPixel;

        internal static void StartAnimation(Animation animation)
        {
            _animationBatch._animations.Add(animation);
        }

        internal static void UpdateAnimations()
        {
            _animationBatch.Animate();
        }

        internal static void DrawRenderBatch()
        {
            _renderBatch.Draw();
        }

        internal static void ClearRenderBatch()
        {
            _renderBatch.Clear();
        }

        internal static void AddSpriteToQueue(Sprite sprite)
        {
            _renderBatch._sprites.Add(sprite);
        }

        internal static void AddSpritesToQueue(IEnumerable<Sprite> sprites)
        {
            foreach (Sprite sprite in sprites)
            {
                _renderBatch._sprites.Add(sprite);
            }
        }

        internal static void SetDeltaTime(float elapsedTime)
        {
            _deltaTime = elapsedTime - _lastElapsedTime;
            _lastElapsedTime = elapsedTime;
        }

        internal static void ClearFrame()
        {
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, _frame.Width, _frame.Height);
            var bmpData = _frame.LockBits(rect, ImageLockMode.ReadWrite, _frame.PixelFormat);

            int bytes = bmpData.Stride * _frame.Height; // Calculate the amount of bytes in the frame
            byte[] buffer = new byte[bytes]; // This creates a byte array of all 0s (fully white)

            // Copy the buffer directly to memory at the location of our frame bitmap
            IntPtr startPointer = bmpData.Scan0; // Grab a pointer to the start adress in memory
            System.Runtime.InteropServices.Marshal.Copy(buffer, 0, startPointer, bytes);

            _frame.UnlockBits(bmpData);
        }

        // Writes pixel data to the a (bit locked!!!) bitmap memory address
        internal static void SetPixelInBitmap(BitmapData bmpData, PixelData pixelData)
        {
            unsafe
            {
                byte* ptr = (byte*)bmpData.Scan0; // Grab start adress pointer
                int index = (int)pixelData._pixelPosition.Y * bmpData.Stride + (int)pixelData._pixelPosition.X * _bytesPerPixel; // Compute pixel location in memory

                // Write pixel color data directly to computed adress
                ptr[index] = pixelData._pixelColor.B;
                ptr[index + 1] = pixelData._pixelColor.G;
                ptr[index + 2] = pixelData._pixelColor.R;
                ptr[index + 3] = pixelData._pixelColor.A;
            }
        }
    }
}
