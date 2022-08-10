using UnityEngine;

namespace StephanHooft.Variables
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> containing a <see cref="string"/> value.
    /// </summary>
    [CreateAssetMenu(fileName = "New StringVariable", menuName = "Variables/StringVariable", order = 2)]
    public class StringVariable : ScriptableObject
    {
        #region Properties

        /// <summary>
        /// The <see cref="StringVariable"/>'s current value.
        /// </summary>
    	public string Value => value;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

#if UNITY_EDITOR
        /// <summary>
        /// A description of the <see cref="StringVariable"/> to be used in editor.
        /// </summary>
        [Multiline]
        public string description = "";
#endif

        [SerializeField]
        private string value;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// Sets the <see cref="StringVariable"/>'s value.
        /// </summary>
        /// <param name="value">
        /// The <see cref="string"/> value to set.
        /// </param>
        public void SetValue(string value)
        {
            this.value = value;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Operators

        /// <summary>
        /// Returns the <see cref="StringVariable"/>'s value.
        /// </summary>
        public static implicit operator string(StringVariable variable)
        {
            return
                variable.Value;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}