using Connect4.src.Graphics.Sprites;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Connect4.src.Graphics
{
    internal static class Rasturizer
    {
        internal static List<PixelData> ComputeCirclePixels(Circle circle)
        {
            List<PixelData> pixels = new List<PixelData>();

            // Compute border pixels
            float angleStep = 2 * (float)Math.PI / circle.IterationCount;
            float currentRadius = circle.Radius;

            for (int i = 0; i < circle.BorderSize; i++)
            {
                currentRadius--;

                for (int j = 0; j < circle.IterationCount; j++)
                {
                    float radians = j * angleStep;

                    int x = (int)Math.Round(circle.XPosition + currentRadius * Math.Cos(radians));
                    int y = (int)Math.Round(circle.YPosition + currentRadius * Math.Sin(radians));

                    pixels.Add(new PixelData(new Vector2(x, y), PixelType.Border, circle.BorderColor));
                }
            }

            // Loop over the first half of the triangle and compute fill pixel coordinates
            // (x - h)^2 + (y - k)^2 = r^2 solve for y to find xstart and xend
            for (int y = (int)(circle.YPosition - circle.Radius); y <= circle.YPosition + circle.Radius; y++)
            {
                float equation = (float)Math.Sqrt((circle.Radius * circle.Radius) - Math.Pow(y - circle.YPosition, 2));

                int xstart = (int)Math.Round(circle.XPosition - equation);
                int xend = (int)Math.Round(circle.XPosition + equation);

                for (int x = xstart; x <= xend; x++)
                {
                    pixels.Add(new PixelData(new Vector2(x, y), PixelType.Filling, circle.FillColor));
                }
            }

            return pixels;
        }

        internal static List<PixelData> ComputeRectanglePixels(Rectangle rectangle)
        {
            List<PixelData> pixels = new List<PixelData>();

            // Compute border pixels
            for (int x = 0; x <= rectangle.Width; x++)
            {
                for (int i = 0; i < rectangle.BorderSize; i++)
                {
                    pixels.Add(new PixelData(new Vector2(rectangle.XPosition + x, rectangle.YPosition + i), PixelType.Border, rectangle.BorderColor));
                }
            }

            for (int x = 0; x <= rectangle.Width; x++)
            {
                for (int i = 0; i < rectangle.BorderSize; i++)
                {
                    pixels.Add(new PixelData(new Vector2(rectangle.XPosition + x, rectangle.YPosition + rectangle.Height - i), PixelType.Border, rectangle.BorderColor));
                }
            }

            for (int y = 1; y <= rectangle.Height - 1; y++)
            {
                for (int i = 0; i < rectangle.BorderSize; i++)
                {
                    pixels.Add(new PixelData(new Vector2(rectangle.XPosition + i, rectangle.YPosition + y), PixelType.Border, rectangle.BorderColor));
                }
            }

            for (int y = 1; y <= rectangle.Height - 1; y++)
            {
                for (int i = 0; i < rectangle.BorderSize; i++)
                {
                    pixels.Add(new PixelData(new Vector2(rectangle.XPosition + rectangle.Width - i, rectangle.YPosition + y), PixelType.Border, rectangle.BorderColor));
                }
            }

            // Compute fill pixels
            for (int y = (int)rectangle.YPosition; y <= rectangle.YPosition + rectangle.Height; y++)
            {
                for (int x = (int)rectangle.XPosition; x <= rectangle.XPosition + rectangle.Width; x++)
                {
                    pixels.Add(new PixelData(new Vector2(x, y), PixelType.Filling, rectangle.FillColor));
                }
            }

            return pixels;
        }

        /*
        internal static List<Vector2> ComputeTriangleBorderPositions(float xPosition, float yPosition, int baseLength, int height)
        {
            List<Vector2> borderPositions = new List<Vector2>();

            Vector2 corner1 = new Vector2(xPosition, yPosition);
            Vector2 corner2 = new Vector2(xPosition + baseLength, yPosition);
            Vector2 corner3 = new Vector2((xPosition + baseLength) / 2, yPosition + height);

            
            float dx = corner1.X - corner2.X;
            float dy = corner1.Y - corner2.Y;

            float slope = dy / dx;

            for (float x = corner1.X; x < corner2.X; x++)
            {

            }

        }

        // Use DDA line algorithm to determine triangle lines, this algorithm assumes x2 > x1
        private static List<Vector2> ComputeLineVectors(Vector2 point1, Vector2 point2)
        {

        }

        */
    }
}
