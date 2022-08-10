using System;

namespace StephanHooft.Variables
{
    /// <summary>
    /// A container for either a <see cref="double"/> value or a reference to a <see cref="DoubleVariable"/>.
    /// </summary>
    [Serializable]
    public class DoubleReference
    {
        #region Properties

        /// <summary>
        /// Returns the <see cref="DoubleReference"/>'s current value, which is either its own constant, or the current
        /// value of a referenced <see cref="DoubleVariable"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if no <see cref="DoubleReference"/> was set.
        /// </exception>
        public double Value => useLocalValue
            ? localValue
            : variable == null
                ? throw
                    new NullReferenceException(ReferenceNotSet)
                : variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        /// <summary>
        /// If true, the <see cref="DoubleReference"/> will use its local value over that of a (potentially) referenced
        /// <see cref="DoubleVariable"/>.
        /// </summary>
        public bool useLocalValue = true;

        /// <summary>
        /// The <see cref="DoubleReference"/>'s local value.
        /// </summary>
        public double localValue;

        /// <summary>
        /// The <see cref="DoubleReference"/>'s linked <see cref="DoubleVariable"/>, if any.
        /// </summary>
        public DoubleVariable variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructors

        /// <summary>
        /// Creates a new <see cref="DoubleReference"/>.
        /// </summary>
        public DoubleReference()
        { }

        /// <summary>
        /// Creates a new <see cref="DoubleReference"/>.
        /// </summary>
        /// <param name="value">
        /// The starting <see cref="double"/> value to assign to the new <see cref="DoubleReference"/>.
        /// </param>
        public DoubleReference(double value)
        {
            useLocalValue = true;
            localValue = value;
        }

        /// <summary>
        /// Creates a new <see cref="DoubleReference"/>.
        /// </summary>
        /// <param name="variable">
        /// The <see cref="DoubleVariable"/> to assign to the new <see cref="DoubleReference"/>.
        /// </param>
        public DoubleReference(DoubleVariable variable)
        {
            useLocalValue = false;
            this.variable = variable;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Operators

        /// <summary>
        /// Returns the <see cref="DoubleReference"/>'s value.
        /// </summary>
        public static implicit operator double(DoubleReference reference)
            => reference.Value;

        /// <summary>
        /// Returns the <see cref="DoubleReference"/>'s <see cref="DoubleVariable"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if no <see cref="DoubleReference"/> was set.
        /// </exception>
        public static implicit operator DoubleVariable(DoubleReference reference)
            => reference.variable == null
                ? throw
                    new InvalidOperationException(ReferenceNotSet)
                : reference.variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Error Messages

        private static string ReferenceNotSet
            => string.Format("{0} cannot return a {1} or its value; {1} not set.",
                typeof(DoubleReference).Name, typeof(DoubleVariable).Name);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
