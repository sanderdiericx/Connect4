namespace Connect4.src.Graphics.Sprites
{
    internal class Circle : Sprite
    {
        internal readonly int _iterationCount = 2000;
        internal float _radius;

        internal Circle(SpriteView spriteView, float radius) : base(spriteView)
        {
            _radius = radius;
        }

        internal override void Initialize()
        {
            base.Initialize();

            pixels = Rasturizer.ComputeCirclePixels(this);
        }
    }
}
