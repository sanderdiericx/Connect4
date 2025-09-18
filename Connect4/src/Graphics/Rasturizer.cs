using System;
using System.Collections.Generic;
using System.Numerics;

namespace Connect4.src.Graphics
{
    internal static class Rasturizer
    {
        public static List<Vector2> ComputeCircleBorderPositions(float xPosition, float yPosition, float radius, int borderSize, int iterationCount)
        {
            // Create the border positions for a circle
            List<Vector2> borderPositions = new List<Vector2>();

            float angleStep = 2 * (float)Math.PI / iterationCount;
            float currentRadius = radius;

            for (int i = 0; i < borderSize; i++)
            {
                currentRadius--;

                for (int j = 0; j < iterationCount; j++)
                {
                    float radians = j * angleStep;

                    int x = (int) Math.Round(xPosition + currentRadius * Math.Cos(radians));
                    int y = (int) Math.Round(yPosition + currentRadius * Math.Sin(radians));

                    borderPositions.Add(new Vector2(x, y));
                }
            }

            return borderPositions;
        }

        public static List<Vector2> ComputeCirclePixelPositions(float xPosition, float yPosition, float radius)
        {
            List<Vector2> pixelPositions = new List<Vector2>();

            // Loop over the first half of the triangle and compute fill pixel coordinates
            // (x - h)^2 + (y - k)^2 = r^2 solve for y to find xstart and xend
            for (int y = (int)(yPosition - radius); y <= yPosition + radius; y++)
            {
                float equation = (float)Math.Sqrt((radius * radius) - Math.Pow(y - yPosition, 2));

                int xstart = (int)Math.Round(xPosition - equation);
                int xend = (int)Math.Round(xPosition + equation);

                for (int x = xstart; x <= xend; x++)
                {
                    pixelPositions.Add(new Vector2(x, y));
                }
            }

            return pixelPositions;
        }

        public static List<Vector2> ComputeRectangleBorderPositions(float xPosition, float yPosition, int width, int height, int bordersize)
        {
            List<Vector2> borderPositions = new List<Vector2>();

            for (int x = 0; x <= width; x++)
            {
                for (int i = 0; i < bordersize; i++)
                {
                    borderPositions.Add(new Vector2(xPosition + x, yPosition + i));
                }
            }

            for (int x = 0; x <= width; x++)
            {
                for (int i = 0; i < bordersize; i++)
                {
                    borderPositions.Add(new Vector2(xPosition + x, yPosition + height - i));
                }
            }

            for (int y = 1; y <= height - 1; y++)
            {
                for (int i = 0; i < bordersize; i++)
                {
                    borderPositions.Add(new Vector2(xPosition + i, yPosition + y));
                }   
            }

            for (int y = 1; y <= height - 1; y++)
            {
                for (int i = 0; i < bordersize; i++)
                {
                    borderPositions.Add(new Vector2(xPosition + width - i, yPosition + y));
                }
            }

            return borderPositions;
        }

        public static List<Vector2> ComputeRectanglePixelPositions(float xPosition, float yPosition, int width, int height)
        {
            List<Vector2> pixelPositions = new List<Vector2>();

            for (int y = (int)yPosition; y <= yPosition + height; y++)
            {
                for (int x = (int)xPosition; x <= xPosition + width; x++)
                {
                    pixelPositions.Add(new Vector2(x, y));
                }
            }

            return pixelPositions;
        }

        /*
        public static List<Vector2> ComputeTriangleBorderPositions(float xPosition, float yPosition, int baseLength, int height)
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
