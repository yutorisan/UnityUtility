using System;
using System.Collections;
using System.Collections.Generic;
using UnityUtility.Enums;

namespace UnityUtility.Collections {
    public interface IMatrix<T> : IReadOnlyMatrix<T> {
        void Set(T value, int row, int column);
        void Set(T value, MatrixIndex index);
        bool TryGet(MatrixIndex index, out T value);
        bool TryGet(MatrixIndex origin, Direction8 direction, out T value, int amount = 1);
    }
}
