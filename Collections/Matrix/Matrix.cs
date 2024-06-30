using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TMPro;
namespace UnityUtility.Collections
{
    public class Matrix<T> : IMatrix<T> {
        private T[] source;
        private int columnSize, rowSize;

        public Matrix(int columnSize, int rowSize) {
            this.columnSize = columnSize;
            this.rowSize = rowSize;
            source = new T[columnSize * rowSize];
        }

        public T Get(int row, int column) => source[Coord2Index(row, column)];
        public void Set(T value, int row, int column) => source[Coord2Index(row, column)] = value;

        private int Coord2Index(int row, int column) => row * columnSize + column;

        public IEnumerator<T> GetEnumerator() => source.AsEnumerable().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
