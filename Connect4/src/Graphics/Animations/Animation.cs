namespace Connect4.src.Graphics
{
    internal abstract class Animation
    {
        internal protected float _t;
        internal protected AnimationTarget _animationTarget;
        internal protected bool _animationDone;

        internal Animation(AnimationTarget animationTarget)
        {
            _animationTarget = animationTarget;

            _t = 0;
            _animationDone = false;
        }

        internal virtual void AnimateSprite()
        {

        }

        // Resets an animation
        internal virtual void Reset()
        {
            _t = 0;
            _animationDone = false;
        }
    }
}
