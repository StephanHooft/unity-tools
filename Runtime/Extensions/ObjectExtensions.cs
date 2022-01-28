using System;
using UnityEngine;

namespace StephanHooft.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Checks whether the <see cref="object"/> is Null.
        /// </summary>
        /// <returns>True if the <see cref="object"/> is Null.</returns>
        public static bool IsNull(this object obj)
        {
            return
                obj == null;
        }

        /// <summary>
        /// Returns true if the <see cref="object"/> is of <see cref="Type"/> <typeparamref name="T"/>.
        /// </summary>
        /// <returns>True if the object is a <typeparamref name="T"/>, false otherwise.</returns>
        public static bool IsOfType<T>(this object obj)
        {
            return
                obj is T;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the <typeparamref name="T"/> is null. Returns the <typeparamref name="T"/> otherwise.
        /// </summary>
        /// <returns>The <typeparamref name="T"/>.</returns>
        public static T MustNotBeNull<T>(this T obj)
        {
            if (obj == null) 
                throw
                    new ArgumentNullException(obj.GetType().ToString());
            return
                obj;
        }

        /// <summary>
        /// <para>Throws an <see cref="Exception"/> if the <typeparamref name="T"/> is null.</para>
        /// <para><paramref name="objectToDestroyIfNull"/> will also be destroyed if null.</para>
        /// </summary>
        /// <param name="objectToDestroyIfNull">The <see cref="GameObject"/> to destroy if the <typeparamref name="T"/> reference is null.</param>
        /// <returns>The <typeparamref name="T"/>.</returns>
        public static T MustNotBeNull<T>(this T obj, GameObject objectToDestroyIfNull)
        {
            if (objectToDestroyIfNull == null) 
                throw
                    new ArgumentNullException("objectToDestroyIfNull");
            if (obj == null)
            {
                UnityEngine.Object.Destroy(objectToDestroyIfNull);
                throw
                    new Exception(string.Format("{0} is null.", obj.GetType()));
            }
            return
                obj;
        }
    }
}
