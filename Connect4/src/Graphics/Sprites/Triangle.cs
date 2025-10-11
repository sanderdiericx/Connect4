namespace Connect4.src.Graphics.Sprites
{
    internal class Triangle : Sprite
    {
        internal int _baseLength;
        internal int _height;

        internal Triangle(SpriteView spriteView, int baseLength, int height) : base(spriteView)
        {
            _baseLength = baseLength;
            _height = height;
        }

        internal override void Initialize()
        {
            base.Initialize();

            _pixels = Rasturizer.ComputeTrianglePixels(this);
        }
    }
}
