using System.Diagnostics.Eventing.Reader;
using System.Drawing;

namespace Connect4.src.Graphics.Sprites
{
    internal class Circle : Sprite
    {
        public readonly int IterationCount = 2000;
        public float Radius;

        public Circle(SpriteView spriteView, float radius) : base(spriteView)
        {
            Radius = radius;
        }

        public override void Initialize()
        {
            base.Initialize();

            pixels = Rasturizer.ComputeCirclePixels(this);
        }
    }
}
