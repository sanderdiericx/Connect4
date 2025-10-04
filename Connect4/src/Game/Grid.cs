using Connect4.src.Graphics;
using Connect4.src.Graphics.Sprites;
using System.Collections.Generic;

namespace Connect4.src.Game
{
    internal class Grid
    {
        internal GridLayout _gridLayout;

        internal GridCell[,] _gridCells;

        internal Grid(GridLayout gridLayout)
        {
            _gridLayout = gridLayout;

            GenerateGameGridRectangles();
        }

        internal List<Sprite> GetSprites()
        {
            List<Sprite> sprites = new List<Sprite>();

            // Make sure cellMarkers are rendered before cellRectangles
            foreach (GridCell gridCell in _gridCells)
            {
                sprites.Add(gridCell._cellMarker);
            }

            foreach (GridCell gridCell in _gridCells)
            {
                sprites.Add(gridCell._cellRectangle);
            }

            return sprites;
        }

        private void GenerateGameGridRectangles()
        {
            _gridCells = new GridCell[_gridLayout._columns, _gridLayout._rows];

            int rectangleWidth = (GraphicsEngine._windowWidth - (_gridLayout._padding * 2) - (_gridLayout._offset * _gridLayout._columns)) / _gridLayout._columns;
            int rectangleHeight = (GraphicsEngine._windowHeight - _gridLayout._gap - (_gridLayout._padding * 2) - (_gridLayout._offset * _gridLayout._rows)) / _gridLayout._rows;

            // Loop through and create rectangles based off the gridLayout
            for (int col = 0; col < _gridLayout._columns; col++)
            {
                int x = _gridLayout._padding + col * (rectangleWidth + _gridLayout._offset);

                for (int row = 0; row < _gridLayout._rows; row++)
                {
                    int y = _gridLayout._padding + _gridLayout._gap + row * (rectangleHeight + _gridLayout._offset);

                    Rectangle rectangle = new Rectangle(new SpriteView(x, y, _gridLayout._borderColor, _gridLayout._fillColor, _gridLayout._borderSize), rectangleWidth, rectangleHeight);
                    rectangle.Initialize();
                    rectangle._hasBorder = _gridLayout._hasBorder;
                    rectangle._isFilled = _gridLayout._isFilled;

                    _gridCells[col, row] = new GridCell(rectangle, null, CellType.Empty);
                }
            }
        }
    }
}
