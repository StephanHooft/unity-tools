using StephanHooft.Extensions;
using UnityEngine;

namespace StephanHooft.Variables.Ranges
{
    /// <summary>
    /// Class to add a min-max <see cref="PropertyAttribute"/> to a <see cref="IntRange"/>.
    /// </summary>
    public class IntRangeMinMaxAttribute : PropertyAttribute
    {
        #region Fields

        public int min;
        public int max;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructor

        /// <summary>
        /// Adds a min-max <see cref="PropertyAttribute"/> to an <see cref="IntRange"/>.
        /// </summary>
        public IntRangeMinMaxAttribute(int min, int max)
        {
            this.min = min;
            this.max = max.MustBeAbove(min, "max");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
