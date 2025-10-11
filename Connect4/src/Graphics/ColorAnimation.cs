using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using Connect4.src.Graphics.Sprites;


namespace Connect4.src.Graphics
{
    internal class ColorAnimation : Animation
    {
        private Vector4 _startColor;
        private Vector4 _endColor;

        internal ColorAnimation(Sprite sprite, float speed, Func<float, float> easingFunction, Color endColor) : base(sprite, speed, easingFunction)
        {
            // Normalize to 0-1 for interpolation
            _endColor = new Vector4(endColor.A / 255f, endColor.R / 255f, endColor.G / 255f, endColor.B / 255f);
            _startColor = new Vector4(sprite._fillColor.A / 255f, sprite._fillColor.R / 255f, sprite._fillColor.G / 255f, sprite._fillColor.B / 255f);
        }

        // Use linear interpolation to interpolate the sprite color to a target color
        internal override void AnimateSprite()
        {
            base.AnimateSprite();

            t += Math.Min(speed * GraphicsEngine._deltaTime, 1f);

            float easedT = easingFunction(t);

            Vector4 interpolatedColor = _startColor + (_endColor- _startColor) * easedT;

            if (t <= 1f) // Check if animation is completed
            {
                // Convert back to 0-255
                Color color = Color.FromArgb((int)(interpolatedColor.X * 255), (int)(interpolatedColor.Y * 255), (int)(interpolatedColor.Z * 255), (int)(interpolatedColor.W * 255));

                sprite.SetFillColor(color);
            }
            else
            {
                // Convert back to 0-255
                Color endColor = Color.FromArgb((int)(_endColor.X * 255), (int)(_endColor.Y * 255), (int)(_endColor.Z * 255), (int)(_endColor.W * 255));

                sprite.SetFillColor(endColor);
                animationDone = true;
            }
        }
    }
}
