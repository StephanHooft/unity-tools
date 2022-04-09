using StephanHooft.Extensions;
using UnityEngine;

namespace StephanHooft.VariableRanges
{
    /// <summary>
    /// Class to add a min-max <see cref="PropertyAttribute"/> to a <see cref="FloatRange"/>.
    /// </summary>
    public class FloatRangeMinMaxAttribute : PropertyAttribute
    {
        #region Fields

        public float min;
        public float max;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructor

        /// <summary>
        /// Adds a min-max <see cref="PropertyAttribute"/> to a <see cref="FloatRange"/>.
        /// </summary>
        public FloatRangeMinMaxAttribute(float min, float max)
        {
            this.min = min;
            this.max = max.MustBeAbove(min, "max");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
