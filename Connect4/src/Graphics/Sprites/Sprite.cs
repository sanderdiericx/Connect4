using Connect4.src.Logs;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;

namespace Connect4.src.Graphics.Sprites
{
    internal abstract class Sprite
    {
        internal protected List<PixelData> _pixels;

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

        internal void SetFillColor(Color fillColor)
        {
            foreach (var pixel in _pixels)
            {
                if (pixel._pixelType == PixelType.Filling)
                {
                    pixel._pixelColor = fillColor;
                }
            }
        }

        internal void SetBorderColor(Color borderColor)
        {
            foreach (var pixel in _pixels)
            {
                if (pixel._pixelType == PixelType.Border)
                {
                    pixel._pixelColor = borderColor;
                }
            }
        }


        internal void SetPosition(Vector2 position)
        {
            Vector2 delta = position - new Vector2(_xPosition, _yPosition); // Calculate the difference from current position

            _xPosition = position.X;
            _yPosition = position.Y;

            // Move pixels by the same delta
            for (int i = 0; i < _pixels.Count; i++)
            {
                _pixels[i]._pixelPosition += delta;
            }
        }


        internal void Transform(Vector2 transform)
        {
            _xPosition += transform.X;
            _yPosition += transform.Y;

            // Loop through all pixels and apply transformation
            for (int i = 0; i < _pixels.Count; i++)
            {
                _pixels[i]._pixelPosition += transform;
            }
        }

        // Initialize builds pixel data for sprites, initialize can also be called to reset sprite size, border color and fillcolor
        internal virtual void Initialize()
        {
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
                    for (int i = 0; i < _pixels.Count; i++)
                    {
                        PixelData pixel = _pixels[i];

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
                    for (int i = 0; i < _pixels.Count; i++)
                    {
                        PixelData pixel = _pixels[i];

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
