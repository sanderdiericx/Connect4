using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System;
using Connect4.src.Logs;
using System.Linq;
using System.Drawing.Imaging;

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

        internal void Transform(Vector2 transform)
        {
            _xPosition += transform.X;
            _yPosition += transform.Y;

            // Loop through all pixels and apply transformation
            for (int i = 0; i < pixels.Count; i++)
            {
                pixels[i]._pixelPosition += transform;
            }
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
        internal void Draw(BitmapData bmpData)
        {
            if (!_isInitialized)
            {
                Logger.LogWarning("Sprite must be initialized before attempting to draw");

                return;
            }

            if (_isVisible)
            {
                if (_isFilled)
                {
                    for (int i = 0; i < pixels.Count; i++)
                    {
                        PixelData pixel = pixels[i];

                        if (pixel._pixelPosition.X >= 0 && pixel._pixelPosition.X < GraphicsEngine._windowWidth && pixel._pixelPosition.Y >= 0 && pixel._pixelPosition.Y < GraphicsEngine._windowHeight)
                        {
                            if (pixel._pixelType != PixelType.Border)
                            {
                                GraphicsEngine.SetPixelInBitmap(bmpData, pixel);
                            }
                        }
                    }
                }


                if (_hasBorder)
                {
                    for (int i = 0; i < pixels.Count; i++)
                    {
                        PixelData pixel = pixels[i];

                        if (pixel._pixelPosition.X >= 0 && pixel._pixelPosition.X < GraphicsEngine._windowWidth && pixel._pixelPosition.Y >= 0 && pixel._pixelPosition.Y < GraphicsEngine._windowHeight)
                        {
                            if (pixel._pixelType != PixelType.Filling)
                            {
                                GraphicsEngine.SetPixelInBitmap(bmpData, pixel);
                            }
                        }
                    }
                }
            }
        }
    }
}
