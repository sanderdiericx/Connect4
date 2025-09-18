using System.Drawing;

namespace Connect4.src.Graphics.Sprites
{
    internal struct SpriteView
    {
        internal float X;
        internal float Y;
        internal Color BorderColor;
        internal Color FillColor;
        internal int BorderSize;

        internal SpriteView(float x, float y, Color borderColor, Color fillColor, int borderSize)
        {
            X = x;
            Y = y;
            BorderColor = borderColor;
            FillColor = fillColor;
            BorderSize = borderSize;
        }
    }
}
