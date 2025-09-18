using System.Diagnostics.Eventing.Reader;
using System.Drawing;

namespace Connect4.src.Graphics.Sprites
{
    internal class Circle : Sprite
    {
        private const int ITERATION_COUNT = 2000;
        private float _radius;

        public Circle(SpriteView spriteView, float radius) : base(spriteView)
        {
            _radius = radius;
        }

        public override void Initialize()
        {
            base.Initialize();

            borderPositions = Rasturizer.ComputeCircleBorderPositions(xPosition, yPosition, _radius, borderSize, ITERATION_COUNT);
            pixelPositions = Rasturizer.ComputeCirclePixelPositions(xPosition, yPosition, _radius);
        }
    }
}
