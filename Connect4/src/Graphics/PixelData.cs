using System.Numerics;
using System.Drawing;

namespace Connect4.src.Graphics
{
    internal struct PixelData
    {
        public Vector2 PixelPosition;
        public PixelType PixelType;
        public Color PixelColor;

        public PixelData(Vector2 pixelPosition, PixelType pixelType, Color pixelColor)
        {
            PixelPosition = pixelPosition;
            PixelType = pixelType;
            PixelColor = pixelColor;
        }
    }
}
