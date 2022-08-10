using UnityEngine;

namespace StephanHooft.Variables
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> containing a <see cref="Vector3"/> value.
    /// </summary>
    [CreateAssetMenu(fileName = "New Vector3Variable", menuName = "Variables/Vector3Variable", order = 2)]
    public class Vector3Variable : ScriptableObject
    {
        #region Properties

        /// <summary>
        /// The <see cref="Vector3Variable"/>'s current value.
        /// </summary>
    	public Vector3 Value => value;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

#if UNITY_EDITOR
        /// <summary>
        /// A description of the <see cref="Vector3Variable"/> to be used in editor.
        /// </summary>
        [Multiline]
        public string description = "";
#endif

        [SerializeField]
        private Vector3 value;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// Sets the <see cref="Vector3Variable"/>'s value.
        /// </summary>
        /// <param name="value">
        /// The <see cref="Vector3"/> value to set.
        /// </param>
        public void SetValue(Vector3 value)
        {
            this.value = value;
        }

        /// <summary>
        /// Modifies the <see cref="Vector3Variable"/>'s <see cref="Vector3"/> value by a certain <paramref name="amount"/>.
        /// </summary>
        /// <param name="amount">
        /// The <see cref="Vector3"/> amount by which to modify the <see cref="Vector3Variable"/>'s value.
        /// </param>
        public void ApplyChange(Vector3 amount)
        {
            value += amount;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Operators

        /// <summary>
        /// Returns the <see cref="Vector3Variable"/>'s value.
        /// </summary>
        public static implicit operator Vector3(Vector3Variable variable)
        {
            return
                variable.Value;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}