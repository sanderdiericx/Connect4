using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System;
using Connect4.src.Logs;

namespace Connect4.src.Graphics.Sprites
{
    internal abstract class Sprite
    {
        protected float xPosition;
        protected float yPosition;
        protected List<Vector2> borderPositions;
        protected List<Vector2> pixelPositions;
        protected int borderSize;

        private bool _isInitialized;
        
        public bool IsFilled;
        public bool HasBorder;
        public bool IsVisible;

        private Color _fillColor;
        private Color _borderColor;


        public Sprite(SpriteView spriteView)
        {
            xPosition = spriteView.X;
            yPosition = spriteView.Y;

            _isInitialized = false;
            IsFilled = true;
            HasBorder = true;
            IsVisible = true;

            _borderColor = spriteView.BorderColor;
            _fillColor = spriteView.FillColor;

            borderSize = spriteView.BorderSize;
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
                    foreach (var position in pixelPositions)
                    {
                        if (position.X >= 0 && position.X < GraphicsEngine.WindowWidth && position.Y >= 0 && position.Y < GraphicsEngine.WindowHeight)
                        {
                            GraphicsEngine.Frame.SetPixel((int)position.X, (int)position.Y, _fillColor);
                        }
                    }
                }

                if (HasBorder)
                {
                    foreach (var position in borderPositions)
                    {
                        if (position.X >= 0 && position.X < GraphicsEngine.WindowWidth && position.Y >= 0 && position.Y < GraphicsEngine.WindowHeight)
                        {
                            GraphicsEngine.Frame.SetPixel((int)position.X, (int)position.Y, _borderColor);
                        }
                    }
                }
            }
        }
    }
}
