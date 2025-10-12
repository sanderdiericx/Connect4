using Connect4.src.Graphics.Sprites;

namespace Connect4.src.Game
{
    internal struct GridCell
    {
        internal Rectangle _cellRectangle;
        internal Circle _cellMarker;
        internal CellType _cellType;

        internal GridCell(Rectangle cellRectangle, Circle cellMarker, CellType cellType)
        {
            _cellRectangle = cellRectangle;
            _cellMarker = cellMarker;
            _cellType = cellType;
        }
    }
}
