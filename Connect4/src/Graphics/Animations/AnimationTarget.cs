using Connect4.src.Graphics.Sprites;
using System;

namespace Connect4.src.Graphics
{
    internal struct AnimationTarget
    {
        internal Sprite _sprite;
        internal float _speed;
        internal Func<float, float> _easingFunction;

        internal AnimationTarget(Sprite sprite, float speed, Func<float, float> easingFunction)
        {
            _sprite = sprite;
            _speed = speed;
            _easingFunction = easingFunction;
        }
    }
}
