using StephanHooft.SortKeyDictionary;

namespace StephanHooft.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.
    /// </summary>
    public static class SortKeyDictionaryExtensions
    {
        #region Static Methods

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> is empty.
        /// </summary>
        /// <returns><see cref="true"/> if the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> is empty.
        /// </returns>
        public static bool IsEmpty<TKey, TSortKey, TValue>
            (this SortKeyDictionary<TKey, TSortKey, TValue> d) where TSortKey : System.IComparable
        {
            return
                d.Count == 0;
        }

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> is
        /// <see cref="null"/> or empty.
        /// </summary>
        /// <returns><see cref="true"/> if the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> is
        /// <see cref="null"/> or empty.</returns>
        public static bool IsNullOrEmpty<TKey, TSortKey, TValue>
            (this SortKeyDictionary<TKey, TSortKey, TValue> d) where TSortKey : System.IComparable
        {
            return
                d == null || d.Count == 0;
        }

        /// <summary>
        /// Ensures that the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> contains a certain 
        /// <typeparamref name="TKey"/>.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> to check for.</param>
        /// <param name="dictionaryName">The <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> name to use
        /// if an exception is thrown.</param>
        /// <returns>The original <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.</returns>
        /// <exception cref="System.ArgumentException">If the <typeparamref name="TKey"/> is not present.
        /// </exception>
        public static SortKeyDictionary<TKey, TSortKey, TValue> MustContainKey<TKey, TSortKey, TValue>
            (this SortKeyDictionary<TKey, TSortKey, TValue> d, TKey key, string dictionaryName)
            where TSortKey : System.IComparable
        {
            if (d.ContainsKey(key))
                return
                    d;
            else
                throw
                    new System.ArgumentException(string.Format(
                        "Key {0} must be present in SortKeyDictionary<{1},{2},{3}> {4}.",
                    key, typeof(TKey), typeof(TSortKey), typeof(TValue), dictionaryName));
        }

        /// <summary>
        /// Ensures that the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> contains a certain 
        /// <typeparamref name="TSortKey"/>.
        /// </summary>
        /// <param name="sortKey">The <typeparamref name="TSortKey"/> to check for.</param>
        /// <param name="dictionaryName">The <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> name to use
        /// if an exception is thrown.</param>
        /// <returns>The original <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.</returns>
        /// <exception cref="System.ArgumentException">If the <typeparamref name="TSortKey"/> is not present.
        /// </exception>
        public static SortKeyDictionary<TKey, TSortKey, TValue> MustContainKey<TKey, TSortKey, TValue>
            (this SortKeyDictionary<TKey, TSortKey, TValue> d, TSortKey sortKey, string dictionaryName)
            where TSortKey : System.IComparable
        {
            if (d.ContainsSortKey(sortKey))
                return
                    d;
            else
                throw
                    new System.ArgumentException(string.Format(
                        "Sorting Key {0} must be present in SortKeyDictionary<{1},{2},{3}> {4}.",
                    sortKey, typeof(TKey), typeof(TSortKey), typeof(TValue), dictionaryName));
        }

        /// <summary>
        /// Ensures that the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> contains a certain 
        /// <typeparamref name="TValue"/>.
        /// </summary>
        /// <param name="value">The <typeparamref name="TValue"/> to check for.</param>
        /// <param name="dictionaryName">The <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> name to use
        /// if an exception is thrown.</param>
        /// <returns>The original <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.</returns>
        /// <exception cref="System.ArgumentException">If the <typeparamref name="TValue"/> is not present.
        /// </exception>
        public static SortKeyDictionary<TKey, TSortKey, TValue> MustContainValue<TKey, TSortKey, TValue>
            (this SortKeyDictionary<TKey, TSortKey, TValue> d, TValue value, string dictionaryName)
            where TSortKey : System.IComparable
        {
            if (d.ContainsValue(value))
                return
                    d;
            else
                throw
                    new System.ArgumentException(string.Format(
                        "Value {0} must be present in SortKeyDictionary<{1},{2},{3}> {4}.",
                    value, typeof(TKey), typeof(TSortKey), typeof(TValue), dictionaryName));
        }

        ///////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Ensures that the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> does not contain a certain 
        /// <typeparamref name="TKey"/>.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> to check against.</param>
        /// <param name="dictionaryName">The <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> name to use
        /// if an exception is thrown.</param>
        /// <returns>The original <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.</returns>
        /// <exception cref="System.ArgumentException">If the <typeparamref name="TKey"/> is present.</exception>
        public static SortKeyDictionary<TKey, TSortKey, TValue> MustNotContainKey<TKey, TSortKey, TValue>
            (this SortKeyDictionary<TKey, TSortKey, TValue> d, TKey key, string dictionaryName)
            where TSortKey : System.IComparable
        {
            if (!d.ContainsKey(key))
                return
                    d;
            else
                throw
                    new System.ArgumentException(string.Format(
                        "Key {0} must not be present in SortKeyDictionary<{1},{2},{3}> {4}.",
                    key, typeof(TKey), typeof(TSortKey), typeof(TValue), dictionaryName));
        }

        /// <summary>
        /// Ensures that the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> does not contain a certain 
        /// <typeparamref name="TSortKey"/>.
        /// </summary>
        /// <param name="sortKey">The <typeparamref name="TSortKey"/> to check against.</param>
        /// <param name="dictionaryName">The <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> name to use
        /// if an exception is thrown.</param>
        /// <returns>The original <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.</returns>
        /// <exception cref="System.ArgumentException">If the <typeparamref name="TSortKey"/> is present.</exception>
        public static SortKeyDictionary<TKey, TSortKey, TValue> MustNotContainKey<TKey, TSortKey, TValue>
            (this SortKeyDictionary<TKey, TSortKey, TValue> d, TSortKey sortKey, string dictionaryName)
            where TSortKey : System.IComparable
        {
            if (!d.ContainsSortKey(sortKey))
                return
                    d;
            else
                throw
                    new System.ArgumentException(string.Format(
                        "Sorting Key {0} must not be present in SortKeyDictionary<{1},{2},{3}> {4}.",
                    sortKey, typeof(TKey), typeof(TSortKey), typeof(TValue), dictionaryName));
        }

        /// <summary>
        /// Ensures that the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> does not contain a certain 
        /// <typeparamref name="TValue"/>.
        /// </summary>
        /// <param name="value">The <typeparamref name="TValue"/> to check against.</param>
        /// <param name="dictionaryName">The <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> name to use
        /// if an exception is thrown.</param>
        /// <returns>The original <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.</returns>
        /// <exception cref="System.ArgumentException">If the <typeparamref name="TValue"/> is present.</exception>
        public static SortKeyDictionary<TKey, TSortKey, TValue> MustNotContainValue<TKey, TSortKey, TValue>
            (this SortKeyDictionary<TKey, TSortKey, TValue> d, TValue value, string dictionaryName)
            where TSortKey: System.IComparable
        {
            if (!d.ContainsValue(value))
                return
                    d;
            else
                throw
                    new System.ArgumentException(string.Format(
                        "Value {0} must not be present in SortKeyDictionary<{1},{2},{3}> {4}.",
                    value, typeof(TKey), typeof(TSortKey), typeof(TValue), dictionaryName));
        }

        /// <summary>
        /// Adds the given <typeparamref name="TKey"/>, <typeparamref name="TSortKey"/> and
        /// <typeparamref name="TValue"/> to the given  <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> only if
        /// the <typeparamref name="TKey"/> is NOT present.
        /// </summary>
        /// <param name="key">The given <typeparamref name="TKey"/>.</param>
        /// <param name="sortKey">The given <typeparamref name="TSortKey"/>.</param>
        /// <param name="value">The given <typeparamref name="TValue"/>.</param>
        /// <returns><see cref="true"/> if added successfully, <see cref="false"/> otherwise.</returns>
        public static bool TryAdd<TKey, TSortKey, TValue>
            (this SortKeyDictionary<TKey, TSortKey, TValue> d, TKey key, TSortKey sortKey, TValue value)
            where TSortKey : System.IComparable
        {
            if (d.ContainsKey(key))
                return
                    false;
            d.Add(key, sortKey, value);
            return
                true;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
