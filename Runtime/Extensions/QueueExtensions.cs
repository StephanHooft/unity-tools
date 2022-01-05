using System.Collections.Generic;

namespace StephanHooft.Extensions
{
    public static class QueueExtensions
    {
        /// <summary>
        /// Returns true if the <see cref="Queue{T}"/> has no items.
        /// </summary>
        /// <returns>True if the <see cref="Queue{T}"/> has no <typeparamref name="T"/>s.</returns>
        public static bool IsEmpty<T>(this Queue<T> queue) => queue.Count == 0;

        /// <summary>
        /// Returns true if the <see cref="Queue{T}"/> is null or empty.
        /// </summary>
        /// <returns>True if the <see cref="Queue{T}"/> is null or empty.</returns>
        public static bool IsNullOrEmpty<T>(this Queue<T> queue)
            => queue.Count == 0 || queue == null;
    }
}
