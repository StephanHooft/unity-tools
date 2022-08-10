using System;
using UnityEngine;

namespace StephanHooft.Variables
{
    /// <summary>
    /// A container for either a <see cref="Color"/> value or a reference to a <see cref="ColorVariable"/>.
    /// </summary>
    [Serializable]
    public class ColorReference
    {
        #region Properties

        /// <summary>
        /// Returns the <see cref="ColorReference"/>'s current value, which is either its own constant, or the current
        /// value of a referenced <see cref="ColorVariable"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if no <see cref="ColorReference"/> was set.
        /// </exception>
        public Color Value => useLocalValue
            ? localValue
            : variable == null
                ? throw
                    new NullReferenceException(ReferenceNotSet)
                : variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        /// <summary>
        /// If true, the <see cref="ColorReference"/> will use its local value over that of a (potentially) referenced
        /// <see cref="ColorVariable"/>.
        /// </summary>
        public bool useLocalValue = true;

        /// <summary>
        /// The <see cref="ColorReference"/>'s local value.
        /// </summary>
        public Color localValue;

        /// <summary>
        /// The <see cref="ColorReference"/>'s linked <see cref="ColorVariable"/>, if any.
        /// </summary>
        public ColorVariable variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructors

        /// <summary>
        /// Creates a new <see cref="ColorReference"/>.
        /// </summary>
        public ColorReference()
        { }

        /// <summary>
        /// Creates a new <see cref="ColorReference"/>.
        /// </summary>
        /// <param name="value">
        /// The starting <see cref="Color"/> value to assign to the new <see cref="ColorReference"/>.
        /// </param>
        public ColorReference(Color value)
        {
            useLocalValue = true;
            localValue = value;
        }

        /// <summary>
        /// Creates a new <see cref="ColorReference"/>.
        /// </summary>
        /// <param name="variable">
        /// The <see cref="ColorVariable"/> to assign to the new <see cref="ColorReference"/>.
        /// </param>
        public ColorReference(ColorVariable variable)
        {
            useLocalValue = false;
            this.variable = variable;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Operators

        /// <summary>
        /// Returns the <see cref="ColorReference"/>'s value.
        /// </summary>
        public static implicit operator Color(ColorReference reference)
            => reference.Value;

        /// <summary>
        /// Returns the <see cref="ColorReference"/>'s <see cref="ColorVariable"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if no <see cref="ColorReference"/> was set.
        /// </exception>
        public static implicit operator ColorVariable(ColorReference reference)
            => reference.variable == null
                ? throw
                    new InvalidOperationException(ReferenceNotSet)
                : reference.variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Error Messages

        private static string ReferenceNotSet
            => string.Format("{0} cannot return a {1} or its value; {1} not set.",
                typeof(ColorReference).Name, typeof(ColorVariable).Name);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
