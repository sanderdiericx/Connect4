using Connect4.src.Graphics.Sprites;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Connect4.src.Graphics
{
    /// <summary>
    /// The rasturizer class handles computing sprites boundary and filling pixels, typically these methods are only called once in a sprites lifetime (at Initialize),
    /// but they can be called again to compute pixels with new parameters (ex: radius)
    /// </summary>
    internal static class Rasturizer
    {
        internal static List<PixelData> ComputeCirclePixels(Circle circle)
        {
            List<PixelData> pixels = new List<PixelData>();

            // Compute border pixels
            float angleStep = 2 * (float)Math.PI / circle._iterationCount;
            float currentRadius = circle._radius;

            for (int i = 0; i < circle._borderSize; i++)
            {
                currentRadius--;

                for (int j = 0; j < circle._iterationCount; j++)
                {
                    float radians = j * angleStep;

                    int x = (int)Math.Round(circle._xPosition + currentRadius * Math.Cos(radians));
                    int y = (int)Math.Round(circle._yPosition + currentRadius * Math.Sin(radians));

                    pixels.Add(new PixelData(new Vector2(x, y), PixelType.Border, circle._borderColor));
                }
            }

            // Loop over each y position in the triangle and compute fill pixel coordinates
            // (x - h)^2 + (y - k)^2 = r^2 solve for y to find xstart and xend
            for (int y = (int)(circle._yPosition - circle._radius); y <= circle._yPosition + circle._radius; y++)
            {
                float equation = (float)Math.Sqrt((circle._radius * circle._radius) - Math.Pow(y - circle._yPosition, 2));

                int xstart = (int)Math.Round(circle._xPosition - equation);
                int xend = (int)Math.Round(circle._xPosition + equation);

                // Here we use circle._borderSize / 2 to avoid overdrawing the border pixels
                for (int x = xstart + circle._borderSize / 2; x <= xend - circle._borderSize / 2; x++)
                {
                    pixels.Add(new PixelData(new Vector2(x, y), PixelType.Filling, circle._fillColor));
                }
            }

            return pixels;
        }

        internal static List<PixelData> ComputeRectanglePixels(Rectangle rectangle)
        {
            List<PixelData> pixels = new List<PixelData>();

            // Compute border pixels
            for (int x = 0; x <= rectangle._width; x++)
            {
                for (int i = 0; i < rectangle._borderSize; i++)
                {
                    pixels.Add(new PixelData(new Vector2(rectangle._xPosition + x, rectangle._yPosition + i), PixelType.Border, rectangle._borderColor));
                }
            }

            for (int x = 0; x <= rectangle._width; x++)
            {
                for (int i = 0; i < rectangle._borderSize; i++)
                {
                    pixels.Add(new PixelData(new Vector2(rectangle._xPosition + x, rectangle._yPosition + rectangle._height - i), PixelType.Border, rectangle._borderColor));
                }
            }

            for (int y = 1; y <= rectangle._height - 1; y++)
            {
                for (int i = 0; i < rectangle._borderSize; i++)
                {
                    pixels.Add(new PixelData(new Vector2(rectangle._xPosition + i, rectangle._yPosition + y), PixelType.Border, rectangle._borderColor));
                }
            }

            for (int y = 1; y <= rectangle._height - 1; y++)
            {
                for (int i = 0; i < rectangle._borderSize; i++)
                {
                    pixels.Add(new PixelData(new Vector2(rectangle._xPosition + rectangle._width - i, rectangle._yPosition + y), PixelType.Border, rectangle._borderColor));
                }
            }

            // Compute fill pixels
            for (int y = (int)rectangle._yPosition; y <= rectangle._yPosition + rectangle._height; y++)
            {
                for (int x = (int)rectangle._xPosition; x <= rectangle._xPosition + rectangle._width; x++)
                {
                    pixels.Add(new PixelData(new Vector2(x, y), PixelType.Filling, rectangle._fillColor));
                }
            }

            return pixels;
        }

        // NOTE: this is by far not the best way to rasterize a triangle, but it works for our simple use case
        internal static List<PixelData> ComputeTrianglePixels(Triangle triangle)
        {
            List<PixelData> pixels = new List<PixelData>();

            // Compute border pixels
            Vector2 corner1 = new Vector2(triangle._xPosition, triangle._yPosition);
            Vector2 corner2 = new Vector2(triangle._xPosition + triangle._baseLength, triangle._yPosition);
            Vector2 corner3 = new Vector2(triangle._xPosition + (triangle._baseLength / 2), triangle._yPosition + triangle._height);

            Vector2 currentCorner1 = corner1;
            Vector2 currentCorner2 = corner2;
            Vector2 currentCorner3 = corner3;

            for (int i = 0; i < triangle._borderSize; i++)
            {
                if (i < triangle._borderSize / 2)
                {
                    ComputeLineVectors(triangle, pixels, currentCorner1, currentCorner2);
                }

                ComputeLineVectors(triangle, pixels, currentCorner1, currentCorner3);
                ComputeLineVectors(triangle, pixels, currentCorner3, currentCorner2);

                currentCorner1.X++;
                currentCorner1.Y++;

                currentCorner2.X--;
                currentCorner2.Y++;

                currentCorner3.Y--;
            }

            // Loop over each y position in the triangle and compute fill pixel coordinates
            int minY = (int)corner1.Y;
            int maxY = (int)corner3.Y;

            for (int y = minY + triangle._borderSize / 2; y <= maxY - triangle._borderSize / 2; y++)
            {
                // Compute how far along the y axis we are
                float t = (y - corner1.Y) / (corner3.Y - corner1.Y);

                // Interpolate to find the left and right x coordinates at this assumed y position
                float leftX = Lerp(corner1.X, corner3.X, t);
                float rightX = Lerp(corner2.X, corner3.X, t);

                if (y == minY)
                {
                    leftX = corner1.X;
                    rightX = corner2.X;
                }

                for (int x = (int)leftX + 1; x <= (int)rightX; x++)
                {
                    pixels.Add(new PixelData(new Vector2(x, y), PixelType.Filling, triangle._fillColor));
                }
            }


            return pixels;
        }

        private static float Lerp(float a, float b, float t)
        {
            return a + t * (b - a);
        }

        // Use DDA line algorithm to determine triangle lines, this algorithm assumes x2 > x1
        private static void ComputeLineVectors(Triangle triangle, List<PixelData> pixels, Vector2 point1, Vector2 point2)
        {
            float dx = point2.X - point1.X;
            float dy = point2.Y - point1.Y;

            float slope = dy / dx;

            for (float x = point1.X; x <= point2.X; x++)
            {
                float y = slope * (x - point1.X) + point1.Y;

                pixels.Add(new PixelData(new Vector2(x, y), PixelType.Border, triangle._borderColor));
            }
        }
    }
}