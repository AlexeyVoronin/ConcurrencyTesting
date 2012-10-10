using System;
using System.Collections.Generic;

namespace Asteros.Abc.Common.Collections
{
    internal static class DictionaryExtensions
    {
        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> createValueFunc)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, createValueFunc());
            }

            return dictionary[key];
        }

        public static void RemoveRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TKey> keys)
        {
            foreach (var key in new List<TKey>(keys))
            {
                dictionary.Remove(key);
            }
        }
    }
}
