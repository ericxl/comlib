using System;
using System.Collections.Generic;
public static class IEnumerableExtensions
{
    /// <summary>
    ///   Shuffles a list in O(n) time by using the Fisher-Yates/Knuth algorithm.
    /// </summary>
    /// <param name="r"></param>
    /// <param name = "list"></param>
    public static void Shuffle<T>(this IList<T> list)
    {
        var r = new Random();
        for (var i = 0; i < list.Count; i++)
        {
            var j = r.Next(0, i + 1);

            var temp = list[j];
            list[j] = list[i];
            list[i] = temp;
        }
    }

    public static bool Equals<T>(this IList<T> list, IList<T> other)
    {
        if (list == null && other != null) return false;
        if (list != null && other == null) return false;
        if (list == null && other == null) return true;
        return new HashSet<T>(list).SetEquals(other);
    }
}

