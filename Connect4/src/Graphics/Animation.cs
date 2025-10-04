using Connect4.src.Graphics.Sprites;
using System;
using System.Numerics;

namespace Connect4.src.Graphics
{
    internal class Animation
    {
        private Sprite _sprite;
        private float _startY;
        private float _endY;
        private float _speed;
        private float _t;
        private Func<float, float> _easingFunction;

        internal bool _animationDone;

        internal Animation(Sprite sprite, float targetY, float speed, Func<float, float> easingFunction)
        {
            _sprite = sprite;
            _endY = targetY;
            _speed = speed;
            _easingFunction = easingFunction;

            _startY = sprite._yPosition;
            _t = 0;
            _animationDone = false;
        }

        // Use LERP to interpolate the sprite position to a target position
        internal void AnimateSprite()
        {
            _t += Math.Min(_speed * GraphicsEngine._deltaTime, 1f);

            float easedT = _easingFunction(_t);

            float currentYPosition = _startY + (_endY - _startY) * easedT;

            if (_t <= 1f) // Check if animation is completed
            {
                _sprite.SetPosition(new Vector2(_sprite._xPosition, currentYPosition));
            }
            else
            {
                _sprite.SetPosition(new Vector2(_sprite._xPosition, _endY));
                _animationDone = true;
            }
        }
    }
}
