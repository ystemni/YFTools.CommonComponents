using System;
using System.Collections.Generic;
using System.Linq;

namespace YFTools.CommonComponents.Extensions;

public static class LinqExtension
{
    public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
        var hashSet = new HashSet<TKey>();
        return source.Where(t => hashSet.Add(keySelector(t)));
    }
}