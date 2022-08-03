using UnityEngine;

namespace StephanHooft.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Vector3"/>.
    /// </summary>
    public static class Vector3Extensions
    {
        #region Static Methods
        /// <summary>
        /// Returns the normalised direction towards another <see cref="Vector3"/> destination.
        /// </summary>
        /// <param name="destination">The <see cref="Vector3"/> destination to calculate direction to.</param>
        /// <returns>The direction from the source <see cref="Vector3"/> to the destination <see cref="Vector3"/>.</returns>
        public static Vector3 DirectionTo(this Vector3 source, Vector3 destination)
        {
            return
                Vector3.Normalize(destination - source);
        }

        /// <summary>
        /// Returns the distance <see cref="float"/> from this <see cref="Vector3"/> to another.
        /// </summary>
        /// <param name="destination">The destination <see cref="Vector3"/>.</param>
        /// <returns>The <see cref="float"/> distance from the <paramref name="source"/> to the <paramref name="destination"/>.</returns>
        public static float DistanceTo(this Vector3 source, Vector3 destination)
        {
            return
                Vector3.Distance(source, destination);
        }

        /// <summary>
        /// Return the <see cref="Vector3"/> with its Y-component at 0.
        /// </summary>
        /// <returns>A modified <see cref="Vector3"/> with y component set to 0.</returns>
        public static Vector3 Flat(this Vector3 source)
        {
            return
                new Vector3(source.x, 0, source.z);
        }

        /// <summary>
        /// Returns the <see cref="Vector3"/> with a set <paramref name="x"/> component, and Y- and Z-components
        /// scaled to match while retaining the X,Y,Z proportions of the source <see cref="Vector3"/>.
        /// </summary>
        /// <param name="x">
        /// A X-component value for the scaled <see cref="Vector3"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Vector3"/>.
        /// </returns>
        public static Vector3 ScaledToX(this Vector3 source, float x)
        {
            var factor = x / source.x;
            var y = source.y * factor;
            var z = source.z * factor;
            return
                new(x, y, z);
        }

        /// <summary>
        /// Returns the <see cref="Vector3"/> with a set <paramref name="y"/> component, and X- and Z-components
        /// scaled to match while retaining the X,Y,Z proportions of the source <see cref="Vector3"/>.
        /// </summary>
        /// <param name="y">
        /// A Y-component value for the scaled <see cref="Vector3"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Vector3"/>.
        /// </returns>
        public static Vector3 ScaledToY(this Vector3 source, float y)
        {
            var factor = y / source.y;
            var x = source.x * factor;
            var z = source.z * factor;
            return
                new(x, y, z);
        }

        /// <summary>
        /// Returns the <see cref="Vector3"/> with a set <paramref name="z"/> component, and X- and Y-components
        /// scaled to match while retaining the X,Y,Z proportions of the source <see cref="Vector3"/>.
        /// </summary>
        /// <param name="z">
        /// A Z-component value for the scaled <see cref="Vector3"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Vector3"/>.
        /// </returns>
        public static Vector3 ScaledToZ(this Vector3 source, float z)
        {
            var factor = z / source.z;
            var x = source.x * factor;
            var y = source.y * factor;
            return
                new(x, y, z);
        }

        /// <summary>
        /// Converts a <see cref="Vector3"/>[] into a <see cref="Vector2"/>[].
        /// </summary>
        /// <returns>A <see cref="Vector2"/>[].</returns>
        public static Vector2[] ToVector2Array(this Vector3[] array)
        {
            return
                System.Array.ConvertAll(array, (Vector3 v3) => new Vector2(v3.x, v3.y));
        }

        /// <summary>
        /// Returns the <see cref="Vector3"/> with one or more components set to a specific value.
        /// </summary>
        /// <param name="x">Value for the modified X-component.</param>
        /// <param name="y">Value for the modified Y-component.</param>
        /// <param name="z">Value for the modified Z-component.</param>
        /// <returns>A <see cref="Vector3"/> with the modified component values.</returns>
        public static Vector3 With(this Vector3 source, float? x = null, float? y = null, float? z = null)
        {
            return
                new Vector3(x ?? source.x, y ?? source.y, z ?? source.z);
        }

        /// <summary>
        /// Returns the <see cref="Vector3"/> with a specific magnitude.
        /// </summary>
        /// <param name="magnitude">The modified <see cref="float"/> magnitude.</param>
        /// <returns>A <see cref="Vector3"/> with the specified magnitude.</returns>
        public static Vector3 WithMagnitude(this Vector3 source, float magnitude)
        {
            return
                Vector3.Normalize(source) * magnitude;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
