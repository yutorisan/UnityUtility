namespace UnityUtility.Collections {
    public readonly struct MatrixIndex {
        public MatrixIndex(int row, int column)
        {
            Row = row;
            Column = column;
        }
        public int Row { get; }
        public int Column { get; }
    }
}