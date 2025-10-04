using Connect4.src.Graphics;
using Connect4.src.Graphics.Sprites;
using Connect4.src.Logs;
using System;
using System.Drawing;
using Rectangle = Connect4.src.Graphics.Sprites.Rectangle;

namespace Connect4.src.Game
{
    internal static class ConnectFour
    {
        // Returns the marker that corresponds to the set cell, returns null if an empty cellType is chosen
        internal static void SetGridCell(Grid grid, int col, int row, CellType cellType, Func<float, float> easingFunction, float animationSpeed)
        {
            if (row >= grid._gridLayout._rows || col >= grid._gridLayout._columns)
            {
                Logger.LogWarning("Selected grid cell is outside grid boundaries!");
                return;
            }

            grid._gridCells[col, row]._cellType = cellType;

            if (cellType != CellType.Empty)
            {
                Rectangle cellRectangle = grid._gridCells[col, row]._cellRectangle;

                int markerXPosition = (int)cellRectangle._xPosition + (cellRectangle._width / 2);
                int markerYPosition = grid._gridLayout._gap;

                // Create the marker at the correct position
                SpriteView spriteView = new SpriteView(markerXPosition, markerYPosition, Color.Black, cellType == CellType.Yellow ? Color.Yellow : Color.Red, 8);
                Circle marker = new Circle(spriteView, cellRectangle._width / cellRectangle._height * (cellRectangle._height - cellRectangle._height / 6) / 2);
                marker.Initialize();

                // Create an animation to drop the marker onto the grid
                Animation animation = new Animation(marker, (int)cellRectangle._yPosition + (cellRectangle._height / 2), animationSpeed, easingFunction);
                GraphicsEngine.StartAnimation(animation);

                // Asign the newly created marker to the correct spot in the grid
                grid._gridCells[col, row]._cellMarker = marker;
            }
        }
    }
}
