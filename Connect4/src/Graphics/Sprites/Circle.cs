using System.Diagnostics.Eventing.Reader;
using System.Drawing;

namespace Connect4.src.Graphics.Sprites
{
    internal class Circle : Sprite
    {
        internal readonly int IterationCount = 2000;
        internal float Radius;

        internal Circle(SpriteView spriteView, float radius) : base(spriteView)
        {
            Radius = radius;
        }

        internal override void Initialize()
        {
            base.Initialize();

            pixels = Rasturizer.ComputeCirclePixels(this);
        }
    }
}
