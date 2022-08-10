using System;

namespace StephanHooft.Variables
{
    /// <summary>
    /// A container for either a <see cref="int"/> value or a reference to a <see cref="IntVariable"/>.
    /// </summary>
    [Serializable]
    public class IntReference
    {
        #region Properties

        /// <summary>
        /// Returns the <see cref="IntReference"/>'s current value, which is either its own constant, or the current
        /// value of a referenced <see cref="IntVariable"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if no <see cref="IntReference"/> was set.
        /// </exception>
        public int Value => useLocalValue
            ? localValue
            : variable == null
                ? throw
                    new NullReferenceException(ReferenceNotSet)
                : variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        /// <summary>
        /// If true, the <see cref="IntReference"/> will use its local value over that of a (potentially) referenced
        /// <see cref="IntVariable"/>.
        /// </summary>
        public bool useLocalValue = true;

        /// <summary>
        /// The <see cref="IntReference"/>'s local value.
        /// </summary>
        public int localValue;

        /// <summary>
        /// The <see cref="IntReference"/>'s linked <see cref="IntVariable"/>, if any.
        /// </summary>
        public IntVariable variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructors

        /// <summary>
        /// Creates a new <see cref="IntReference"/>.
        /// </summary>
        public IntReference()
        { }

        /// <summary>
        /// Creates a new <see cref="IntReference"/>.
        /// </summary>
        /// <param name="value">
        /// The starting <see cref="int"/> value to assign to the new <see cref="IntReference"/>.
        /// </param>
        public IntReference(int value)
        {
            useLocalValue = true;
            localValue = value;
        }

        /// <summary>
        /// Creates a new <see cref="IntReference"/>.
        /// </summary>
        /// <param name="variable">
        /// The <see cref="IntVariable"/> to assign to the new <see cref="IntReference"/>.
        /// </param>
        public IntReference(IntVariable variable)
        {
            useLocalValue = false;
            this.variable = variable;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Operators

        /// <summary>
        /// Returns the <see cref="IntReference"/>'s value.
        /// </summary>
        public static implicit operator int(IntReference reference)
            => reference.Value;

        /// <summary>
        /// Returns the <see cref="IntReference"/>'s <see cref="IntVariable"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if no <see cref="IntReference"/> was set.
        /// </exception>
        public static implicit operator IntVariable(IntReference reference)
            => reference.variable == null
                ? throw
                    new InvalidOperationException(ReferenceNotSet)
                : reference.variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Error Messages

        private static string ReferenceNotSet
            => string.Format("{0} cannot return a {1} or its value; {1} not set.",
                typeof(IntReference).Name, typeof(IntVariable).Name);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
