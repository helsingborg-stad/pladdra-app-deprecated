using System;
using System.Collections.Generic;
using System.Linq;

public static class EnumerableExtensions {
    public static IEnumerable<T> UniqueBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> key) {
        var keys = new HashSet<TKey>();
        return enumerable.Where(item => keys.Add(key(item)));
    }
}