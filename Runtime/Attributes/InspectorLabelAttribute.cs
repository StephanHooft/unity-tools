using UnityEngine;

namespace StephanHooft.Attributes
{
    /// <summary>
    /// Class to override the inspector label of a field.
    /// </summary>
    public class InspectorLabelAttribute : PropertyAttribute
    {
        #region Properties

        /// <summary>
        /// The label <see cref="string"/> to show in the inspector.
        /// </summary>
        public string Label { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructor

        /// <summary>
        /// Overrides the inspector label of a <see cref="string"/> field.
        /// <para>Doesn't work on arrays, lists and other collections.</para>
        /// </summary>
        /// <param name="label">The label <see cref="string"/> to show in the inspector.</param>
        public InspectorLabelAttribute(string label)
        {
            if (string.IsNullOrEmpty(label))
                throw
                    new System.ArgumentException("label");
            Label = label;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
