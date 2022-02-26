using UnityEngine;

namespace StephanHooft.Attributes
{
    /// <summary>
    /// Class to add a min-max <see cref="PropertyAttribute"/> to a <see cref="Vector2"/> or a <see cref="Vector2Int"/>.
    /// <remarks><para>Heavily based on code from: https://github.com/GucioDevs/SimpleMinMaxSlider .</para></remarks>
    /// </summary>
    public class MinMaxAttribute : PropertyAttribute
    {
        #region Fields

        public float min;
        public float max;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructor

        /// <summary>
        /// Adds a min-max <see cref="PropertyAttribute"/> to a <see cref="Vector2"/> or a <see cref="Vector2Int"/>.
        /// </summary>
        public MinMaxAttribute(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
