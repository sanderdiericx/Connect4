using System.Drawing;

namespace Connect4.src.Graphics.Sprites
{
    internal class Rectangle : Sprite
    {
        public int Width;
        public int Height;

        public Rectangle(SpriteView spriteView, int width, int height) : base(spriteView)
        {
            Width = width;
            Height = height;
        }

        public override void Initialize()
        {
            base.Initialize();

            pixels = Rasturizer.ComputeRectanglePixels(this);
        }
    }
}
