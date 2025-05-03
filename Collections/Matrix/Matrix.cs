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
            var destinationIndex = GetIndex(origin, direction, amount);
            if (!IsValidIndex(destinationIndex)) {
                return default;
            }
            return source[Coord2Index(destinationIndex)];
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
            var destinationIndex = GetIndex(origin, direction, amount);
            if (IsValidIndex(destinationIndex)) {
                value = source[Coord2Index(destinationIndex)];
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
        private int Coord2Index(int row, int column)
        {
            if (row < 0 || rowSize <= row)       throw new ArgumentOutOfRangeException(nameof(row));
            if (column < 0 || rowSize <= column) throw new ArgumentOutOfRangeException(nameof(column));
            return row * columnSize + column;
        }

        private MatrixIndex GetIndex(MatrixIndex origin, Direction8 direction, int amount = 1) {
            MatrixIndex newIndex = direction switch
            {
                Direction8.Up => origin.Shift(amount, 0),
                Direction8.Down => origin.Shift(-amount, 0),
                Direction8.Left => origin.Shift(0, -amount),
                Direction8.Right => origin.Shift(0, amount),
                Direction8.RightUp => origin.Shift(amount, amount),
                Direction8.LeftUp => origin.Shift(amount, -amount),
                Direction8.RightDown => origin.Shift(-amount, amount),
                Direction8.LeftDown => origin.Shift(-amount, -amount),
                _ => throw new InvalidEnumArgumentException(),
            };

            return newIndex;
        }
        private bool IsValidIndex(int index) => 0 <= index && index < source.Length;
        private bool IsValidIndex(MatrixIndex index)
        {
            if (index.Row    < 0 || index.Row    >= this.rowSize)    return false;
            if (index.Column < 0 || index.Column >= this.columnSize) return false;
            return IsValidIndex(Coord2Index(index));
        }

        public IEnumerator<T> GetEnumerator() => source.AsEnumerable().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerable<T> GetDirectionEnumerable(MatrixIndex origin, Direction8 direction)
        {
            int amount = 1;
            while (TryGet(origin, direction, out T value, amount++))
            {
                yield return value;
            }
        }
    }
}
