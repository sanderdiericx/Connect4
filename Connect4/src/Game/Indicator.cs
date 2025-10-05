using Connect4.src.Graphics;
using Connect4.src.Graphics.Sprites;
using System;
using System.Drawing;
using System.Numerics;

namespace Connect4.src.Game
{
    internal class Indicator
    {
        private GridLayout _gridLayout;
        private int[] _rectangleCenterPositions;

        internal Triangle _triangle;

        internal Indicator(GridLayout gridLayout, int size, Color borderColor, Color fillColor, int borderSize)
        {
            _gridLayout = gridLayout;

            // Fill an array with each x position for each column in the grid, this way we can snap the indicator above it
            _rectangleCenterPositions = new int[gridLayout._columns];

            for (int col = 0; col < gridLayout._columns; col++)
            {
                // Calculate x position based off grid layout paramaters
                int rectangleWidth = (GraphicsEngine._windowWidth - (_gridLayout._padding * 2) - (_gridLayout._offset * _gridLayout._columns)) / _gridLayout._columns;
                int x = (_gridLayout._padding + col * (rectangleWidth + _gridLayout._offset)) + rectangleWidth / 2;

                _rectangleCenterPositions[col] = x;
            }

            SpriteView spriteView = new SpriteView(_rectangleCenterPositions[0] - (size / 2), _gridLayout._gap / 1.5f, borderColor, fillColor, borderSize);
            _triangle = new Triangle(spriteView, size, size);
            _triangle.Initialize();            
        }

        internal void UpdatePosition(int mouseX)
        {
            // Find the closest center position to the mouse
            int closestIndex = 0;
            int closestDistance = Math.Abs(mouseX - _rectangleCenterPositions[0]);

            for (int i = 0; i <_rectangleCenterPositions.Length; i++)
            {
                int distance = Math.Abs(mouseX - _rectangleCenterPositions[i]);

                if (distance < closestDistance)
                {
                    closestDistance = Math.Abs(distance);
                    closestIndex = i;
                }
            }

            _triangle.SetPosition(new Vector2(_rectangleCenterPositions[closestIndex] - (_triangle._baseLength / 2), _triangle._yPosition));
        }
    }
}
