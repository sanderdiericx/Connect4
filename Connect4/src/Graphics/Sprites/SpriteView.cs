using System.Drawing;

namespace Connect4.src.Graphics.Sprites
{
    internal struct SpriteView
    {
        public float X;
        public float Y;
        public Color BorderColor;
        public Color FillColor;
        public int BorderSize;

        public SpriteView(float x, float y, Color borderColor, Color fillColor, int borderSize)
        {
            X = x;
            Y = y;
            BorderColor = borderColor;
            FillColor = fillColor;
            BorderSize = borderSize;
        }
    }
}
