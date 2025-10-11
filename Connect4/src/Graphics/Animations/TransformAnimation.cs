using System;
using System.Numerics;

namespace Connect4.src.Graphics
{
    internal class TransformAnimation : Animation
    {
        private Vector2 _startPosition;
        private Vector2 _endPosition;

        internal TransformAnimation(AnimationTarget animationTarget, Vector2 endPosition) : base(animationTarget)
        {
            _endPosition = endPosition;
            _startPosition = new Vector2(_animationTarget._sprite._xPosition, _animationTarget._sprite._yPosition);
        }

        // Use linear interpolation to interpolate the sprite position to a target position
        internal override void AnimateSprite()
        {
            base.AnimateSprite();

            _t += Math.Min(_animationTarget._speed * GraphicsEngine._deltaTime, 1f);

            float easedT = _animationTarget._easingFunction(_t);

            Vector2 currentPosition = _startPosition + (_endPosition - _startPosition) * easedT;

            if (_t <= 1f) // Check if animation is completed
            {
                _animationTarget._sprite.SetPosition(currentPosition);
            }
            else
            {
                _animationTarget._sprite.SetPosition(_endPosition);
                _animationDone = true;
            }
        }
    }
}
