using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.src.Game
{
    internal struct GridLayout
    {
        internal int Columns;
        internal int Rows;
        internal int Gap;
        internal int Padding;
        internal int Offset;
        internal Color BorderColor;
        internal Color FillColor;
        internal int BorderSize;
        internal bool IsFilled;
        internal bool HasBorder;

        internal GridLayout(int columns, int rows, int gap, int padding, int offset, Color borderColor, Color fillColor, int borderSize, bool isFilled, bool hasBorder)
        {
            Columns = columns;
            Rows = rows;
            Gap = gap;
            Padding = padding;
            Offset = offset;
            BorderColor = borderColor;
            FillColor = fillColor;
            BorderSize = borderSize;
            IsFilled = isFilled;
            HasBorder = hasBorder;
        }
    }
}
