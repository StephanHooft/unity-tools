using System.Collections.Generic;

namespace StephanHooft.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Check whether a specific <paramref name="item"/> appears in the <see cref="System.Array"/>.
        /// </summary>
        /// <param name="item">The item to check the <see cref="System.Array"/> for.</param>
        /// <returns>True if the <see cref="System.Array"/> contains <paramref name="item"/>.</returns>
        public static bool Contains<T>(this T[] array, T item)
        {
            foreach (T entry in array)
                if (entry.Equals(item))
                    return true;
            return false;
        }

        /// <summary>
        /// Returns true if the <see cref="System.Array"/> is null or empty.
        /// </summary>
        /// <returns>True if the <see cref="System.Array"/> is null or empty.</returns>
        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array.Length == 0 || array == null;
        }

        /// <summary>
        /// Converts an <see cref="System.Array"/> to a <see cref="List{T}"/>.
        /// </summary>
        /// <returns>A <see cref="List{T}"/>.</returns>
        public static List<T> ToList<T>(this T[] array)
        {
            List<T> output = new List<T>();
            output.AddRange(array);
            return output;
        }
    }
}
