using System.Collections.Generic;

namespace StephanHooft.Extensions
{
    /// <summary>
    /// Extension methods for arrays.
    /// </summary>
    public static class ArrayExtensions
    {
        #region Static Methods

        /// <summary>
        /// Checks whether a specific <paramref name="item"/> appears in the array.
        /// </summary>
        /// <param name="item">The item to check the array for.</param>
        /// <returns><see cref="true"/> if the array contains <paramref name="item"/>.</returns>
        public static bool Contains<T>(this T[] a, T item)
        {
            foreach (T entry in a)
                if (entry.Equals(item))
                    return
                        true;
            return
                false;
        }

        /// <summary>
        /// Returns the lowest-indexed item of the array.
        /// </summary>
        /// <returns>The last <typeparamref name="T"/> in the array.</returns>
        public static T First<T>(this T[] a)
            => a[0];

        /// <summary>
        /// Returns <see cref="true"/> if the array is empty.
        /// </summary>
        /// <returns><see cref="true"/> if the array is empty.</returns>
        public static bool IsEmpty<T>(this T[] a)
            => a.Length == 0;

        /// <summary>
        /// Returns <see cref="true"/> if the array is <see cref="null"/> or empty.
        /// </summary>
        /// <returns><see cref="true"/> if the array is <see cref="null"/> or empty.</returns>
        public static bool IsNullOrEmpty<T>(this T[] a)
            => a.Length == 0 || a == null;

        /// <summary>
        /// Returns te highest-indexed item of the array.
        /// </summary>
        /// <returns>The last <typeparamref name="T"/> in the array.</returns>
        public static T Last<T>(this T[] a)
            => a[a.Length - 1];

        /// <summary>
        /// Returns the highest index of the array.
        /// </summary>
        /// <returns>The <paramref name="offset"/> index of the last <typeparamref name="T"/> in the
        /// <see cref="List{T}"/>.</returns>
        public static int LastIndex<T>(this T[] a)
            => a.Length - 1;

        /// <summary>
        /// Ensures that the array contains a set <typeparamref name="T"/>.
        /// </summary>
        /// <param name="item">The <typeparamref name="T"/> to check for.</param>
        /// <param name="arrayName">The array name to use if an exception is thrown.</param>
        /// <returns>The original array.</returns>
        /// <exception cref="System.ArgumentException">If no <typeparamref name="T"/> is contained.</exception>
        public static T[] MustContain<T>(this T[] a, T item, string arrayName)
        {
            foreach (T entry in a)
                if (entry.Equals(item))
                    return
                        a;
            throw
                new System.ArgumentException(
                    string.Format("{0} must be present in {1}[] {2}.", item, typeof(T), arrayName));
        }

        /// <summary>
        /// Ensures that the array does not contain a set <typeparamref name="T"/>.
        /// </summary>
        /// <param name="item">The <typeparamref name="T"/> to check against.</param>
        /// <param name="arrayName">The array name to use if an exception is thrown.</param>
        /// <returns>The original array.</returns>
        /// <exception cref="System.ArgumentException">If a <typeparamref name="T"/> is contained.</exception>
        public static T[] MustNotContain<T>(this T[] a, T item, string arrayName)
        {
            foreach (T entry in a)
                if (entry.Equals(item))
                    throw
                        new System.ArgumentException(
                            string.Format("{0} must not be present in {1}[] {2}.", item, typeof(T), arrayName));
            return
                a;
        }

        /// <summary>
        /// Converts an array to a <see cref="List{T}"/>.
        /// </summary>
        /// <returns>A <see cref="List{T}"/>.</returns>
        public static List<T> ToList<T>(this T[] a)
        {
            List<T> output = new List<T>();
            output.AddRange(a);
            return
                output;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
