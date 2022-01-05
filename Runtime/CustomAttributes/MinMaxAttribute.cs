using UnityEngine;

namespace StephanHooft.CustomAttributes
{
    /// <summary>
    /// Class to add a min-max <see cref="PropertyAttribute"/> to a <see cref="Vector2"/> or a <see cref="Vector2Int"/>.
    /// <para>Heavily based on code from: https://github.com/GucioDevs/SimpleMinMaxSlider .</para>
    /// </summary>
    public class MinMaxAttribute : PropertyAttribute
    {
        public float min;
        public float max;

        /// <summary>
        /// Add a min-max <see cref="PropertyAttribute"/> to a <see cref="Vector2"/> or a <see cref="Vector2Int"/>.
        /// </summary>
        public MinMaxAttribute(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
}
