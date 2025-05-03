using System;
using System.Collections;
using System.Collections.Generic;
using UnityUtility.Enums;

namespace UnityUtility.Collections {
    public interface IReadOnlyMatrix<out T> : IEnumerable<T> {
        T Get(int row, int column);
        T Get(MatrixIndex index);
        /// <summary>
        /// 指定の座標を基準に、指定方向にnマス分移動した座標の値を取得する
        /// </summary>
        /// <param name="origin">基準とする座標</param>
        /// <param name="direction">方向</param>
        /// <param name="amount">移動量</param>
        /// <returns></returns>
        # nullable enable
        T? Get(MatrixIndex origin, Direction8 direction, int amount = 1);
        # nullable disable
        bool Exist(MatrixIndex index);
        bool Exist(MatrixIndex origin, Direction8 direction, int amount = 1);
        /// <summary>
        /// originを起点として、そこからdirection方向にまっすく移動した際に通過する要素を列挙します
        /// </summary>
        /// <param name="origin">起点座標</param>
        /// <param name="direction">精査方向</param>
        /// <returns></returns>
        IEnumerable<T> GetDirectionEnumerable(MatrixIndex origin, Direction8 direction);
    }

}
