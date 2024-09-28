using System;

namespace UnityUtility.Collections {
    public readonly struct MatrixIndex {
        public MatrixIndex(int row, int column)
        {
            Row = row;
            Column = column;
        }
        public int Row { get; }
        public int Column { get; }
        public MatrixIndex Shift(int row, int column) => new(Row + row, Column + column);

        public override string ToString() => $"Row={Row}, Column={Column}";
    }
}