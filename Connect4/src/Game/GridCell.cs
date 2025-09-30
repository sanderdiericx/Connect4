using Connect4.src.Graphics.Sprites;

namespace Connect4.src.Game
{
    internal struct GridCell
    {
        internal Rectangle _cellRectangle;
        internal CellType _cellType;

        internal GridCell(Rectangle cellRectangle, CellType cellType)
        {
            _cellRectangle = cellRectangle;
            _cellType = cellType;
        }
    }
}
