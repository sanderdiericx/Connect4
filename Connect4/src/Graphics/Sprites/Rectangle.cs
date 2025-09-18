using System.Drawing;

namespace Connect4.src.Graphics.Sprites
{
    internal class Rectangle : Sprite
    {
        private int _width;
        private int _height;

        public Rectangle(SpriteView spriteView, int width, int height) : base(spriteView)
        {
            _width = width;
            _height = height;
        }

        public override void Initialize()
        {
            base.Initialize();

            borderPositions = Rasturizer.ComputeRectangleBorderPositions(xPosition, yPosition, _width, _height, borderSize);
            pixelPositions = Rasturizer.ComputeRectanglePixelPositions(xPosition, yPosition, _width, _height);
        }
    }
}
