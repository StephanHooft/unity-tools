﻿using System.Collections.Generic;

namespace StephanHooft.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="List{T}"/>.
    /// </summary>
    public static class ListExtensions
    {
        #region Static Methods

        /// <summary>
        /// Counts the amount of items in the <see cref="List{T}"/> that match a certain
        /// <see cref="System.Predicate{T}"/>.
        /// </summary>
        /// <param name="predicate">The <see cref="System.Predicate{T}"/> to check against.</param>
        /// <returns>The amount of items in the <see cref="List{T}"/> that match the <paramref name="predicate"/>.
        /// </returns>
        public static int Count<T>(this List<T> l, System.Predicate<T> predicate)
        {
            var result = 0;
            foreach (T item in l)
                if (predicate(item))
                    result++;
            return
                result;
        }

        /// <summary>
        /// Returns the lowest-indexed item of a <see cref="List{T}"/>.
        /// </summary>
        /// <returns>The last <typeparamref name="T"/> in the <see cref="List{T}"/>.</returns>
        public static T First<T>(this List<T> l)
            => l[0];

        /// <summary>
        /// Returns true if the <see cref="List{T}"/> is empty.
        /// </summary>
        /// <returns>True if the <see cref="List{T}"/> is empty.</returns>
        public static bool IsEmpty<T>(this List<T> l)
            => l.Count == 0;

        /// <summary>
        /// Returns true if the <see cref="List{T}"/> is null or empty.
        /// </summary>
        /// <returns>True if the <see cref="List{T}"/> is null or empty.</returns>
        public static bool IsNullOrEmpty<T>(this List<T> l)
            => l.Count == 0 || l == null;

        /// <summary>
        /// Returns te highest-indexed item of a <see cref="List{T}"/>.
        /// </summary>
        /// <returns>The last <typeparamref name="T"/> in the <see cref="List{T}"/>.</returns>
        public static T Last<T>(this List<T> l)
            => l[^1];

        /// <summary>
        /// Returns the highest index of a <see cref="List{T}"/>.
        /// </summary>
        /// <returns>The index of the last <typeparamref name="T"/> in the <see cref="List{T}"/>.</returns>
        public static int LastIndex<T>(this List<T> l)
            => l.Count - 1;

        /// <summary>
        /// Ensures that the <see cref="List{T}"/> contains a set <typeparamref name="T"/>.
        /// </summary>
        /// <param name="item">The <typeparamref name="T"/> to check for.</param>
        /// <param name="listName">The <see cref="List{T}"/> name to use if an exception is thrown.</param>
        /// <returns>The original <see cref="List{T}"/>.</returns>
        /// <exception cref="System.ArgumentException">If the <see cref="List{T}"/> does not contain
        /// <paramref name="item"/>.</exception>"
        public static List<T> MustContain<T>(this List<T> l, T item, string listName)
        {
            if (l.Contains(item))
                return
                    l;
            else
                throw
                    new System.ArgumentException(
                        string.Format("{0} must be present in List<{1}> {2}.", item, typeof(T), listName));
        }

        /// <summary>
        /// Ensures that the <see cref="List{T}"/> does not contain a set <typeparamref name="T"/>.
        /// </summary>
        /// <param name="item">The <typeparamref name="T"/> to check against.</param>
        /// <param name="listName">The <see cref="List{T}"/> name to use if an exception is thrown.</param>
        /// <returns>The original <see cref="List{T}"/>.</returns>
        /// <exception cref="System.ArgumentException">If the <see cref="List{T}"/> contains
        /// <paramref name="item"/>.</exception>"
        public static List<T> MustNotContain<T>(this List<T> l, T item, string listName)
        {
            if (!l.Contains(item))
                return
                    l;
            else
                throw
                    new System.ArgumentException(
                        string.Format("{0} must not be present in List<{1}> {2}.", item, typeof(T), listName));
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
