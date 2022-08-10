using System;
using UnityEngine;

namespace StephanHooft.Variables
{
    /// <summary>
    /// A container for either a <see cref="Vector2"/> value or a reference to a <see cref="Vector2Variable"/>.
    /// </summary>
    [Serializable]
    public class Vector2Reference
    {
        #region Properties

        /// <summary>
        /// Returns the <see cref="Vector2Reference"/>'s current value, which is either its own constant, or the current
        /// value of a referenced <see cref="Vector2Variable"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if no <see cref="Vector2Reference"/> was set.
        /// </exception>
        public Vector2 Value => useLocalValue
            ? localValue
            : variable == null
                ? throw
                    new NullReferenceException(ReferenceNotSet)
                : variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        /// <summary>
        /// If true, the <see cref="Vector2Reference"/> will use its local value over that of a (potentially) referenced
        /// <see cref="Vector2Variable"/>.
        /// </summary>
        public bool useLocalValue = true;

        /// <summary>
        /// The <see cref="Vector2Reference"/>'s local value.
        /// </summary>
        public Vector2 localValue;

        /// <summary>
        /// The <see cref="Vector2Reference"/>'s linked <see cref="Vector2Variable"/>, if any.
        /// </summary>
        public Vector2Variable variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructors

        /// <summary>
        /// Creates a new <see cref="Vector2Reference"/>.
        /// </summary>
        public Vector2Reference()
        { }

        /// <summary>
        /// Creates a new <see cref="Vector2Reference"/>.
        /// </summary>
        /// <param name="value">
        /// The starting <see cref="Vector2"/> value to assign to the new <see cref="Vector2Reference"/>.
        /// </param>
        public Vector2Reference(Vector2 value)
        {
            useLocalValue = true;
            localValue = value;
        }

        /// <summary>
        /// Creates a new <see cref="Vector2Reference"/>.
        /// </summary>
        /// <param name="variable">
        /// The <see cref="Vector2Variable"/> to assign to the new <see cref="Vector2Reference"/>.
        /// </param>
        public Vector2Reference(Vector2Variable variable)
        {
            useLocalValue = false;
            this.variable = variable;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Operators

        /// <summary>
        /// Returns the <see cref="Vector2Reference"/>'s value.
        /// </summary>
        public static implicit operator Vector2(Vector2Reference reference)
            => reference.Value;

        /// <summary>
        /// Returns the <see cref="Vector2Reference"/>'s <see cref="Vector2Variable"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if no <see cref="Vector2Reference"/> was set.
        /// </exception>
        public static implicit operator Vector2Variable(Vector2Reference reference)
            => reference.variable == null
                ? throw
                    new InvalidOperationException(ReferenceNotSet)
                : reference.variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Error Messages

        private static string ReferenceNotSet
            => string.Format("{0} cannot return a {1} or its value; {1} not set.",
                typeof(Vector2Reference).Name, typeof(Vector2Variable).Name);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
