using System;

namespace StephanHooft.Variables
{
    /// <summary>
    /// A container for either a <see cref="string"/> value or a reference to a <see cref="StringVariable"/>.
    /// </summary>
    [Serializable]
    public class StringReference
    {
        #region Properties

        /// <summary>
        /// Returns the <see cref="StringReference"/>'s current value, which is either its own constant, or the current
        /// value of a referenced <see cref="StringVariable"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if no <see cref="StringReference"/> was set.
        /// </exception>
        public string Value => useLocalValue
            ? localValue
            : variable == null
                ? throw
                    new NullReferenceException(ReferenceNotSet)
                : variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        /// <summary>
        /// If true, the <see cref="StringReference"/> will use its local value over that of a (potentially) referenced
        /// <see cref="StringVariable"/>.
        /// </summary>
        public bool useLocalValue = true;

        /// <summary>
        /// The <see cref="StringReference"/>'s local value.
        /// </summary>
        public string localValue;

        /// <summary>
        /// The <see cref="StringReference"/>'s linked <see cref="StringVariable"/>, if any.
        /// </summary>
        public StringVariable variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructors

        /// <summary>
        /// Creates a new <see cref="StringReference"/>.
        /// </summary>
        public StringReference()
        { }

        /// <summary>
        /// Creates a new <see cref="StringReference"/>.
        /// </summary>
        /// <param name="value">
        /// The starting <see cref="string"/> value to assign to the new <see cref="StringReference"/>.
        /// </param>
        public StringReference(string value)
        {
            useLocalValue = true;
            localValue = value;
        }

        /// <summary>
        /// Creates a new <see cref="StringReference"/>.
        /// </summary>
        /// <param name="variable">
        /// The <see cref="StringVariable"/> to assign to the new <see cref="StringReference"/>.
        /// </param>
        public StringReference(StringVariable variable)
        {
            useLocalValue = false;
            this.variable = variable;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Operators

        /// <summary>
        /// Returns the <see cref="StringReference"/>'s value.
        /// </summary>
        public static implicit operator string(StringReference reference)
            => reference.Value;

        /// <summary>
        /// Returns the <see cref="StringReference"/>'s <see cref="StringVariable"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if no <see cref="StringReference"/> was set.
        /// </exception>
        public static implicit operator StringVariable(StringReference reference)
            => reference.variable == null
                ? throw
                    new InvalidOperationException(ReferenceNotSet)
                : reference.variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Error Messages

        private static string ReferenceNotSet
            => string.Format("{0} cannot return a {1} or its value; {1} not set.",
                typeof(StringReference).Name, typeof(StringVariable).Name);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
