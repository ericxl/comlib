using System.Collections.Generic;

public static class DictionaryExtensions
{
    public static TValue TryGet<TKey,TValue>(this Dictionary<TKey, TValue> dict, TKey key)
    {
        TValue outValue;
        return dict.TryGetValue(key, out outValue) ? outValue : default(TValue);
    }
}
