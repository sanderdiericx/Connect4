using System.Drawing;

namespace Connect4.src.Graphics.Sprites
{
    internal class Rectangle : Sprite
    {
        internal int Width;
        internal int Height;

        internal Rectangle(SpriteView spriteView, int width, int height) : base(spriteView)
        {
            Width = width;
            Height = height;
        }

        internal override void Initialize()
        {
            base.Initialize();

            pixels = Rasturizer.ComputeRectanglePixels(this);
        }
    }
}
