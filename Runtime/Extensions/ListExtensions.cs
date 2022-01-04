using System;
using System.Collections.Generic;

namespace StephanHooft.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Returns true if the <see cref="List{T}"/> is null or empty.
        /// </summary>
        /// <returns>True if the <see cref="List{T}"/> is null or empty.</returns>
        public static bool IsNullOrEmpty<T>(this List<T> list) 
            => list.Count == 0 || list == null;

        /// <summary>
        /// Counts the amount of items in the <see cref="List{T}"/> that match a certain <see cref="Predicate{T}"/>.
        /// </summary>
        /// <param name="predicate">The <see cref="Predicate{T}"/> to check against.</param>
        /// <returns>The amount of items in the <see cref="List{T}"/> that match the <paramref name="predicate"/>.</returns>
        public static int Count<T>(this List<T> list, Predicate<T> predicate)
        {
            int result = 0;
            foreach (T item in list)
                if (predicate(item)) 
                    result++;
            return result;
        }

        /// <summary>
        /// <para>The lowest-indexed item of a <see cref="List{T}"/></para>
        /// </summary>
        /// <returns>The last <typeparamref name="T"/> in the <see cref="List{T}"/>.</returns>
        public static T First<T>(this List<T> list) 
            => list[0];

        /// <summary>
        /// <para>The highest-indexed item of a <see cref="List{T}"/></para>
        /// </summary>
        /// <returns>The last <typeparamref name="T"/> in the <see cref="List{T}"/>.</returns>
        public static T Last<T>(this List<T> list) 
            => list[list.Count - 1];

        /// <summary>
        /// <para>Returns the highest index of a <see cref="List{T}"/></para>
        /// </summary>
        /// <returns>The <paramref name="offset"/> index of the last <typeparamref name="T"/> in the <see cref="List{T}"/>.</returns>
        public static int LastIndex<T>(this List<T> list) 
            => list.Count - 1;
    }
}
