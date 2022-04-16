using System.Collections.Generic;

namespace StephanHooft.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="SortedList{TKey, TValue}"/>.
    /// </summary>
    public static class SortedListExtensions
    {
        #region Static Methods

        /// <summary>
        /// Counts the amount of items in the <see cref="SortedList{TKey, TValue}"/> that match a certain
        /// <see cref="System.Predicate{T}"/>.
        /// </summary>
        /// <param name="predicate">The <see cref="System.Predicate{T}"/> to check against.</param>
        /// <returns>The amount of items in the <see cref="SortedList{TKey, TValue}"/> that match the <paramref name="predicate"/>.
        /// </returns>
        public static int Count<TKey, TValue>(this SortedList<TKey, TValue> l, System.Predicate<TValue> predicate)
        {
            var result = 0;
            foreach (TValue item in l.Values)
                if (predicate(item))
                    result++;
            return
                result;
        }

        public static int Count<TKey, TValue>(this SortedList<TKey, TValue> l, System.Predicate<KeyValuePair<TKey,TValue>> predicate)
        {
            var result = 0;
            foreach (KeyValuePair<TKey, TValue> item in l)
                if (predicate(item))
                    result++;
            return
                result;
        }

        /// <summary>
        /// Returns the lowest-indexed item of a <see cref="SortedList{TKey, TValue}"/>.
        /// </summary>
        /// <returns>The last <typeparamref name="TValue"/> in the <see cref="SortedList{TKey, TValue}"/>.</returns>
        public static TValue First<TKey, TValue>(this SortedList<TKey, TValue> l)
            => l.Values[0];

        /// <summary>
        /// Returns true if the <see cref="SortedList{TKey, TValue}"/> is empty.
        /// </summary>
        /// <returns>True if the <see cref="SortedList{TKey, TValue}"/> is empty.</returns>
        public static bool IsEmpty<TKey, TValue>(this SortedList<TKey, TValue> l)
            => l.Count == 0;

        /// <summary>
        /// Returns true if the <see cref="SortedList{TKey, TValue}"/> is null or empty.
        /// </summary>
        /// <returns>True if the <see cref="SortedList{TKey, TValue}"/> is null or empty.</returns>
        public static bool IsNullOrEmpty<TKey, TValue>(this SortedList<TKey, TValue> l)
            => l.Count == 0 || l == null;

        /// <summary>
        /// Returns the highest-indexed item of a <see cref="SortedList{TKey, TValue}"/>.
        /// </summary>
        /// <returns>The last <typeparamref name="TValue"/> in the <see cref="SortedList{TKey, TValue}"/>.</returns>
        public static TValue Last<TKey, TValue>(this SortedList<TKey, TValue> l)
            => l.Values[^1];

        /// <summary>
        /// Returns the highest index of a <see cref="SortedList{TKey, TValue}"/>.
        /// </summary>
        /// <returns>The index of the last <typeparamref name="TValue"/> in the <see cref="SortedList{TKey, TValue}"/>.
        /// </returns>
        public static int LastIndex<TKey, TValue>(this SortedList<TKey, TValue> l)
            => l.Count - 1;

        /// <summary>
        /// Ensures that the <see cref="SortedList{TKey, TValue}"/> contains a certain <typeparamref name="TKey"/>.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> to check for.</param>
        /// <param name="listName">The <see cref="SortedList{TKey, TValue}"/> name to use if an exception is thrown.
        /// </param>
        /// <returns>The original <see cref="SortedList{TKey, TValue}"/>.</returns>
        /// <exception cref="System.ArgumentException">If the <see cref="SortedList{TKey, TValue}"/> does not contain
        /// the <paramref name="key"/>.</exception>"
        public static SortedList<TKey, TValue> MustContainKey<TKey, TValue>
            (this SortedList<TKey, TValue> l, TKey key, string listName)
        {
            if (l.ContainsKey(key))
                return
                    l;
            else
                throw
                    new System.ArgumentException(string.Format(
                        "{1} {0} must be present in SortedList<{1}, {2}> {3}.",
                        key, typeof(TKey), typeof(TValue), listName));
        }

        /// <summary>
        /// Ensures that the <see cref="SortedList{TKey, TValue}"/> contains a certain <typeparamref name="TValue"/>.
        /// </summary>
        /// <param name="value">The <typeparamref name="TValue"/> to check for.</param>
        /// <param name="listName">The <see cref="SortedList{TKey, TValue}"/> name to use if an exception is thrown.
        /// </param>
        /// <returns>The original <see cref="SortedList{TKey, TValue}"/>.</returns>
        /// <exception cref="System.ArgumentException">If the <see cref="SortedList{TKey, TValue}"/> does not contain
        /// the <paramref name="value"/>.</exception>"
        public static SortedList<TKey, TValue> MustContainValue<TKey, TValue>
            (this SortedList<TKey, TValue> l, TValue value, string listName)
        {
            if (l.ContainsValue(value))
                return
                    l;
            else
                throw
                    new System.ArgumentException(string.Format(
                        "{2} {0} must be present in SortedList<{1}, {2}> {3}.",
                        value, typeof(TKey), typeof(TValue), listName));
        }

        /// <summary>
        /// Ensures that the <see cref="SortedList{TKey, TValue}"/> does not contain a certain
        /// <typeparamref name="TKey"/>.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> to check against.</param>
        /// <param name="listName">The <see cref="SortedList{TKey, TValue}"/> name to use if an exception is thrown.
        /// </param>
        /// <returns>The original <see cref="SortedList{TKey, TValue}"/>.</returns>
        /// <exception cref="System.ArgumentException">If the <see cref="SortedList{TKey, TValue}"/> contains
        /// the <paramref name="key"/>.</exception>"
        public static SortedList<TKey, TValue> MustNotContainKey<TKey, TValue>
            (this SortedList<TKey, TValue> l, TKey key, string listName)
        {
            if (!l.ContainsKey(key))
                return
                    l;
            else
                throw
                    new System.ArgumentException(string.Format(
                        "{1} {0} must not be present in SortedList<{1}, {2}> {3}.",
                        key, typeof(TKey), typeof(TValue), listName));
        }

        /// <summary>
        /// Ensures that the <see cref="SortedList{TKey, TValue}"/> does not contain a certain
        /// <typeparamref name="TValue"/>.
        /// </summary>
        /// <param name="value">The <typeparamref name="TValue"/> to check against.</param>
        /// <param name="listName">The <see cref="SortedList{TKey, TValue}"/> name to use if an exception is thrown.
        /// </param>
        /// <returns>The original <see cref="SortedList{TKey, TValue}"/>.</returns>
        /// <exception cref="System.ArgumentException">If the <see cref="SortedList{TKey, TValue}"/> contains
        /// the <paramref name="value"/>.</exception>"
        public static SortedList<TKey, TValue> MustNotContainValue<TKey, TValue>
            (this SortedList<TKey, TValue> l, TValue value, string listName)
        {
            if (!l.ContainsValue(value))
                return
                    l;
            else
                throw
                    new System.ArgumentException(string.Format(
                        "{2} {0} must not be present in SortedList<{1}, {2}> {3}.",
                        value, typeof(TKey), typeof(TValue), listName));
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
