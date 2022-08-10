using System;

namespace StephanHooft.Variables
{
    /// <summary>
    /// A container for either a <see cref="float"/> value or a reference to a <see cref="FloatVariable"/>.
    /// </summary>
    [Serializable]
    public class FloatReference
    {
        #region Properties

        /// <summary>
        /// Returns the <see cref="FloatReference"/>'s current value, which is either its own constant, or the current
        /// value of a referenced <see cref="FloatVariable"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if no <see cref="FloatReference"/> was set.
        /// </exception>
        public float Value => useLocalValue
            ? localValue
            : variable == null
                ? throw
                    new NullReferenceException(ReferenceNotSet)
                : variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        /// <summary>
        /// If true, the <see cref="FloatReference"/> will use its local value over that of a (potentially) referenced
        /// <see cref="FloatVariable"/>.
        /// </summary>
        public bool useLocalValue = true;

        /// <summary>
        /// The <see cref="FloatReference"/>'s local value.
        /// </summary>
        public float localValue;

        /// <summary>
        /// The <see cref="FloatReference"/>'s linked <see cref="FloatVariable"/>, if any.
        /// </summary>
        public FloatVariable variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructors

        /// <summary>
        /// Creates a new <see cref="FloatReference"/>.
        /// </summary>
        public FloatReference()
        { }

        /// <summary>
        /// Creates a new <see cref="FloatReference"/>.
        /// </summary>
        /// <param name="value">
        /// The starting <see cref="float"/> value to assign to the new <see cref="FloatReference"/>.
        /// </param>
        public FloatReference(float value)
        {
            useLocalValue = true;
            localValue = value;
        }

        /// <summary>
        /// Creates a new <see cref="FloatReference"/>.
        /// </summary>
        /// <param name="variable">
        /// The <see cref="FloatVariable"/> to assign to the new <see cref="FloatReference"/>.
        /// </param>
        public FloatReference(FloatVariable variable)
        {
            useLocalValue = false;
            this.variable = variable;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Operators

        /// <summary>
        /// Returns the <see cref="FloatReference"/>'s value.
        /// </summary>
        public static implicit operator float(FloatReference reference)
            => reference.Value;

        /// <summary>
        /// Returns the <see cref="FloatReference"/>'s <see cref="FloatVariable"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if no <see cref="FloatReference"/> was set.
        /// </exception>
        public static implicit operator FloatVariable(FloatReference reference)
            => reference.variable == null 
                ? throw 
                    new InvalidOperationException(ReferenceNotSet) 
                : reference.variable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Error Messages

        private static string ReferenceNotSet
            => string.Format("{0} cannot return a {1} or its value; {1} not set.",
                typeof(FloatReference).Name, typeof(FloatVariable).Name);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
