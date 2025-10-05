using Connect4.src.Graphics;
using Connect4.src.Graphics.Sprites;
using Connect4.src.Logs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Rectangle = Connect4.src.Graphics.Sprites.Rectangle;

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

        internal void SetGridCell(int col, int row, CellType cellType, Func<float, float> easingFunction, float animationSpeed)
        {
            if (row >= _gridLayout._rows || col >= _gridLayout._columns)
            {
                Logger.LogWarning("Selected grid cell is outside grid boundaries!");
                return;
            }

            _gridCells[col, row]._cellType = cellType;

            if (cellType != CellType.Empty)
            {
                Rectangle cellRectangle = _gridCells[col, row]._cellRectangle;

                int markerXPosition = (int)cellRectangle._xPosition + (cellRectangle._width / 2);
                int markerYPosition = -100; // Spawn offscreen

                // Create the marker at the correct position
                SpriteView spriteView = new SpriteView(markerXPosition, markerYPosition, Color.Black, cellType == CellType.Yellow ? Color.Gold : Color.Firebrick, 8);
                Circle marker = new Circle(spriteView, cellRectangle._width / cellRectangle._height * (cellRectangle._height - cellRectangle._height / 6) / 2);
                marker.Initialize();

                // Create an animation to drop the marker onto the grid
                Animation animation = new Animation(marker, new Vector2(markerXPosition, (int)cellRectangle._yPosition + (cellRectangle._height / 2)), animationSpeed, easingFunction);
                GraphicsEngine.StartAnimation(animation);

                // Asign the newly created marker to the correct spot in the grid
                _gridCells[col, row]._cellMarker = marker;
            }
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
