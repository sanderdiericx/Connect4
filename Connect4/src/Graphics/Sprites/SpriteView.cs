using System.Drawing;

namespace Connect4.src.Graphics.Sprites
{
    internal struct SpriteView
    {
        internal float _x;
        internal float _y;
        internal Color _borderColor;
        internal Color _fillColor;
        internal int _borderSize;

        internal SpriteView(float x, float y, Color borderColor, Color fillColor, int borderSize)
        {
            _x = x;
            _y = y;
            _borderColor = borderColor;
            _fillColor = fillColor;
            _borderSize = borderSize;
        }
    }
}
