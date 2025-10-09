using Connect4.src.Graphics.Sprites;
using System.Drawing;
using System.Numerics;

namespace Connect4.src.Game
{
    internal class Indicator
    {
        internal Triangle _triangle;
        private int[] _rectangleCenterPositions;
        private Grid _grid;

        internal Indicator(Grid grid, GridLayout _gridLayout, int size, Color borderColor, Color fillColor, int borderSize)
        {
            _rectangleCenterPositions = grid._rectangleCenterPositions;
            _grid = grid;

            SpriteView spriteView = new SpriteView(_rectangleCenterPositions[0] - (size / 2), _gridLayout._gap * 1.1f, borderColor, fillColor, borderSize);
            _triangle = new Triangle(spriteView, size, size);
            _triangle.Initialize();
        }


        internal void SetFillColor(Color fillColor)
        {
            _triangle.SetFillColor(fillColor);
        }

        // Moves the indicator to the closest columns center x position
        internal void UpdatePosition()
        {
            _triangle.SetPosition(new Vector2(_rectangleCenterPositions[_grid.GetClosestIndex()] - (_triangle._baseLength / 2), _triangle._yPosition));
        }
    }
}
