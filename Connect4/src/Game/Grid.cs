using Connect4.src.Graphics.Sprites;
using Connect4.src.Graphics;

namespace Connect4.src.Game
{
    internal class Grid
    {
        private GridLayout _gridLayout;

        public Sprite[,] GameGrid;

        public Grid(GridLayout gridLayout)
        {
            _gridLayout = gridLayout;

            GenerateGameGridRectangles();
        }

        private void GenerateGameGridRectangles()
        {
            GameGrid = new Rectangle[_gridLayout.Columns, _gridLayout.Rows];

            int rectangleWidth = (GraphicsEngine.WindowWidth - (_gridLayout.Padding * 2) - (_gridLayout.Offset * _gridLayout.Columns)) / _gridLayout.Columns;
            int rectangleHeight = (GraphicsEngine.WindowHeight - _gridLayout.Gap - (_gridLayout.Padding * 2) - (_gridLayout.Offset * _gridLayout.Rows)) / _gridLayout.Rows;

            // Loop through and create rectangles based off the gridLayout
            for (int col = 0; col < _gridLayout.Columns; col++)
            {
                int x = _gridLayout.Padding + col * (rectangleWidth + _gridLayout.Offset);

                for (int row = 0; row < _gridLayout.Rows; row++)
                {
                    int y = _gridLayout.Padding + _gridLayout.Gap + row * (rectangleHeight + _gridLayout.Offset);

                    Rectangle rectangle = new Rectangle(new SpriteView(x, y, _gridLayout.BorderColor, _gridLayout.FillColor, _gridLayout.BorderSize), rectangleWidth, rectangleHeight);
                    rectangle.Initialize();
                    rectangle.HasBorder = _gridLayout.HasBorder;
                    rectangle.IsFilled = _gridLayout.IsFilled;

                    GameGrid[col, row] = rectangle;
                }
            }
        }
    }
}
