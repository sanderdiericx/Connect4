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
        internal int[] _rectangleCenterPositions;
        internal Vector2 _lastHighlight;
        internal Vector2 _lastMove;

        internal Grid(GridLayout gridLayout)
        {
            _gridLayout = gridLayout;

            _lastHighlight = Vector2.Zero;
            _lastMove = Vector2.Zero;

            GenerateGameGridRectangles();
            GenerateRectangleCenterPositions();
        }

        // Highlights the currently indicated cell
        internal void HighlightSelectedCell(Color highlightColor)
        {
            int col = GetClosestIndex();
            int row = FindFurthestCell(col);

            // Reset the last highlighted cell if the indicated cell changed
            if (_lastHighlight.X != col || _lastHighlight.Y != row)
            {
                _gridCells[(int)_lastHighlight.X, (int)_lastHighlight.Y]._cellRectangle.SetBorderColor(_gridLayout._borderColor);
            }

            if (row != -1)
            {
                _gridCells[col, row]._cellRectangle.SetBorderColor(highlightColor);

                _lastHighlight = new Vector2(col, row);
            }
        }


        // Converts the gridcell structure into a list of sprites
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

        // Loops through a column and find the furthest cell without a marker, returns -1 if no available cells were found
        internal int FindFurthestCell(int col)
        {
            bool cellFound = false;
            int rowIndex = -1;

            for (int row = _gridLayout._rows - 1; row >= 0; row--)
            {
                if (!cellFound && _gridCells[col, row]._cellType == CellType.Empty)
                {
                    cellFound = true;
                    rowIndex = row;
                }
            }

            return rowIndex;
        }


        // Returns the column index which the mouse is closest to
        internal int GetClosestIndex()
        {
            // Find the closest center position to the mouse
            int closestIndex = 0;
            int closestDistance = Math.Abs(GraphicsEngine._windowMousePosition.X - _rectangleCenterPositions[0]);

            for (int i = 0; i < _rectangleCenterPositions.Length; i++)
            {
                int distance = Math.Abs(GraphicsEngine._windowMousePosition.X - _rectangleCenterPositions[i]);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }

            return closestIndex;
        }

        // Fills a specified index in the grid with a game marker
        internal void SetGridCell(int col, int row, CellType cellType, Func<float, float> easingFunction, float animationSpeed)
        {
            if (row >= _gridLayout._rows || col >= _gridLayout._columns || row < 0 || col < 0)
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

                // Create a transformAnimation to drop the marker onto the grid
                AnimationTarget animationTarget = new AnimationTarget(marker, animationSpeed, easingFunction);
                TransformAnimation transformAnimation = new TransformAnimation(animationTarget, new Vector2(markerXPosition, (int)cellRectangle._yPosition + (cellRectangle._height / 2)));
                GraphicsEngine.StartAnimation(transformAnimation);

                // Asign the newly created marker to the correct spot in the grid
                _gridCells[col, row]._cellMarker = marker;

                // Save the last move
                _lastMove = new Vector2(col, row);
            }
        }

        // Fills an array with each x position for each column in the grid
        private void GenerateRectangleCenterPositions()
        {
            _rectangleCenterPositions = new int[_gridLayout._columns];

            for (int col = 0; col < _gridLayout._columns; col++)
            {
                // Calculate x position based off grid layout paramaters
                int rectangleWidth = (GraphicsEngine._windowWidth - (_gridLayout._padding * 2) - (_gridLayout._offset * _gridLayout._columns)) / _gridLayout._columns;
                int x = (_gridLayout._padding + col * (rectangleWidth + _gridLayout._offset)) + rectangleWidth / 2;

                _rectangleCenterPositions[col] = x;
            }
        }


        // Create the grid of rectangles
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
