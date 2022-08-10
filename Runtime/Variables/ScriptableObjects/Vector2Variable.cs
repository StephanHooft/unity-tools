using UnityEngine;

namespace StephanHooft.Variables
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> containing a <see cref="Vector2"/> value.
    /// </summary>
    [CreateAssetMenu(fileName = "New Vector2Variable", menuName = "Variables/Vector2Variable", order = 2)]
    public class Vector2Variable : ScriptableObject
    {
        #region Properties

        /// <summary>
        /// The <see cref="Vector2Variable"/>'s current value.
        /// </summary>
    	public Vector2 Value => value;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

#if UNITY_EDITOR
        /// <summary>
        /// A description of the <see cref="Vector2Variable"/> to be used in editor.
        /// </summary>
        [Multiline]
        public string description = "";
#endif

        [SerializeField]
        private Vector2 value;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// Sets the <see cref="Vector2Variable"/>'s value.
        /// </summary>
        /// <param name="value">
        /// The <see cref="Vector2"/> value to set.
        /// </param>
        public void SetValue(Vector2 value)
        {
            this.value = value;
        }

        /// <summary>
        /// Modifies the <see cref="Vector2Variable"/>'s <see cref="Vector2"/> value by a certain <paramref name="amount"/>.
        /// </summary>
        /// <param name="amount">
        /// The <see cref="Vector2"/> amount by which to modify the <see cref="Vector2Variable"/>'s value.
        /// </param>
        public void ApplyChange(Vector2 amount)
        {
            value += amount;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Operators

        /// <summary>
        /// Returns the <see cref="Vector2Variable"/>'s value.
        /// </summary>
        public static implicit operator Vector2(Vector2Variable variable)
        {
            return
                variable.Value;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}