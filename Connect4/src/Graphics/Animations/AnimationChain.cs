using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.src.Graphics.Animations
{
    internal class AnimationChain
    {
        internal IEnumerable<Animation> _animations;

        internal AnimationChain(IEnumerable<Animation> animations)
        {
            _animations = animations;
        }
    }
}
