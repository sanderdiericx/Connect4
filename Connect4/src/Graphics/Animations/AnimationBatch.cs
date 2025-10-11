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
                    if (currentAnimationIndex + 1 >= animationChain._animations.Count())
                    {
                        _animationChains.RemoveAt(i);
                    }
                    else // If it is not finished, start the next animation
                    {
                        currentAnimationIndex++;

                        _animations.Add(animationChain._animations.ElementAt(currentAnimationIndex));
                    }
                }
            }
        }

        internal void Animate()
        {
            for (int i = 0; i < _animations.Count; i++)
            {
                if (!_animations[i]._animationDone)
                {
                    _animations[i].AnimateSprite();
                }
                else // The animation is done, we can remove the reference to it from the list
                {
                    _animations.Remove(_animations[i]);
                }
            }
        }
    }
}
