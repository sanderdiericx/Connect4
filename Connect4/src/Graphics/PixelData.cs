using System.Numerics;
using System.Drawing;

namespace Connect4.src.Graphics
{
    internal struct PixelData
    {
        internal Vector2 PixelPosition;
        internal PixelType PixelType;
        internal Color PixelColor;

        internal PixelData(Vector2 pixelPosition, PixelType pixelType, Color pixelColor)
        {
            PixelPosition = pixelPosition;
            PixelType = pixelType;
            PixelColor = pixelColor;
        }
    }
}
