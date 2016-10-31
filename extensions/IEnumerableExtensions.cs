using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class IEnumerableExtensions
{
    /// <summary>
    /// Shuffles a list in O(n) time by using the Fisher-Yates/Knuth algorithm.
    /// </summary>
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

    public static T RandomElement<T>(this IList<T> array)
    {
        var index = new Random().Next(0, array.Count);
        return array[index];
    }

    public static bool Equals<T>(this IList<T> list, IList<T> other)
    {
        if (list == null && other != null) return false;
        if (list != null && other == null) return false;
        if (list == null && other == null) return true;
        if (list.Count != other.Count) return false;
        return !list.Where((t, i) => Newtonsoft.Json.JsonConvert.SerializeObject(t) != Newtonsoft.Json.JsonConvert.SerializeObject(other[i])).Any();
    }

    public static void PopulateDataBy<TData, TController>(this IList<TController> goList, IList<TData> list, Func<TData, TController> AddFactory, Action<TController, TData> UpdateFactory,
        Action<TController> CleanUpFactory)
    {
        var listCount = list == null ? 0 : list.Count;
        var iteration = Math.Min(goList.Count, listCount);
        var maxIteration = Math.Max(goList.Count, listCount);
        var deleteGo = goList.Count > listCount;
        if (UpdateFactory != null)
        {
            for (var i = 0; i < iteration; i++)
            {
                UpdateFactory(goList[i], list[i]);
            }
        }

        if (deleteGo)
        {
            for (var i = iteration; i < maxIteration; i++)
            {
                var go = goList.Last();
                if (CleanUpFactory != null)
                {
                    CleanUpFactory(go);
                }
                goList.RemoveAt(goList.Count - 1);
            }
        }
        else
        {
            for (var i = iteration; i < maxIteration; i++)
            {
                var added = AddFactory(list[i]);
                if (UpdateFactory != null)
                {
                    UpdateFactory(added, list[i]);
                }
                goList.Add(added);
            }
        }
    }

    /// <summary>
    /// Calls ToString() on every element of the list, puts [encapsulate] directly before and after the result
    /// and then concatenates the results with [seperator] between them.
    /// </summary>
    /// <typeparam name="T">The collection element type.</typeparam>
    /// <param name="list">The collection to concatenate.</param>
    /// <param name="separator">The seperator between entries.</param>
    /// <param name="encapsulate">The string to put directly before and after every item.</param>
    /// <returns>A string containing the ToString() results of all items.</returns>
    public static string ToOneLineString<T>(this IEnumerable<T> list, string separator = ", ", string encapsulate = "\"")
    {
        var useEncapsulate = encapsulate.Length > 0;

        var result = new StringBuilder();
        foreach (var element in list)
        {
            if (result.Length > 0)
                result.Append(separator);

            if (useEncapsulate)
                result.Append(encapsulate);

            result.Append(element);

            if (useEncapsulate)
                result.Append(encapsulate);
        }

        return result.ToString();
    }
}

