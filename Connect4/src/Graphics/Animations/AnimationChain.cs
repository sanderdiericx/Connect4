using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Connect4.src.Graphics.Animations
{
    internal class AnimationChain
    {
        internal IEnumerable<Animation> _animations;
        internal bool _endlessAnimation;

        internal AnimationChain(IEnumerable<Animation> animations, bool endlessAnimation)
        {
            _animations = animations;
            _endlessAnimation = endlessAnimation;

            // If the animation is endless, we need to add a new animation from end animation, back to start animation
            if (endlessAnimation)
            {
                List<Animation> animationList = _animations.ToList();

                AnimationTarget lastAnimationTarget = _animations.Last()._animationTarget;
                Color firstColor = _animations.ElementAt(0)._animationTarget._sprite._fillColor;

                animationList.Add(new ColorAnimation(lastAnimationTarget, firstColor));

                _animations = animationList.AsEnumerable();
            }
        }
    }
}
