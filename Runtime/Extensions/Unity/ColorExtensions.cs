using UnityEngine;

namespace StephanHooft.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Color"/>.
    /// </summary>
    public static class ColorExtensions
    {
        #region Static Methods

        /// <summary>
        /// Returns the <see cref="Color"/> with one or more components set to a specific <see cref="float"/> value.
        /// </summary>
        /// <param name="r">Value (<see cref="float"/>) for the modified R-component.</param>
        /// <param name="g">Value (<see cref="float"/>) for the modified G-component.</param>
        /// <param name="b">Value (<see cref="float"/>) for the modified B-component.</param>
        /// <param name="a">Value (<see cref="float"/>) for the modified A-component.</param>
        /// <returns>A <see cref="Color"/> with the modified component values.</returns>
        public static Color With(this Color original, float? r = null, float? g = null, float? b = null, float? a = null)
        {
            return
                new Color(r ?? original.r, g ?? original.g, b ?? original.b, a ?? original.a);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
