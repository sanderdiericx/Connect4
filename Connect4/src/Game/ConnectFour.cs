using System.Drawing;
using Connect4.src.Graphics.Sprites;
using Rectangle = Connect4.src.Graphics.Sprites.Rectangle;

namespace Connect4.src.Game
{
    internal static class ConnectFour
    {
        // Returns the marker that corresponds to the set cell, returns null if an empty cellType is chosen
        internal static Circle SetGridCell(Grid grid, int row, int column, CellType cellType)
        {
            grid._grid[column, row]._cellType = cellType;
            Rectangle cellRectangle = grid._grid[column, row]._cellRectangle;

            // Create a marker at the correct coordinates with correct attributes in accordance with cellType
            Circle marker = null;
            if (cellType != CellType.Empty)
            {
                SpriteView spriteView;

                spriteView = new SpriteView(cellRectangle._xPosition + (cellRectangle._width / 2), cellRectangle._yPosition + (cellRectangle._height / 2), Color.Black, cellType == CellType.Yellow ? Color.Yellow : Color.Red, 8);
 
                marker = new Circle(spriteView, cellRectangle._width / cellRectangle._height * (cellRectangle._height - cellRectangle._height / 6) / 2);
                marker.Initialize();
            }

            return marker; // We return a marker to make it more straightforward to add custom marker animations, otherwise a better design choice might be to integrate it directly into the grid class
        }
    }
}
