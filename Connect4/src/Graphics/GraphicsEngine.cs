using Connect4.src.Graphics.Sprites;
using Connect4.src.Logs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

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

            _windowMousePosition = new Point(0, 0);
            _windowWidth = width;
            _windowHeight = height;

            _isMouseInside = false;
            _isMouseDown = false;

            _btnNewGameClicked = false;

            _lastElapsedTime = 0;

            return _instance;
        }

        internal static Bitmap _frame;

        private static RenderBatch _renderBatch;
        private static AnimationBatch _animationBatch;

        internal static Point _windowMousePosition;
        internal static int _windowWidth;
        internal static int _windowHeight;
        internal static bool _isMouseInside;
        internal static bool _isMouseDown;

        internal static bool _btnNewGameClicked;

        internal static float _deltaTime;
        private static float _lastElapsedTime;

        private const int BYTES_PER_PIXEL = 4;

        /*
         * ANIMATIONS
         */

        // Starts an animation chain by adding it to the animation batch
        internal static void StartAnimationChain(IEnumerable<Animation> animations, bool endlessAnimation)
        {
            var chain = new Animations.AnimationChain(animations, endlessAnimation);

            // Start the first animation
            _animationBatch._animations.Add(chain._animations.ElementAt(0));

            _animationBatch.AddAnimationChain(chain);
        }

        internal static void StopAllAnimationChains()
        {
            _animationBatch._animationChains.Clear();
        }

        internal static void StopAllAnimations()
        {
            _animationBatch._animations.Clear();
        }


        internal static void StartAnimation(Animation animation)
        {
            _animationBatch._animations.Add(animation);
        }

        internal static void UpdateAnimations()
        {
            _animationBatch.UpdateAnimationChains();
            _animationBatch.Animate();
        }


        /*
         * RENDERING
         */
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

        // Clears the current frame bitmap
        internal static void ClearFrame()
        {
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, _frame.Width, _frame.Height);
            var bmpData = _frame.LockBits(rect, ImageLockMode.ReadWrite, _frame.PixelFormat);

            int bytes = bmpData.Stride * _frame.Height; // Calculate the amount of bytes in the frame
            byte[] buffer = new byte[bytes]; // This creates a byte array of all 0s (fully white)

            // Copy the buffer directly to memory at the location of our frame bitmap
            IntPtr startPointer = bmpData.Scan0; // Grab a pointer to the start adress in memory
            Marshal.Copy(buffer, 0, startPointer, bytes);

            _frame.UnlockBits(bmpData);
        }

        // Writes pixel data to the (bit locked!!!) bitmap memory address
        internal static void SetPixelInBitmap(BitmapData bmpData, PixelData pixelData)
        {
            unsafe
            {
                byte* ptr = (byte*)bmpData.Scan0; // Grab start adress pointer
                int index = (int)pixelData._pixelPosition.Y * bmpData.Stride + (int)pixelData._pixelPosition.X * BYTES_PER_PIXEL; // Compute pixel location in memory

                // Write pixel color data directly to computed adress
                ptr[index] = pixelData._pixelColor.B;
                ptr[index + 1] = pixelData._pixelColor.G;
                ptr[index + 2] = pixelData._pixelColor.R;
                ptr[index + 3] = pixelData._pixelColor.A;
            }
        }

        /*
         * OTHER
         */

        internal static void SetDeltaTime(float elapsedTime)
        {
            _deltaTime = elapsedTime - _lastElapsedTime;
            _lastElapsedTime = elapsedTime;
        }
    }
}
