using Connect4.src.Graphics.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Connect4.src.Graphics
{
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

            // Loop over the first half of the triangle and compute fill pixel coordinates
            // (x - h)^2 + (y - k)^2 = r^2 solve for y to find xstart and xend
            for (int y = (int)(circle._yPosition - circle._radius); y <= circle._yPosition + circle._radius; y++)
            {
                float equation = (float)Math.Sqrt((circle._radius * circle._radius) - Math.Pow(y - circle._yPosition, 2));

                int xstart = (int)Math.Round(circle._xPosition - equation);
                int xend = (int)Math.Round(circle._xPosition + equation);

                for (int x = xstart; x <= xend; x++)
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

        // TO DO, USE PROPER NAMING CONVENTIONS FOR ALL INTERNAL VARIABLES

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

            return pixels;
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