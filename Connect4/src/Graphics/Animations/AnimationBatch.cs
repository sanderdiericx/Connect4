using Connect4.src.Graphics.Animations;
using System.Collections.Generic;
using System.Linq;

namespace Connect4.src.Graphics.Sprites
{
    internal class AnimationBatch
    {
        internal readonly List<Animation> _animations;
        internal List<(AnimationChain, int)> _animationChains;

        internal AnimationBatch()
        {
            _animations = new List<Animation>();
            _animationChains = new List<(AnimationChain, int)>();
        }

        internal void AddAnimationChain(AnimationChain animationChain)
        {
            // Chains should always start at animationIndex 0
            _animationChains.Add((animationChain, 0));
        }

        // Checks every animation chain to see which need to move on to the next animation
        internal void UpdateAnimationChains()
        {
            for (int i = 0; i < _animationChains.Count; i++)
            {
                AnimationChain animationChain = _animationChains[i].Item1;
                int currentAnimationIndex = _animationChains[i].Item2;
                Animation currentAnimation = animationChain._animations.ElementAt(currentAnimationIndex);

                // Check if the current animation is done
                if (currentAnimation._animationDone)
                {
                    bool animationChainIsFinished = currentAnimationIndex + 1 >= animationChain._animations.Count();

                    // If the animation chain is finished, remove it from the saved animationChains
                    if (animationChainIsFinished && !animationChain._endlessAnimation)
                    {
                        _animationChains.RemoveAt(i);
                    }
                    else if (animationChainIsFinished && animationChain._endlessAnimation) // if it is finished and is endless, restart it from the first index
                    {
                        ResetAnimationChain(animationChain, currentAnimationIndex, i);
                    }
                    else // If the animation chain is not finished, start the next animation
                    {
                        NextAnimation(animationChain, currentAnimationIndex, i);
                    }
                }
            }
        }

        // Animates all normal animations
        internal void Animate()
        {
            for (int i = _animations.Count - 1; i >= 0; i--)
            {
                _animations[i].AnimateSprite();

                if (_animations[i]._animationDone) // If the animation is done, remove the reference from the list
                {
                    _animations.RemoveAt(i);
                }
            }
        }

        private void NextAnimation(AnimationChain animationChain, int currentAnimationIndex, int animationChainIndex)
        {
            currentAnimationIndex++;
            _animationChains[animationChainIndex] = (animationChain, currentAnimationIndex);

            _animations.Add(animationChain._animations.ElementAt(currentAnimationIndex));
        }

        private void ResetAnimationChain(AnimationChain animationChain, int currentAnimationIndex, int animationChainIndex)
        {
            currentAnimationIndex = 0;
            _animationChains[animationChainIndex] = (animationChain, currentAnimationIndex);

            // Reset all animations in the chain
            foreach (var animation in animationChain._animations)
            {
                animation.Reset();
            }

            // Start the first animation in the chain
            _animations.Add(animationChain._animations.ElementAt(currentAnimationIndex));
        }
    }
}
