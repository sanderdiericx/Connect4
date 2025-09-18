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
        public int Columns;
        public int Rows;
        public int Gap;
        public int Padding;
        public int Offset;
        public Color BorderColor;
        public Color FillColor;
        public int BorderSize;
        public bool IsFilled;
        public bool HasBorder;

        public GridLayout(int columns, int rows, int gap, int padding, int offset, Color borderColor, Color fillColor, int borderSize, bool isFilled, bool hasBorder)
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
