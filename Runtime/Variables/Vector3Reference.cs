using System;
using UnityEngine;

namespace StephanHooft.Variables
{
    /// <summary>
    /// A container for either a <see cref="Vector3"/> value or a reference to a <see cref="Vector3Variable"/>.
    /// </summary>
    [Serializable]
    public class Vector3Reference
    {
        #region Properties

        /// <summary>
        /// Returns the <see cref="Vector3Reference"/>'s current value, which is either its own constant, or the current
        /// value of a referenced <see cref="Vector3Variable"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if no <see cref="Vector3Reference"/> was set.
        /// </exception>
        public Vector3 Value => useLocalValue
            ? localValue
            : variable == null
                ? throw
                    new NullReferenceException(ReferenceNotSet)
                : variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        /// <summary>
        /// If true, the <see cref="Vector3Reference"/> will use its local value over that of a (potentially) referenced
        /// <see cref="Vector3Variable"/>.
        /// </summary>
        public bool useLocalValue = true;

        /// <summary>
        /// The <see cref="Vector3Reference"/>'s local value.
        /// </summary>
        public Vector3 localValue;

        /// <summary>
        /// The <see cref="Vector3Reference"/>'s linked <see cref="Vector3Variable"/>, if any.
        /// </summary>
        public Vector3Variable variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructors

        /// <summary>
        /// Creates a new <see cref="Vector3Reference"/>.
        /// </summary>
        public Vector3Reference()
        { }

        /// <summary>
        /// Creates a new <see cref="Vector3Reference"/>.
        /// </summary>
        /// <param name="value">
        /// The starting <see cref="Vector3"/> value to assign to the new <see cref="Vector3Reference"/>.
        /// </param>
        public Vector3Reference(Vector3 value)
        {
            useLocalValue = true;
            localValue = value;
        }

        /// <summary>
        /// Creates a new <see cref="Vector3Reference"/>.
        /// </summary>
        /// <param name="variable">
        /// The <see cref="Vector3Variable"/> to assign to the new <see cref="Vector3Reference"/>.
        /// </param>
        public Vector3Reference(Vector3Variable variable)
        {
            useLocalValue = false;
            this.variable = variable;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Operators

        /// <summary>
        /// Returns the <see cref="Vector3Reference"/>'s value.
        /// </summary>
        public static implicit operator Vector3(Vector3Reference reference)
            => reference.Value;

        /// <summary>
        /// Returns the <see cref="Vector3Reference"/>'s <see cref="Vector3Variable"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if no <see cref="Vector3Reference"/> was set.
        /// </exception>
        public static implicit operator Vector3Variable(Vector3Reference reference)
            => reference.variable == null
                ? throw
                    new InvalidOperationException(ReferenceNotSet)
                : reference.variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Error Messages

        private static string ReferenceNotSet
            => string.Format("{0} cannot return a {1} or its value; {1} not set.",
                typeof(Vector3Reference).Name, typeof(Vector3Variable).Name);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
