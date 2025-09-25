using System.Numerics;
using System.Drawing;

namespace Connect4.src.Graphics
{
    internal class PixelData
    {
        internal Vector2 _pixelPosition;
        internal PixelType _pixelType;
        internal Color _pixelColor;

        internal PixelData(Vector2 pixelPosition, PixelType pixelType, Color pixelColor)
        {
            _pixelPosition = pixelPosition;
            _pixelType = pixelType;
            _pixelColor = pixelColor;
        }
    }
}
