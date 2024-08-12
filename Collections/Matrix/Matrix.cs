using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityUtility.Enums;

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
        public T Get(MatrixIndex index) => source[Coord2Index(index)];
        #nullable enable
        public T? Get(MatrixIndex origin, Direction8 direction, int amount = 1)
        {
            int destinationIndex = GetIndex(origin, direction, amount);
            if (!IsValidIndex(destinationIndex)) {
                return default;
            }
            return source[destinationIndex];
        }
        #nullable disable
        public bool TryGet(MatrixIndex index, out T value)
        {
            if (IsValidIndex(index)) {
                value = source[Coord2Index(index)];
                return true;
            } else {
                value = default;
                return false;
            }
        }

        public bool TryGet(MatrixIndex origin, Direction8 direction, out T value, int amount = 1)
        {
            int destinationIndex = GetIndex(origin, direction, amount);
            if (IsValidIndex(destinationIndex)) {
                value = source[destinationIndex];
                return true;
            } else {
                value = default;
                return false;
            }
        }

        public bool Exist(MatrixIndex index) => IsValidIndex(index);
        public bool Exist(MatrixIndex origin, Direction8 direction, int amount = 1) => IsValidIndex(GetIndex(origin, direction, amount));

        public void Set(T value, int row, int column) => source[Coord2Index(row, column)] = value;
        public void Set(T value, MatrixIndex index) => source[Coord2Index(index)] = value;

        private int Coord2Index(MatrixIndex index) => Coord2Index(index.Row, index.Column);
        private int Coord2Index(int row, int column) => row * columnSize + column;
        private int GetIndex(MatrixIndex origin, Direction8 direction, int amount = 1) {
            int originIndex = Coord2Index(origin);
            int indexDiff = direction switch
            {
                Direction8.Up => -columnSize,
                Direction8.Down => columnSize,
                Direction8.Left => -1,
                Direction8.Right => 1,
                Direction8.RightUp => -columnSize + 1,
                Direction8.LeftUp => -columnSize - 1,
                Direction8.RightDown => columnSize + 1,
                Direction8.LeftDown => columnSize - 1,
                _ => throw new InvalidEnumArgumentException(),
            };

            return originIndex + indexDiff * amount;
        }
        private bool IsValidIndex(int index) => 0 <= index && index < source.Length;
        private bool IsValidIndex(MatrixIndex index) => IsValidIndex(Coord2Index(index));

        public IEnumerator<T> GetEnumerator() => source.AsEnumerable().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerable<T> GetDirectionEnumerator(MatrixIndex origin, Direction8 direction)
        {
            int amount = 1;
            while (TryGet(origin, direction, out T value, amount++))
            {
                yield return value;
            }
        }
    }
}
