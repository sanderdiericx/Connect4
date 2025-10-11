using Connect4.src.Graphics.Sprites;
using System;
using System.Numerics;

namespace Connect4.src.Graphics
{
    internal class TransformAnimation : Animation
    {
        private Vector2 _startPosition;
        private Vector2 _endPosition;

        internal TransformAnimation(Sprite sprite, float speed, Func<float, float> easingFunction, Vector2 endPosition) : base(sprite, speed, easingFunction)
        {
            this.sprite = sprite;
            this.speed = speed;
            this.easingFunction = easingFunction;

            _endPosition = endPosition;
            _startPosition = new Vector2(sprite._xPosition, sprite._yPosition);
        }

        // Use LERP to interpolate the sprite position to a target position
        internal override void AnimateSprite()
        {
            base.AnimateSprite();

            t += Math.Min(speed * GraphicsEngine._deltaTime, 1f);

            float easedT = easingFunction(t);

            Vector2 currentPosition = _startPosition + (_endPosition - _startPosition) * easedT;

            if (t <= 1f) // Check if animation is completed
            {
                sprite.SetPosition(currentPosition);
            }
            else
            {
                sprite.SetPosition(_endPosition);
                animationDone = true;
            }
        }
    }
}
