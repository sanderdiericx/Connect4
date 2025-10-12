using Connect4.src.Graphics.Animations;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Connect4.src.Graphics.Sprites
{
    // Animationbatch holds all animations that need to be animated over the course of many frames
    internal class AnimationBatch
    {
        internal readonly List<Animation> _animations;

        // A dictionary containing an animation chain, along with the index of the current animation that is running
        internal List<(AnimationChain, int)> _animationChains;

        internal AnimationBatch()
        {
            _animations = new List<Animation>();
            _animationChains = new List<(AnimationChain, int)>();
        }

        internal void ClearAnimations()
        {
            _animations.Clear();
        }

        internal void AddAnimationChain(AnimationChain animationChain)
        {
            _animationChains.Add((animationChain, 0)); // Always start at animation index 0
        }

        // Checks every animation chain to see which need to move on to the next animation
        internal void UpdateAnimationChains()
        {
            for (int i = 0; i < _animationChains.Count; i++)
            {
                AnimationChain animationChain = _animationChains[i].Item1;
                int currentAnimationIndex = _animationChains[i].Item2;

                // Check if the current animation is done
                if (animationChain._animations.ElementAt(currentAnimationIndex)._animationDone)
                {
                    // If the animation chain is finished, remove it from the saved animationChains
                    if (currentAnimationIndex + 1 >= animationChain._animations.Count() && !animationChain._endlessAnimation)
                    {
                        _animationChains.RemoveAt(i);
                    }
                    else if (currentAnimationIndex + 1 >= animationChain._animations.Count() && animationChain._endlessAnimation) // if it is finished and is endless, restart it from the first index
                    {
                        currentAnimationIndex = 0;
                        _animationChains[i] = (animationChain, currentAnimationIndex);

                        // Reset all animations in the chain
                        foreach (var animation in animationChain._animations)
                        {
                            animation.Reset();
                        }

                        _animations.Add(animationChain._animations.ElementAt(currentAnimationIndex));
                    }
                    else // If it is not finished, start the next animation
                    {
                        currentAnimationIndex++;
                        _animationChains[i] = (animationChain, currentAnimationIndex);

                        _animations.Add(animationChain._animations.ElementAt(currentAnimationIndex));
                    }
                }
            }
        }

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
    }
}
