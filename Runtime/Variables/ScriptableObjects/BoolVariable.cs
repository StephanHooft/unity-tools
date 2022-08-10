using UnityEngine;

namespace StephanHooft.Variables
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> containing a <see cref="bool"/> value.
    /// </summary>
    [CreateAssetMenu(fileName = "New BoolVariable", menuName = "Variables/BoolVariable", order = 2)]
    public class BoolVariable : ScriptableObject
    {
        #region Properties

        /// <summary>
        /// The <see cref="BoolVariable"/>'s current value.
        /// </summary>
    	public bool Value => value;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

#if UNITY_EDITOR
        /// <summary>
        /// A description of the <see cref="BoolVariable"/> to be used in editor.
        /// </summary>
        [Multiline]
        public string description = "";
#endif

        [SerializeField]
        private bool value;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// Sets the <see cref="BoolVariable"/>'s value.
        /// </summary>
        /// <param name="value">
        /// The <see cref="bool"/> value to set.
        /// </param>
        public void SetValue(bool value)
        {
            this.value = value;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Operators

        /// <summary>
        /// Returns the <see cref="BoolVariable"/>'s value.
        /// </summary>
        public static implicit operator bool(BoolVariable variable)
        {
            return
                variable.Value;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}