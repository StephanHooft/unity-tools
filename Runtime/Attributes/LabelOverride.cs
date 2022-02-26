using UnityEngine;

namespace StephanHooft.Attributes
{
    /// <summary>
    /// Class to override the inspector label of a field.
    /// <remarks><para>Borrowed from: https://answers.unity.com/questions/1005277/can-i-change-variable-name-on-inspector.html .</para></remarks>
    /// </summary>
    public class LabelOverride : PropertyAttribute
    {
        #region Fields

        public string label;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion

        #region Constructor

        /// <summary>
        /// Overrides the inspector label of a <see cref="string"/> field.
        /// </summary>
        /// <param name="label">The label <see cref="string"/> to show in the inspector.</param>
        public LabelOverride(string label)
        {
            if (string.IsNullOrEmpty(label))
                throw
                    new System.ArgumentException("label");
            this.label = label;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion

    }
}
