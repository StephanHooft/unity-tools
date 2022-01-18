using System;
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
        public static bool AddIfNotExists<TKey, TValue>(this Dictionary<TKey, TValue> d, TKey key, TValue value)
        {
            if (d.ContainsKey(key))
                return false;
            d.Add(key, value);
            return true;
        }

        /// <summary>
        /// Method that adds the given key and value to the given dictionary if the key is NOT present in the dictionary.
        /// If present, the value will be replaced with the new value.
        /// </summary>
        /// <param name="key">The given key.</param>
        /// <param name="value">The given value.</param>
        public static void AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> d, TKey key, TValue value)
        {
            if (d.ContainsKey(key))
                d[key] = value;
            else
                d.Add(key, value);
        }

        /// <summary>
        /// Returns true if the <see cref="Dictionary{TKey, TValue}"/> is empty.
        /// </summary>
        /// <returns>True if the <see cref="Dictionary{TKey, TValue}"/> is empty.</returns>
        public static bool IsEmpty<TKey, TValue>(this Dictionary<TKey, TValue> d)
        {
            return d.Count == 0;
        }

        /// <summary>
        /// Returns true if the <see cref="Dictionary{TKey, TValue}"/> is null or empty.
        /// </summary>
        /// <returns>True if the <see cref="Dictionary{TKey, TValue}"/> is null or empty.</returns>
        public static bool IsNullOrEmpty<TKey, TValue>(this Dictionary<TKey, TValue> d)
        {
            return d == null || d.Count == 0;
        }

        /// <summary>
        /// Ensures that the <see cref="Dictionary{TKey, TValue}"/> contains a set <typeparamref name="TKey"/> <paramref name="key"/>.
        /// <para>An <see cref="ArgumentException"/> featuring <paramref name="dictionaryName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> to check for.</param>
        /// <param name="dictionaryName">The <see cref="Dictionary{TKey, TValue}"/> name to use if an <see cref="ArgumentException"/> is thrown.</param>
        /// <returns>The original <see cref="Dictionary{TKey, TValue}"/>, assuming no <see cref="ArgumentException"/> was thrown.</returns>
        public static Dictionary<TKey, TValue> MustContainKey<TKey, TValue>(this Dictionary<TKey, TValue> d, TKey key, string dictionaryName)
        {
            if (d.ContainsKey(key))
                return
                    d;
            else
                throw
                    new ArgumentException("Key " + key.ToString() + " must be present in Dictionary<" + typeof(TKey).ToString()
                    + ", " + typeof(TValue).ToString() + "> " + dictionaryName + ".");
        }

        /// <summary>
        /// Ensures that the <see cref="Dictionary{TKey, TValue}"/> contains a set <typeparamref name="TValue"/> <paramref name="value"/>.
        /// <para>An <see cref="ArgumentException"/> featuring <paramref name="dictionaryName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="value">The <typeparamref name="TValue"/> to check for.</param>
        /// <param name="dictionaryName">The <see cref="Dictionary{TKey, TValue}"/> name to use if an <see cref="ArgumentException"/> is thrown.</param>
        /// <returns>The original <see cref="Dictionary{TKey, TValue}"/>, assuming no <see cref="ArgumentException"/> was thrown.</returns>
        public static Dictionary<TKey, TValue> MustContainValue<TKey, TValue>(this Dictionary<TKey, TValue> d, TValue value, string dictionaryName)
        {
            if (d.ContainsValue(value))
                return
                    d;
            else
                throw
                    new ArgumentException("Value " + value.ToString() + " must be present in Dictionary<" + typeof(TKey).ToString()
                    + ", " + typeof(TValue).ToString() + "> " + dictionaryName + ".");
        }

        /// <summary>
        /// Ensures that the <see cref="Dictionary{TKey, TValue}"/> does not contain a set <typeparamref name="TKey"/> <paramref name="key"/>.
        /// <para>An <see cref="ArgumentException"/> featuring <paramref name="dictionaryName"/> is thrown if it does.</para>
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> to check against.</param>
        /// <param name="dictionaryName">The <see cref="Dictionary{TKey, TValue}"/> name to use if an <see cref="ArgumentException"/> is thrown.</param>
        /// <returns>The original <see cref="Dictionary{TKey, TValue}"/>, assuming no <see cref="ArgumentException"/> was thrown.</returns>
        public static Dictionary<TKey, TValue> MustNotContainKey<TKey, TValue>(this Dictionary<TKey, TValue> d, TKey key, string dictionaryName)
        {
            if (!d.ContainsKey(key))
                return
                    d;
            else
                throw
                    new ArgumentException("Key " + key.ToString() + " must not be present in Dictionary<" + typeof(TKey).ToString()
                    + ", " + typeof(TValue).ToString() + "> " + dictionaryName + ".");
        }

        /// <summary>
        /// Ensures that the <see cref="Dictionary{TKey, TValue}"/> does not contain a set <typeparamref name="TValue"/> <paramref name="value"/>.
        /// <para>An <see cref="ArgumentException"/> featuring <paramref name="dictionaryName"/> is thrown if it does.</para>
        /// </summary>
        /// <param name="value">The <typeparamref name="TValue"/> to check against.</param>
        /// <param name="dictionaryName">The <see cref="Dictionary{TKey, TValue}"/> name to use if an <see cref="ArgumentException"/> is thrown.</param>
        /// <returns>The original <see cref="Dictionary{TKey, TValue}"/>, assuming no <see cref="ArgumentException"/> was thrown.</returns>
        public static Dictionary<TKey, TValue> MustNotContainValue<TKey, TValue>(this Dictionary<TKey, TValue> d, TValue value, string dictionaryName)
        {
            if (!d.ContainsValue(value))
                return
                    d;
            else
                throw
                    new ArgumentException("Value " + value.ToString() + " must not be present in Dictionary<" + typeof(TKey).ToString()
                    + ", " + typeof(TValue).ToString() + "> " + dictionaryName + ".");
        }
    }
}
