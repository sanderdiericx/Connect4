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

        internal float XPosition;
        internal float YPosition;

        internal bool IsFilled;
        internal bool HasBorder;
        internal bool IsVisible;

        internal Color FillColor;
        internal Color BorderColor;
        internal int BorderSize;

        internal Sprite(SpriteView spriteView)
        {
            XPosition = spriteView.X;
            YPosition = spriteView.Y;

            _isInitialized = false;
            IsFilled = true;
            HasBorder = true;
            IsVisible = true;

            BorderColor = spriteView.BorderColor;
            FillColor = spriteView.FillColor;

            BorderSize = spriteView.BorderSize;
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

            if (IsVisible)
            {
                if (IsFilled)
                {
                    foreach (var pixelData in pixels.Where(p => p.PixelType == PixelType.Filling))
                    {
                        if (pixelData.PixelPosition.X >= 0 && pixelData.PixelPosition.X < GraphicsEngine.WindowWidth && pixelData.PixelPosition.Y >= 0 && pixelData.PixelPosition.Y < GraphicsEngine.WindowHeight)
                        {
                            GraphicsEngine.Frame.SetPixel((int)pixelData.PixelPosition.X, (int)pixelData.PixelPosition.Y, pixelData.PixelColor);
                        }
                    }
                }

                if (HasBorder)
                {
                    foreach (var pixelData in pixels.Where(p => p.PixelType == PixelType.Border))
                    {
                        if (pixelData.PixelPosition.X >= 0 && pixelData.PixelPosition.X < GraphicsEngine.WindowWidth && pixelData.PixelPosition.Y >= 0 && pixelData.PixelPosition.Y < GraphicsEngine.WindowHeight)
                        {
                            GraphicsEngine.Frame.SetPixel((int)pixelData.PixelPosition.X, (int)pixelData.PixelPosition.Y, pixelData.PixelColor);
                        }
                    }
                }
            }
        }
    }
}
