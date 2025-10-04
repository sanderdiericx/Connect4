namespace Connect4.src.Graphics.Sprites
{
    internal class Rectangle : Sprite
    {
        internal int _width;
        internal int _height;

        internal Rectangle(SpriteView spriteView, int width, int height) : base(spriteView)
        {
            _width = width;
            _height = height;
        }

        internal override void Initialize()
        {
            base.Initialize();

            pixels = Rasturizer.ComputeRectanglePixels(this);
        }
    }
}
