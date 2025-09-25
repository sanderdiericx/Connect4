using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System;
using Connect4.src.Logs;
using System.Linq;

namespace Connect4.src.Graphics.Sprites
{
    internal abstract class Sprite
    {
        protected List<PixelData> pixels;
        
        private bool _isInitialized;

        internal float _xPosition;
        internal float _yPosition;

        internal bool _isFilled;
        internal bool _hasBorder;
        internal bool _isVisible;

        internal Color _fillColor;
        internal Color _borderColor;
        internal int _borderSize;

        internal Sprite(SpriteView spriteView)
        {
            _xPosition = spriteView._x;
            _yPosition = spriteView._y;

            _isInitialized = false;
            _isFilled = true;
            _hasBorder = true;
            _isVisible = true;

            _borderColor = spriteView._borderColor;
            _fillColor = spriteView._fillColor;

            _borderSize = spriteView._borderSize;
        }
        
        internal virtual void RecalculatePixels()
        {
            
        }

        internal void Transform(Vector2 transform)
        {
            _xPosition += transform.X;
            _yPosition += transform.Y;
        }

        // Initialize builds pixel data for sprites, initialize can also be called to reset sprite size, border color and fillcolor
        internal virtual void Initialize()
        {
            if (_isInitialized)
            {
                Logger.LogWarning("Sprite already initialized!");

                return; // Return so that Initialize() doesnt run in subclasses
            }

            _isInitialized = true;
        }

        // Draws sprite to the current bitmap, draw order is determined by the order of input to the renderBatch
        internal void Draw()
        {
            if (!_isInitialized)
            {
                Logger.LogWarning("Sprite must be initialized before attempting to draw");

                return;
            }

            if (_isVisible)
            {
                // Lock bitmap bits
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, GraphicsEngine._frame.Width, GraphicsEngine._frame.Height);
                var bmpData = GraphicsEngine._frame.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, GraphicsEngine._frame.PixelFormat);

                if (_isFilled)
                {
                    foreach (var pixelData in pixels.Where(p => p._pixelType == PixelType.Filling))
                    {
                        if (pixelData._pixelPosition.X >= 0 && pixelData._pixelPosition.X < GraphicsEngine._windowWidth && pixelData._pixelPosition.Y >= 0 && pixelData._pixelPosition.Y < GraphicsEngine._windowHeight)
                        {
                            // Find the position in memory of this pixel
                            int index = (int)pixelData._pixelPosition.Y * bmpData.Stride + (int)pixelData._pixelPosition.X * GraphicsEngine._bytesPerPixel;

                            byte[] pixel = { pixelData._pixelColor.B, pixelData._pixelColor.G, pixelData._pixelColor.R, pixelData._pixelColor.A };

                            IntPtr startIndex = bmpData.Scan0; // Get a pointer to the start position of this pixel in memory
                            System.Runtime.InteropServices.Marshal.Copy(pixel, 0, startIndex + index, 4); // Copy the pixel color data into the right position in memory
                        }
                    }
                }

                if (_hasBorder)
                {
                    foreach (var pixelData in pixels.Where(p => p._pixelType == PixelType.Border))
                    {
                        if (pixelData._pixelPosition.X >= 0 && pixelData._pixelPosition.X < GraphicsEngine._windowWidth && pixelData._pixelPosition.Y >= 0 && pixelData._pixelPosition.Y < GraphicsEngine._windowHeight)
                        {
                            // Find the position in memory of this pixel
                            int index = (int)pixelData._pixelPosition.Y * bmpData.Stride + (int)pixelData._pixelPosition.X * GraphicsEngine._bytesPerPixel;

                            byte[] pixel = { pixelData._pixelColor.B, pixelData._pixelColor.G, pixelData._pixelColor.R, pixelData._pixelColor.A };

                            IntPtr startIndex = bmpData.Scan0; // Get a pointer to the start position of this pixel in memory
                            System.Runtime.InteropServices.Marshal.Copy(pixel, 0, startIndex + index, 4); // Copy the pixel color data into the right position in memory
                        }
                    }
                }

                GraphicsEngine._frame.UnlockBits(bmpData);
            }
        }
    }
}
