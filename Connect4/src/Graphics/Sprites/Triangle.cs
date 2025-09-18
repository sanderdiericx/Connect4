using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.src.Graphics.Sprites
{
    internal class Triangle : Sprite
    {
        private int _baseLength;
        private int _height;

        public Triangle(SpriteView spriteView, int baseLength, int height) : base(spriteView)
        {
            _baseLength = baseLength;
            _height = height;
        }

        public override void Initialize()
        {
            base.Initialize();



        }
    }
}
