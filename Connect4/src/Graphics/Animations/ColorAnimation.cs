using System;
using System.Drawing;
using System.Numerics;


namespace Connect4.src.Graphics
{
    internal class ColorAnimation : Animation
    {
        private Vector4 _startColor;
        private Vector4 _endColor;

        internal ColorAnimation(AnimationTarget animationTarget, Color endColor) : base(animationTarget)
        {
            // Normalize to 0-1 for interpolation
            _endColor = new Vector4(endColor.A / 255f, endColor.R / 255f, endColor.G / 255f, endColor.B / 255f);
            _startColor = new Vector4(_animationTarget._sprite._fillColor.A / 255f, _animationTarget._sprite._fillColor.R / 255f, _animationTarget._sprite._fillColor.G / 255f, _animationTarget._sprite._fillColor.B / 255f);
        }

        // Use linear interpolation to interpolate the sprite color to a target color
        internal override void AnimateSprite()
        {
            base.AnimateSprite();

            _t += Math.Min(_animationTarget._speed * GraphicsEngine._deltaTime, 1f);

            float easedT = _animationTarget._easingFunction(_t);

            Vector4 interpolatedColor = _startColor + (_endColor - _startColor) * easedT;

            if (_t <= 1f) // Check if animation is completed
            {
                // Convert back to 0-255
                Color color = Color.FromArgb((int)(interpolatedColor.X * 255), (int)(interpolatedColor.Y * 255), (int)(interpolatedColor.Z * 255), (int)(interpolatedColor.W * 255));

                _animationTarget._sprite.SetFillColor(color);
            }
            else
            {
                // Convert back to 0-255
                Color endColor = Color.FromArgb((int)(_endColor.X * 255), (int)(_endColor.Y * 255), (int)(_endColor.Z * 255), (int)(_endColor.W * 255));

                _animationTarget._sprite.SetFillColor(endColor);
                _animationDone = true;
            }
        }
    }
}
