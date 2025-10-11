using System.Collections.Generic;

namespace Connect4.src.Graphics.Sprites
{
    // Animationbatch holds all animations that need to be animated over the course of many frames
    internal class AnimationBatch
    {
        internal readonly List<Animation> _animations;

        internal AnimationBatch()
        {
            _animations = new List<Animation>();
        }

        internal void ClearAnimations()
        {
            _animations.Clear();
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
