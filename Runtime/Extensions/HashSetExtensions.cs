using System.Collections.Generic;

namespace StephanHooft.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="HashSet{T}"/>.
    /// </summary>
    public static class HashSetExtensions
    {
        #region Static Methods

        /// <summary>
        /// Counts the amount of items in the <see cref="HashSet{T}"/> that match a certain
        /// <see cref="System.Predicate{T}"/>.
        /// </summary>
        /// <param name="predicate">The <see cref="System.Predicate{T}"/> to check against.</param>
        /// <returns>The amount of items in the <see cref="HashSet{T}"/> that match the <paramref name="predicate"/>.
        /// </returns>
        public static int Count<T>(this HashSet<T> h, System.Predicate<T> predicate)
        {
            var result = 0;
            foreach (T item in h)
                if (predicate(item))
                    result++;
            return
                result;
        }

        /// <summary>
        /// Returns true if the <see cref="HashSet{T}"/> is empty.
        /// </summary>
        /// <returns>True if the <see cref="HashSet{T}"/> is empty.</returns>
        public static bool IsEmpty<T>(this HashSet<T> h)
            => h.Count == 0;

        /// <summary>
        /// Returns true if the <see cref="HashSet{T}"/> is <see cref="null"/> or empty.
        /// </summary>
        /// <returns>True if the <see cref="HashSet{T}"/> is <see cref="null"/> or empty.</returns>
        public static bool IsNullOrEmpty<T>(this HashSet<T> h)
            => h.Count == 0 || h == null;

        /// <summary>
        /// Ensures that the <see cref="HashSet{T}"/> contains a set <typeparamref name="T"/>.
        /// </summary>
        /// <param name="item">The <typeparamref name="T"/> to check for.</param>
        /// <param name="setName">The <see cref="HashSet{T}"/> name to use if an exception is thrown.</param>
        /// <returns>The original <see cref="HashSet{T}"/>.</returns>
        /// <exception cref="System.ArgumentException">If the <see cref="HashSet{T}"/> does not contain
        /// <paramref name="item"/>.</exception>"
        public static HashSet<T> MustContain<T>(this HashSet<T> h, T item, string setName)
        {
            if (h.Contains(item))
                return
                    h;
            else
                throw
                    new System.ArgumentException(
                        string.Format("{0} must be present in HashSet<{1}> {2}.", item, typeof(T), setName));
        }

        /// <summary>
        /// Ensures that the <see cref="HashSet{T}"/> does not contain a set <typeparamref name="T"/>.
        /// </summary>
        /// <param name="item">The <typeparamref name="T"/> to check against.</param>
        /// <param name="setName">The <see cref="HashSet{T}"/> name to use if an exception is thrown.</param>
        /// <returns>The original <see cref="HashSet{T}"/>.</returns>
        /// <exception cref="System.ArgumentException">If the <see cref="HashSet{T}"/> contains
        /// <paramref name="item"/>.</exception>"
        public static HashSet<T> MustNotContain<T>(this HashSet<T> h, T item, string setName)
        {
            if (!h.Contains(item))
                return
                    h;
            else
                throw
                    new System.ArgumentException(
                        string.Format("{0} must not be present in HashSet<{1}> {2}.", item, typeof(T), setName));
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
