using UnityEngine;

namespace StephanHooft.Variables
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> containing a <see cref="float"/> value.
    /// </summary>
    [CreateAssetMenu(fileName = "New FloatVariable", menuName = "Variables/FloatVariable", order = 2)]
    public class FloatVariable : ScriptableObject
    {
        #region Properties

        /// <summary>
        /// The <see cref="FloatVariable"/>'s current value.
        /// </summary>
    	public float Value => value;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

#if UNITY_EDITOR
        /// <summary>
        /// A description of the <see cref="FloatVariable"/> to be used in editor.
        /// </summary>
        [Multiline]
        public string description = "";
#endif

        [SerializeField]
        private float value;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// Sets the <see cref="FloatVariable"/>'s value.
        /// </summary>
        /// <param name="value">
        /// The <see cref="float"/> value to set.
        /// </param>
        public void SetValue(float value)
        {
            this.value = value;
        }

        /// <summary>
        /// Modifies the <see cref="FloatVariable"/>'s <see cref="float"/> value by a certain <paramref name="amount"/>.
        /// </summary>
        /// <param name="amount">
        /// The <see cref="float"/> amount by which to modify the <see cref="FloatVariable"/>'s value.
        /// </param>
        public void ApplyChange(float amount)
        {
            value += amount;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Operators

        /// <summary>
        /// Returns the <see cref="FloatVariable"/>'s value.
        /// </summary>
        public static implicit operator float(FloatVariable variable)
        {
            return
                variable.Value;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}