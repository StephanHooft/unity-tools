using UnityEngine;
using StephanHooft.EditorSafe;
using System;

namespace StephanHooft.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Object"/>.
    /// </summary>
    public static class ObjectExtensions
    {
        #region Static Methods

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
        /// Throws an <see cref="ArgumentNullException"/> if the <typeparamref name="T"/> is null. Returns the <typeparamref name="T"/> otherwise.
        /// </summary>
        /// <param name="paramName">The parameter name to use if an <see cref="ArgumentNullException"/> is thrown.</param>
        /// <returns>The <typeparamref name="T"/>.</returns>
        public static T MustNotBeNull<T>(this T obj, string paramName)
        {
            if (obj == null)
                throw
                    new ArgumentNullException(paramName);
            return
                obj;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the <typeparamref name="T"/> is null.
        /// <para><paramref name="destroyIfNull"/> will also be destroyed if null.</para>
        /// </summary>
        /// <param name="destroyIfNull">The <see cref="GameObject"/> to destroy if the <typeparamref name="T"/> reference is null.</param>
        /// <returns>The <typeparamref name="T"/>.</returns>
        public static T MustNotBeNull<T>(this T obj, GameObject destroyIfNull)
        {
            if (destroyIfNull == null) 
                throw
                    new ArgumentNullException("objectToDestroyIfNull");
            if (obj == null)
            {
                EditModeSafe.Destroy(destroyIfNull);
                throw
                    new ArgumentNullException(string.Format("{0} is null.", obj.GetType().ToString()));
            }
            return
                obj;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the <typeparamref name="T"/> is null.
        /// <para><paramref name="destroyIfNull"/> will also be destroyed if null.</para>
        /// </summary>
        /// <param name="destroyIfNull">The <see cref="GameObject"/> to destroy if the <typeparamref name="T"/> reference is null.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="ArgumentNullException"/> is thrown.</param>
        /// <returns>The <typeparamref name="T"/>.</returns>
        public static T MustNotBeNull<T>(this T obj, GameObject destroyIfNull, string paramName)
        {
            if (destroyIfNull == null)
                throw
                    new ArgumentNullException("objectToDestroyIfNull");
            if (obj == null)
            {
                EditModeSafe.Destroy(destroyIfNull);
                throw
                    new ArgumentNullException(string.Format("{0} is null.", paramName));
            }
            return
                obj;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
