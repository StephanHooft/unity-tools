using System.Collections.Generic;

namespace StephanHooft.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Method that adds the given key and value to the given dictionary only if the key is NOT present in the dictionary.
        /// This will be useful to avoid repetitive "if(!containskey()) then add" pattern of coding.
        /// </summary>
        /// <param name="key">The given key.</param>
        /// <param name="value">The given value.</param>
        /// <returns>True if added successfully, false otherwise.</returns>
        public static bool AddIfNotExists<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
                return false;
            dict.Add(key, value);
            return true;
        }

        /// <summary>
        /// Method that adds the given key and value to the given dictionary if the key is NOT present in the dictionary.
        /// If present, the value will be replaced with the new value.
        /// </summary>
        /// <param name="key">The given key.</param>
        /// <param name="value">The given value.</param>
        public static void AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
                dict[key] = value;
            else
                dict.Add(key, value);
        }

        /// <summary>
        /// Returns true if the <see cref="Dictionary{TKey, TValue}"/> is null or empty.
        /// </summary>
        /// <returns>True if the <see cref="Dictionary{TKey, TValue}"/> is null or empty.</returns>
        public static bool IsNullOrEmpty<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            return dictionary == null || dictionary.Count == 0;
        }
    }
}
