using UnityEngine;

namespace StephanHooft.Extensions
{
    public static class Vector3Extensions
    {
        /// <summary>
        /// Returns the normalised direction towards another <see cref="Vector3"/> destination.
        /// </summary>
        /// <param name="destination">The <see cref="Vector3"/> destination to calculate direction to.</param>
        /// <returns>The direction from the source <see cref="Vector3"/> to the destination <see cref="Vector3"/>.</returns>
        public static Vector3 DirectionTo(this Vector3 source, Vector3 destination)
        {
            return Vector3.Normalize(destination - source);
        }

        /// <summary>
        /// Returns the distance <see cref="float"/> from this <see cref="Vector3"/> to another.
        /// </summary>
        /// <param name="destination">The destination <see cref="Vector3"/>.</param>
        /// <returns>The <see cref="float"/> distance from the <paramref name="source"/> to the <paramref name="destination"/>.</returns>
        public static float DistanceTo(this Vector3 source, Vector3 destination)
        {
            return Vector3.Distance(source, destination);
        }

        /// <summary>
        /// Return the <see cref="Vector3"/> with its Y-component to 0.
        /// </summary>
        /// <returns>A modified <see cref="Vector3"/> with y component set to 0.</returns>
        public static Vector3 Flat(this Vector3 original)
        {
            return new Vector3(original.x, 0, original.z);
        }

        /// <summary>
        /// Converts a <see cref="Vector3"/>[] into a <see cref="Vector2"/>[].
        /// </summary>
        /// <returns>A <see cref="Vector2"/>[].</returns>
        public static Vector2[] ToVector2Array(this Vector3[] array)
        {
            return System.Array.ConvertAll(array, (Vector3 v3) => new Vector2(v3.x, v3.y));
        }

        /// <summary>
        /// Return the <see cref="Vector3"/> with one or more components set to a specific value.
        /// </summary>
        /// <param name="x">Value for the modified X-component.</param>
        /// <param name="y">Value for the modified Y-component.</param>
        /// <param name="z">Value for the modified Z-component.</param>
        /// <returns>A <see cref="Vector3"/> with the modified component values.</returns>
        public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
        }
    }
}
