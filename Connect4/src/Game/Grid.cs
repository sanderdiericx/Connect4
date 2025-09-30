using Connect4.src.Graphics;
using Connect4.src.Graphics.Sprites;

namespace Connect4.src.Game
{
    internal class Grid
    {
        private GridLayout _gridLayout;

        internal GridCell[,] _grid;

        internal Grid(GridLayout gridLayout)
        {
            _gridLayout = gridLayout;

            GenerateGameGridRectangles();
        }

        private void GenerateGameGridRectangles()
        {
            _grid = new GridCell[_gridLayout._columns, _gridLayout._rows];

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

                    _grid[col, row] = new GridCell(rectangle, CellType.Empty);
                }
            }
        }
    }
}
