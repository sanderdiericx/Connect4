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

        // TO DO, USE PROPER NAMING CONVENTIONS FOR ALL INTERNAL VARIABLES

        internal static List<PixelData> ComputeTrianglePixels(Triangle triangle)
        {
            List<PixelData> pixels = new List<PixelData>();

            Vector2 corner1 = new Vector2(triangle.XPosition, triangle.YPosition);
            Vector2 corner2 = new Vector2(triangle.XPosition + triangle.baseLength, triangle.YPosition);
            Vector2 corner3 = new Vector2((triangle.XPosition + triangle.baseLength) / 2, triangle.YPosition + triangle.height);

            ComputeLineVectors(triangle, pixels, corner1, corner2);
            ComputeLineVectors(triangle, pixels, corner1, corner3);
            ComputeLineVectors(triangle, pixels, corner3, corner2);

            

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

                pixels.Add(new PixelData(new Vector2(x, y), PixelType.Border, triangle.BorderColor));
            }
        }
    }
}