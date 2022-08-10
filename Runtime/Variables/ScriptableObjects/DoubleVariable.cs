using UnityEngine;

namespace StephanHooft.Variables
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> containing a <see cref="double"/> value.
    /// </summary>
    [CreateAssetMenu(fileName = "New DoubleVariable", menuName = "Variables/DoubleVariable", order = 2)]
    public class DoubleVariable : ScriptableObject
    {
        #region Properties

        /// <summary>
        /// The <see cref="DoubleVariable"/>'s current value.
        /// </summary>
    	public double Value => value;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

#if UNITY_EDITOR
        /// <summary>
        /// A description of the <see cref="DoubleVariable"/> to be used in editor.
        /// </summary>
        [Multiline]
        public string description = "";
#endif

        [SerializeField]
        private double value;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// Sets the <see cref="DoubleVariable"/>'s value.
        /// </summary>
        /// <param name="value">
        /// The <see cref="double"/> value to set.
        /// </param>
        public void SetValue(double value)
        {
            this.value = value;
        }

        /// <summary>
        /// Modifies the <see cref="DoubleVariable"/>'s <see cref="double"/> value by a certain <paramref name="amount"/>.
        /// </summary>
        /// <param name="amount">
        /// The <see cref="double"/> amount by which to modify the <see cref="DoubleVariable"/>'s value.
        /// </param>
        public void ApplyChange(double amount)
        {
            value += amount;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Operators

        /// <summary>
        /// Returns the <see cref="DoubleVariable"/>'s value.
        /// </summary>
        public static implicit operator double(DoubleVariable variable)
        {
            return
                variable.Value;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}