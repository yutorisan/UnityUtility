using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityUtility.Linq {
    public static partial class EnumerableFactory {
        public static IEnumerable<Vector2Int> FromVector2Int(Vector2Int source) {
            for (int y = 0; y < source.y; y++) {
                for (int x = 0; x < source.x; x++) {
                    yield return new Vector2Int(x, y);
                }
            }
        }
    }
}