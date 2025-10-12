using System.Drawing;

namespace Connect4.src.Game
{
    internal struct GridLayout
    {
        internal int _columns;
        internal int _rows;
        internal int _gap;
        internal int _padding;
        internal int _offset;
        internal Color _borderColor;
        internal Color _fillColor;
        internal int _borderSize;
        internal bool _isFilled;
        internal bool _hasBorder;

        internal GridLayout(int columns, int rows, int gap, int padding, int offset, Color borderColor, Color fillColor, int borderSize, bool isFilled, bool hasBorder)
        {
            _columns = columns;
            _rows = rows;
            _gap = gap;
            _padding = padding;
            _offset = offset;
            _borderColor = borderColor;
            _fillColor = fillColor;
            _borderSize = borderSize;
            _isFilled = isFilled;
            _hasBorder = hasBorder;
        }
    }
}
