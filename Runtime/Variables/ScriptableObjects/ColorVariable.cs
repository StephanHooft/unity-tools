using UnityEngine;

namespace StephanHooft.Variables
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> containing a <see cref="Color"/> value.
    /// </summary>
    [CreateAssetMenu(fileName = "New ColorVariable", menuName = "Variables/ColorVariable", order = 2)]
    public class ColorVariable : ScriptableObject
    {
        #region Properties

        /// <summary>
        /// The <see cref="ColorVariable"/>'s current value.
        /// </summary>
    	public Color Value => value;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

#if UNITY_EDITOR
        /// <summary>
        /// A description of the <see cref="ColorVariable"/> to be used in editor.
        /// </summary>
        [Multiline]
        public string description = "";
#endif

        [SerializeField]
        private Color value;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// Sets the <see cref="ColorVariable"/>'s value.
        /// </summary>
        /// <param name="value">
        /// The <see cref="Color"/> value to set.
        /// </param>
        public void SetValue(Color value)
        {
            this.value = value;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Operators

        /// <summary>
        /// Returns the <see cref="ColorVariable"/>'s value.
        /// </summary>
        public static implicit operator Color(ColorVariable variable)
        {
            return
                variable.Value;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}