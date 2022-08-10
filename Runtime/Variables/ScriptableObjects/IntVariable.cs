using UnityEngine;

namespace StephanHooft.Variables
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> containing a <see cref="int"/> value.
    /// </summary>
    [CreateAssetMenu(fileName = "New IntVariable", menuName = "Variables/IntVariable", order = 2)]
    public class IntVariable : ScriptableObject
    {
        #region Properties

        /// <summary>
        /// The <see cref="IntVariable"/>'s current value.
        /// </summary>
    	public int Value => value;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

#if UNITY_EDITOR
        /// <summary>
        /// A description of the <see cref="IntVariable"/> to be used in editor.
        /// </summary>
        [Multiline]
        public string description = "";
#endif

        [SerializeField]
        private int value;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// Sets the <see cref="IntVariable"/>'s value.
        /// </summary>
        /// <param name="value">
        /// The <see cref="int"/> value to set.
        /// </param>
        public void SetValue(int value)
        {
            this.value = value;
        }

        /// <summary>
        /// Modifies the <see cref="IntVariable"/>'s <see cref="int"/> value by a certain <paramref name="amount"/>.
        /// </summary>
        /// <param name="amount">
        /// The <see cref="int"/> amount by which to modify the <see cref="IntVariable"/>'s value.
        /// </param>
        public void ApplyChange(int amount)
        {
            value += amount;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Operators

        /// <summary>
        /// Returns the <see cref="IntVariable"/>'s value.
        /// </summary>
        public static implicit operator int(IntVariable variable)
        {
            return
                variable.Value;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}