using UnityEngine;

namespace StephanHooft.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Rect"/>.
    /// </summary>
    public static class RectExtensions
    {
        #region Static Methods

        /// <summary>
        /// Returns the <see cref="Rect"/>'s area.
        /// </summary>
        public static float Area(this Rect r)
        {
            return r.width * r.height;
        }

        /// <summary>
        /// Returns true if <paramref name="r"/> contains <paramref name="r2"/>.
        /// <para>Edges may not be shared.</para>
        /// </summary>
        public static bool Contains(this Rect r, Rect r2)
        {
            return r.Contains(r2.min) && r.Contains(r2.max);
        }

        /// <summary>
		/// Get the corners of the <see cref="Rect"/>: [bottomLeft, topLeft, topRight, bottomRight]
		/// </summary>
		public static Vector2[] GetCorners(this Rect r)
        {
            Vector2[] corners = new Vector2[4];
            corners[0] = r.min;
            corners[1] = new Vector2(r.xMin, r.yMax);
            corners[2] = r.max;
            corners[3] = new Vector2(r.xMax, r.yMin);
            return corners;
        }

        /// <summary>
		/// Returns a <see cref="Rect"/> that contains both <paramref name="r"/> and <paramref name="r2"/>.
		/// </summary>
		public static Rect Encapsulate(this Rect r, Rect r2)
        {
            var res = new Rect();
            res.xMin = Mathf.Min(r.xMin, r2.xMin);
            res.yMin = Mathf.Min(r.yMin, r2.yMin);
            res.xMax = Mathf.Max(r.xMax, r2.xMax);
            res.yMax = Mathf.Max(r.yMax, r2.yMax);
            return res;
        }

        /// <summary>
        /// Returns the aspect of the <see cref="Rect"/>.
        /// </summary>
        /// <param name="aspect">The aspect as a <see cref="float"/>.</param>
        /// <returns>Returns true if height is a value above zero.</returns>
        public static bool GetAspect(this Rect r, out float aspect)
        {
            if (r.height <= 0)
            {
                aspect = float.NaN;
                return false;
            }
            aspect = r.width / r.height;
            return true;
        }

        /// <summary>
		/// Linearly interpolates to <see cref="Rect"/> <paramref name="r2"/> by <paramref name="t"/>.
		/// </summary>
        /// <returns>
        /// The <see cref="Rect"/> resulting from the linear interpolation.
        /// </returns>
		public static Rect LerpTo(this Rect r, Rect r2, float t)
        {
            t = Mathf.Clamp01(t);

            return new Rect(Mathf.Lerp(r.x, r2.x, t), Mathf.Lerp(r.y, r2.y, t),
                Mathf.Lerp(r.width, r2.width, t), Mathf.Lerp(r.height, r2.height, t));
        }

        /// <summary>
        /// Linearly interpolates to <see cref="Rect"/> <paramref name="r2"/> with no limit to <paramref name="t"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="Rect"/> resulting from the linear interpolation.
        /// </returns>
        public static Rect LerpToUnclamped(this Rect r, Rect r2, float t)
        {
            return new Rect(Mathf.LerpUnclamped(r.x, r2.x, t), Mathf.LerpUnclamped(r.y, r2.y, t),
                Mathf.LerpUnclamped(r.width, r2.width, t), Mathf.LerpUnclamped(r.height, r2.height, t));
        }

        /// <summary>
        /// Converts the <see cref="Vector4"/> to a <see cref="Rect"/>: [x, y, width, height].
        /// </summary>
        public static Rect ToRect(this Vector4 v)
        {
            return new Rect(v.x, v.y, v.z, v.w);
        }

        /// <summary>
        /// Returns the <see cref="Rect"/> with one or more components set to a specific value.
        /// </summary>
        /// <param name="position">
        /// Value for the modified position.
        /// </param>
        /// <param name="size">
        /// Value for the modified size.
        /// </param>
        /// <returns>
        /// A <see cref="Rect"/> with the modified component values.
        /// </returns>
        public static Rect With(this Rect r, Vector2? position = null, Vector2? size = null)
        {
            return new Rect(position ?? r.position, size ?? r.size);
        }

        /// <summary>
        /// Returns the <see cref="Rect"/> with one or more components set to a specific value.
        /// </summary>
        /// <param name="x">
        /// Value for the modified X-position.
        /// </param>
        /// <param name="y">
        /// Value for the modified Y-position.
        /// </param>
        /// <param name="width">
        /// Value for the modified width.
        /// </param>
        /// <param name="height">
        /// Value for the modified height.
        /// </param>
        /// <returns>
        /// A <see cref="Rect"/> with the modified component values.
        /// </returns>
        public static Rect With
            (this Rect r, float? x = null, float? y = null, float? width = null, float? height = null)
        {
            return new Rect(x ?? r.x, y ?? r.y, width ?? r.width, height ?? r.height);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
