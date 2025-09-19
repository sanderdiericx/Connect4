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
                if (_isFilled)
                {
                    foreach (var pixelData in pixels.Where(p => p._pixelType == PixelType.Filling))
                    {
                        if (pixelData._pixelPosition.X >= 0 && pixelData._pixelPosition.X < GraphicsEngine._windowWidth && pixelData._pixelPosition.Y >= 0 && pixelData._pixelPosition.Y < GraphicsEngine._windowHeight)
                        {
                            GraphicsEngine._frame.SetPixel((int)pixelData._pixelPosition.X, (int)pixelData._pixelPosition.Y, pixelData._pixelColor);
                        }
                    }
                }

                if (_hasBorder)
                {
                    foreach (var pixelData in pixels.Where(p => p._pixelType == PixelType.Border))
                    {
                        if (pixelData._pixelPosition.X >= 0 && pixelData._pixelPosition.X < GraphicsEngine._windowWidth && pixelData._pixelPosition.Y >= 0 && pixelData._pixelPosition.Y < GraphicsEngine._windowHeight)
                        {
                            GraphicsEngine._frame.SetPixel((int)pixelData._pixelPosition.X, (int)pixelData._pixelPosition.Y, pixelData._pixelColor);
                        }
                    }
                }
            }
        }
    }
}
