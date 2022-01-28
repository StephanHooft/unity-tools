using System;
using System.Collections.Generic;

namespace StephanHooft.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Adds the given <typeparamref name="TKey"/> and <typeparamref name="TValue"/> to the given
        /// <see cref="Dictionary{TKey, TValue}"/> if the <typeparamref name="TKey"/> is NOT present.
        /// If present, the <typeparamref name="TValue"/> will be replaced with the new <typeparamref name="TValue"/>.
        /// </summary>
        /// <param name="key">The given <typeparamref name="TKey"/>.</param>
        /// <param name="value">The given <typeparamref name="TValue"/>.</param>
        public static void AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> d, TKey key, TValue value)
        {
            if (d.ContainsKey(key))
                d[key] = value;
            else
                d.Add(key, value);
        }

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="Dictionary{TKey, TValue}"/> is empty.
        /// </summary>
        /// <returns><see cref="true"/> if the <see cref="Dictionary{TKey, TValue}"/> is empty.</returns>
        public static bool IsEmpty<TKey, TValue>(this Dictionary<TKey, TValue> d)
        {
            return
                d.Count == 0;
        }

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="Dictionary{TKey, TValue}"/> is <see cref="null"/> or empty.
        /// </summary>
        /// <returns><see cref="true"/> if the <see cref="Dictionary{TKey, TValue}"/> is <see cref="null"/> or empty.</returns>
        public static bool IsNullOrEmpty<TKey, TValue>(this Dictionary<TKey, TValue> d)
        {
            return
                d == null || d.Count == 0;
        }

        /// <summary>
        /// Ensures that the <see cref="Dictionary{TKey, TValue}"/> contains a set <typeparamref name="TKey"/>.
        /// <para>An <see cref="ArgumentException"/> featuring <paramref name="dictionaryName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> to check for.</param>
        /// <param name="dictionaryName">The <see cref="Dictionary{TKey, TValue}"/> name to use if an <see cref="ArgumentException"/> is thrown.</param>
        /// <returns>The original <see cref="Dictionary{TKey, TValue}"/>.</returns>
        public static Dictionary<TKey, TValue> MustContainKey<TKey, TValue>(this Dictionary<TKey, TValue> d, TKey key, string dictionaryName)
        {
            if (d.ContainsKey(key))
                return
                    d;
            else
                throw
                    new ArgumentException(string.Format(
                        "Key {0} must be present in Dictionary<{1},{2}> {3}.",
                        key, typeof(TKey), typeof(TValue),dictionaryName));
        }

        /// <summary>
        /// Ensures that the <see cref="Dictionary{TKey, TValue}"/> contains a set <typeparamref name="TValue"/>.
        /// <para>An <see cref="ArgumentException"/> featuring <paramref name="dictionaryName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="value">The <typeparamref name="TValue"/> to check for.</param>
        /// <param name="dictionaryName">The <see cref="Dictionary{TKey, TValue}"/> name to use if an <see cref="ArgumentException"/> is thrown.</param>
        /// <returns>The original <see cref="Dictionary{TKey, TValue}"/>.</returns>
        public static Dictionary<TKey, TValue> MustContainValue<TKey, TValue>(this Dictionary<TKey, TValue> d, TValue value, string dictionaryName)
        {
            if (d.ContainsValue(value))
                return
                    d;
            else
                throw
                    new ArgumentException(string.Format(
                        "Value {0} must be present in Dictionary<{1},{2}> {3}.", 
                        value, typeof(TKey), typeof(TValue), dictionaryName));
        }

        /// <summary>
        /// Ensures that the <see cref="Dictionary{TKey, TValue}"/> does not contain a set <typeparamref name="TKey"/>.
        /// <para>An <see cref="ArgumentException"/> featuring <paramref name="dictionaryName"/> is thrown if it does.</para>
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> to check against.</param>
        /// <param name="dictionaryName">The <see cref="Dictionary{TKey, TValue}"/> name to use if an <see cref="ArgumentException"/> is thrown.</param>
        /// <returns>The original <see cref="Dictionary{TKey, TValue}"/>.</returns>
        public static Dictionary<TKey, TValue> MustNotContainKey<TKey, TValue>(this Dictionary<TKey, TValue> d, TKey key, string dictionaryName)
        {
            if (!d.ContainsKey(key))
                return
                    d;
            else
                throw
                    new ArgumentException(string.Format(
                        "Key {0} must not be present in Dictionary<{1},{2}> {3}.",
                        key, typeof(TKey), typeof(TValue), dictionaryName));
        }

        /// <summary>
        /// Ensures that the <see cref="Dictionary{TKey, TValue}"/> does not contain a set <typeparamref name="TValue"/>.
        /// <para>An <see cref="ArgumentException"/> featuring <paramref name="dictionaryName"/> is thrown if it does.</para>
        /// </summary>
        /// <param name="value">The <typeparamref name="TValue"/> to check against.</param>
        /// <param name="dictionaryName">The <see cref="Dictionary{TKey, TValue}"/> name to use if an <see cref="ArgumentException"/> is thrown.</param>
        /// <returns>The original <see cref="Dictionary{TKey, TValue}"/>.</returns>
        public static Dictionary<TKey, TValue> MustNotContainValue<TKey, TValue>(this Dictionary<TKey, TValue> d, TValue value, string dictionaryName)
        {
            if (!d.ContainsValue(value))
                return
                    d;
            else
                throw
                    new ArgumentException(string.Format(
                        "Value {0} must not be present in Dictionary<{1},{2}> {3}.",
                        value, typeof(TKey), typeof(TValue), dictionaryName));
        }

        /// <summary>
        /// Adds the given <typeparamref name="TKey"/> and <typeparamref name="TValue"/> to the given 
        /// <see cref="Dictionary{TKey, TValue}"/> only if the <typeparamref name="TKey"/> is NOT present.
        /// </summary>
        /// <param name="key">The given <typeparamref name="TKey"/>.</param>
        /// <param name="value">The given <typeparamref name="TValue"/>.</param>
        /// <returns><see cref="true"/> if added successfully, <see cref="false"/> otherwise.</returns>
        public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> d, TKey key, TValue value)
        {
            if (d.ContainsKey(key))
                return
                    false;
            d.Add(key, value);
            return
                true;
        }
    }
}
