using UnityEngine;

namespace StephanHooft.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Vector2"/>.
    /// </summary>
    public static class Vector2Extensions
    {
        #region Static Methods

        /// <summary>
        /// Returns direction toward another <see cref="Vector2"/> destination.
        /// </summary>
        /// <param name="destination">
        /// The <see cref="Vector2"/> destination to calculate direction to.
        /// </param>
        /// <returns>
        /// The direction from the source <see cref="Vector2"/> to the destination <see cref="Vector2"/>.
        /// </returns>
        public static Vector2 DirectionTo(this Vector2 source, Vector2 destination)
        {
            return
                (destination - source).normalized;
        }

        /// <summary>
        /// Returns the distance <see cref="float"/> from this <see cref="Vector2"/> to another.
        /// </summary>
        /// <param name="destination">
        /// The destination <see cref="Vector2"/>.
        /// </param>
        /// <returns>
        /// The <see cref="float"/> distance from the <paramref name="source"/> to the <paramref name="destination"/>.
        /// </returns>
        public static float DistanceTo(this Vector2 source, Vector2 destination)
        {
            return
                Vector2.Distance(source, destination);
        }

        /// <summary>
        /// Rotates the <see cref="Vector2"/> clockwise by 90 degrees.
        /// </summary>
        /// <returns>
        /// The rotated <see cref="Vector2"/>.
        /// </returns>
        public static Vector2 Rotate90DegreesClockwise(this Vector2 source)
        {
            return
                new(source.y, -source.x);
        }

        /// <summary>
        /// Rotates the <see cref="Vector2"/> counter-clockwise by 90 degrees.
        /// </summary>
        /// <returns>
        /// The rotated <see cref="Vector2"/>.
        /// </returns>
        public static Vector2 Rotate90DegreesCounterClockwise(this Vector2 source)
        {
            return
                new(-source.y, source.x);
        }

        /// <summary>
        /// Rotates the <see cref="Vector2"/> by a certain <paramref name="rotationAmount"/> (in degrees or radians).
        /// </summary>
        /// <param name="rotationAmount">
        /// The amount to rotate the <see cref="Vector2"/>.
        /// </param>
        /// <param name="useDegrees">
        /// Set to <see cref="true"/> to use degrees instead of radians.
        /// </param>
        /// <returns>
        /// The rotated <see cref="Vector2"/>.
        /// </returns>
        public static Vector2 Rotate(this Vector2 source, float rotationAmount, bool useDegrees = false)
        {
            var cosRotation = rotationAmount.Cos(useDegrees);
            var sinRotation = rotationAmount.Sin(useDegrees);
            var rotatedX = source.x * cosRotation - source.y * sinRotation;
            var rotatedY = source.x * sinRotation + source.y * cosRotation;
            return
                new Vector2(rotatedX, rotatedY);
        }

        /// <summary>
        /// Returns the <see cref="Vector2"/> with a set <paramref name="x"/> component, and Y-component
        /// scaled to match while retaining the X,Y proportions of the source <see cref="Vector2"/>.
        /// </summary>
        /// <param name="x">
        /// A X-component value for the scaled <see cref="Vector2"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Vector2"/>.
        /// </returns>
        public static Vector2 ScaledToX(this Vector2 source, float x)
        {
            var factor = x / source.x;
            var y = source.y * factor;
            return
                new(x, y);
        }

        /// <summary>
        /// Returns the <see cref="Vector2"/> with a set <paramref name="y"/> component, and X-component
        /// scaled to match while retaining the X,Y proportions of the source <see cref="Vector2"/>.
        /// </summary>
        /// <param name="y">
        /// A Y-component value for the scaled <see cref="Vector2"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Vector2"/>.
        /// </returns>
        public static Vector2 ScaledToY(this Vector2 source, float y)
        {
            var factor = y / source.y;
            var x = source.x * factor;
            return
                new(x, y);
        }

        /// <summary>
        /// Converts a <see cref="Vector2"/>[] into a <see cref="Vector3"/>[].
        /// </summary>
        /// <returns>
        /// A <see cref="Vector3"/>[].
        /// </returns>
        public static Vector3[] ToVector3Array(this Vector2[] array)
        {
            return
                System.Array.ConvertAll(array, (Vector2 v2) => new Vector3(v2.x, v2.y));
        }

        /// <summary>
        /// Returns the <see cref="Vector2"/> with one or more components set to a specific value.
        /// </summary>
        /// <param name="x">
        /// Value for the modified X-component.
        /// </param>
        /// <param name="y">
        /// Value for the modified Y-component.
        /// </param>
        /// <returns>
        /// A <see cref="Vector2"/> with the modified component values.
        /// </returns>
        public static Vector2 With(this Vector2 source, float? x = null, float? y = null)
        {
            return
                new Vector2(x ?? source.x, y ?? source.y);
        }

        /// <summary>
        /// Returns the <see cref="Vector2"/> with a specific magnitude.
        /// </summary>
        /// <param name="magnitude">
        /// The modified <see cref="float"/> magnitude.
        /// </param>
        /// <returns>
        /// A <see cref="Vector2"/> with the specified magnitude.
        /// </returns>
        public static Vector2 WithMagnitude(this Vector2 source, float magnitude)
        {
            return
                source.normalized * magnitude;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
