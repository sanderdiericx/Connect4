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

        public float XPosition;
        public float YPosition;

        public bool IsFilled;
        public bool HasBorder;
        public bool IsVisible;

        public Color FillColor;
        public Color BorderColor;
        public int BorderSize;

        public Sprite(SpriteView spriteView)
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

        public virtual void Initialize()
        {
            if (_isInitialized)
            {
                Logger.LogWarning("Sprite already initialized!");

                return; // Return so that Initialize() doesnt run in subclasses
            }

            _isInitialized = true;
        }

        // Draws sprite to the current bitmap, draw order is determined by the order of input to the renderBatch
        public virtual void Draw()
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
