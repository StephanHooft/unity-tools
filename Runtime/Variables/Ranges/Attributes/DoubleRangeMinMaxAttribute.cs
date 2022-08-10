using StephanHooft.Extensions;
using UnityEngine;

namespace StephanHooft.Variables.Ranges
{
    /// <summary>
    /// Class to add a min-max <see cref="PropertyAttribute"/> to a <see cref="DoubleRange"/>.
    /// </summary>
    public class DoubleRangeMinMaxAttribute : PropertyAttribute
    {
        #region Fields

        public double min;
        public double max;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructor

        /// <summary>
        /// Adds a min-max <see cref="PropertyAttribute"/> to a <see cref="DoubleRange"/>.
        /// </summary>
        public DoubleRangeMinMaxAttribute(double min, double max)
        {
            this.min = min;
            this.max = max.MustBeAbove(min, "max");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
