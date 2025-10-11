using Connect4.src.Graphics.Sprites;
using System;
using System.Numerics;

namespace Connect4.src.Graphics
{
    internal abstract class Animation
    {
        internal protected Sprite sprite;
        internal protected float speed;
        internal protected float t;
        internal protected Func<float, float> easingFunction;

        internal protected bool animationDone;

        internal Animation(Sprite sprite, float speed, Func<float, float> easingFunction)
        {
            this.sprite = sprite;
            this.speed = speed;
            this.easingFunction = easingFunction;

            t = 0;
            animationDone = false;
        }

        internal virtual void AnimateSprite()
        {

        }
    }
}
