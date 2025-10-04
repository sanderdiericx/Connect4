using Connect4.src.Graphics.Sprites;
using System;
using System.Numerics;

namespace Connect4.src.Graphics
{
    internal class Animation
    {
        private Sprite _sprite;
        private Vector2 _startPosition;
        private Vector2 _endPosition;
        private float _speed;
        private float _t;
        private Func<float, float> _easingFunction;

        internal bool _animationDone;

        internal Animation(Sprite sprite, Vector2 endPosition, float speed, Func<float, float> easingFunction)
        {
            _sprite = sprite;
            _endPosition = endPosition;
            _speed = speed;
            _easingFunction = easingFunction;

            _startPosition = new Vector2(sprite._xPosition, sprite._yPosition);
            _t = 0;
            _animationDone = false;
        }

        // Use LERP to interpolate the sprite position to a target position
        internal void AnimateSprite()
        {
            _t += Math.Min(_speed * GraphicsEngine._deltaTime, 1f);

            float easedT = _easingFunction(_t);

            Vector2 currentPosition = _startPosition + (_endPosition - _startPosition) * easedT;

            if (_t <= 1f) // Check if animation is completed
            {
                _sprite.SetPosition(currentPosition);
            }
            else
            {
                _sprite.SetPosition(_endPosition);
                _animationDone = true;
            }
        }
    }
}
