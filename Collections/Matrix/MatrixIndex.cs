using System;
using UnityEngine;

namespace UnityUtility.Collections
{
    public readonly struct MatrixIndex
    {
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
    
    public static class MatrixIndexExtensions
    {
        public static MatrixIndex ToMatrixIndex(this Vector2Int vector) => new(vector.x, vector.y);
        public static Vector2Int ToVector2Int(this MatrixIndex index) => new(index.Row, index.Column);
    }
}