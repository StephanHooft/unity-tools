using System;

namespace StephanHooft.Variables
{
    /// <summary>
    /// A container for either a <see cref="bool"/> value or a reference to a <see cref="BoolVariable"/>.
    /// </summary>
    [Serializable]
    public class BoolReference
    {
        #region Properties

        /// <summary>
        /// Returns the <see cref="BoolReference"/>'s current value, which is either its own constant, or the current
        /// value of a referenced <see cref="BoolVariable"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if no <see cref="BoolReference"/> was set.
        /// </exception>
        public bool Value => useLocalValue
            ? localValue
            : variable == null
                ? throw
                    new NullReferenceException(ReferenceNotSet)
                : variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        /// <summary>
        /// If true, the <see cref="BoolReference"/> will use its local value over that of a (potentially) referenced
        /// <see cref="BoolVariable"/>.
        /// </summary>
        public bool useLocalValue = true;

        /// <summary>
        /// The <see cref="BoolReference"/>'s local value.
        /// </summary>
        public bool localValue;

        /// <summary>
        /// The <see cref="BoolReference"/>'s linked <see cref="BoolVariable"/>, if any.
        /// </summary>
        public BoolVariable variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructors

        /// <summary>
        /// Creates a new <see cref="BoolReference"/>.
        /// </summary>
        public BoolReference()
        { }

        /// <summary>
        /// Creates a new <see cref="BoolReference"/>.
        /// </summary>
        /// <param name="value">
        /// The starting <see cref="bool"/> value to assign to the new <see cref="BoolReference"/>.
        /// </param>
        public BoolReference(bool value)
        {
            useLocalValue = true;
            localValue = value;
        }

        /// <summary>
        /// Creates a new <see cref="BoolReference"/>.
        /// </summary>
        /// <param name="variable">
        /// The <see cref="BoolVariable"/> to assign to the new <see cref="BoolReference"/>.
        /// </param>
        public BoolReference(BoolVariable variable)
        {
            useLocalValue = false;
            this.variable = variable;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Operators

        /// <summary>
        /// Returns the <see cref="BoolReference"/>'s value.
        /// </summary>
        public static implicit operator bool(BoolReference reference)
            => reference.Value;

        /// <summary>
        /// Returns the <see cref="BoolReference"/>'s <see cref="BoolVariable"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if no <see cref="BoolReference"/> was set.
        /// </exception>
        public static implicit operator BoolVariable(BoolReference reference)
            => reference.variable == null
                ? throw
                    new InvalidOperationException(ReferenceNotSet)
                : reference.variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Error Messages

        private static string ReferenceNotSet
            => string.Format("{0} cannot return a {1} or its value; {1} not set.",
                typeof(BoolReference).Name, typeof(BoolVariable).Name);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
