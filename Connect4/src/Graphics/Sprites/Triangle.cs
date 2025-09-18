using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.src.Graphics.Sprites
{
    internal class Triangle : Sprite
    {
        internal int baseLength;
        internal int height;

        internal Triangle(SpriteView spriteView, int baseLength, int height) : base(spriteView)
        {
            this.baseLength = baseLength;
            this.height = height;
        }

        internal override void Initialize()
        {
            base.Initialize();



        }
    }
}
