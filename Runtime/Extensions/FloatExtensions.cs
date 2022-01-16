using UnityEngine;

namespace StephanHooft.Extensions
{
    public static class FloatExtensions
    {
        /// <summary>
        /// Returns the absolute value of the <see cref="float"/>.
        /// </summary>
        /// <returns>The absolute value of the <see cref="float"/>.</returns>
        public static float Abs(this float a)
        {
            return 
                Mathf.Abs(a);
        }

        /// <summary>
        /// <para>Clamps the given <see cref="float"/> value between the given minimum <see cref="float"/> and maximum <see cref="float"/> values. 
        /// Returns the given value if it is within the <paramref name="min"/> and <paramref name="max"/> range.</para>
        /// </summary>
        /// <param name="min">The minimum value of the range to clamp to.</param>
        /// <param name="max">The maximum value of the range to clamp to.</param>
        /// <returns>The <see cref="float"/> result between the <paramref name="min"/> and <paramref name="max"/> values. </returns>
        public static float Clamp(this float original, float min, float max)
        {
            return 
                Mathf.Clamp(original, min, max);
        }

        /// <summary>
        /// Checks if the <see cref="float"/> has a positive value.
        /// </summary>
        /// <returns>True if the <see cref="float"/> value is greater than 0.</returns>
        public static bool IsPositive(this float original)
        {
            if (original > 0)
                return 
                    true;
            else
                return 
                    false;
        }

        /// <summary>
        /// Checks if the <see cref="float"/> has a negative value.
        /// </summary>
        /// <returns>True if the <see cref="float"/> value is smaller than 0.</returns>
        public static bool IsNegative(this float original)
        {
            if (original < 0)
                return 
                    true;
            else
                return 
                    false;
        }

        /// <summary>
        /// Checks if the <see cref="float"/> has a value of 0.
        /// </summary>
        /// <returns>True if the <see cref="float"/> value is 0.</returns>
        public static bool IsZero(this float original)
        {
            if (original == 0)
                return 
                    true;
            else
                return 
                    false;
        }

        /// <summary>
        /// <para>Re-maps a <see cref="float"/> from one range to another.</para>
        /// <para>Does not clamp values within the range by default, because out-of-range values are sometimes intended and useful.
        /// </summary>
        /// <param name="inMin">The lower bound of the <see cref="float"/>'s current range.</param>
        /// <param name="inMax">The upper bound of the <see cref="float"/>'s current range.</param>
        /// <param name="outMin">The lower bound of the <see cref="float"/>'s target range.</param>
        /// <param name="outMax">The upper bound of the <see cref="float"/>'s target range </param>
        /// <param name="clampValue">Set to true to clamp <paramref name="original"/>.</param>
        /// <returns>The mapped <see cref="float"/> value.</returns>
        public static float Map(this float original, float inMin, float inMax, float outMin, float outMax, bool clampValue = false)
        {
            if (clampValue)
                original = original.Clamp(inMin, inMax);
            return 
                (original - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }

        /// <summary>
        /// Returns the largest of two <see cref="float"/>s.
        /// </summary>
        /// <returns>The largest value between <paramref name="original"/> and <paramref name="otherValue"/>.</returns>
        public static float Max(this float original, float otherValue)
        {
            return 
                Mathf.Max(original, otherValue);
        }

        /// <summary>
        /// Returns the smallest of two <see cref="float"/>s.
        /// </summary>
        /// <returns>The largest value between <paramref name="original"/> and <paramref name="otherValue"/>.</returns>
        public static float Min(this float original, float otherValue)
        {
            return 
                Mathf.Min(original, otherValue);
        }

        /// <summary>
        /// Ensures that the <see cref="float"/> value is above a set <paramref name="lower"/> <see cref="float"/> value.
        /// <para>An <see cref="System.ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="lower">The value that the <see cref="float"/> must remain above.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="System.ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="float"/> value, assuming no <see cref="System.ArgumentOutOfRangeException"/> was thrown.</returns>
        public static float MustBeAbove(this float original, float lower, string paramName)
        {
            if (original > lower)
                return
                    original;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="float"/> value is above or equal to a set <paramref name="lower"/> <see cref="float"/> value.
        /// <para>An <see cref="System.ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="lower">The value that the <see cref="float"/> must remain above or equal to.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="System.ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="float"/> value, assuming no <see cref="System.ArgumentOutOfRangeException"/> was thrown.</returns>
        public static float MustBeAboveOrEqualTo(this float original, float lower, string paramName)
        {
            if (original >= lower)
                return
                    original;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="float"/> value is below a set <paramref name="upper"/> <see cref="float"/> value.
        /// <para>An <see cref="System.ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="upper">The value that the <see cref="float"/> must remain below.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="System.ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="float"/> value, assuming no <see cref="System.ArgumentOutOfRangeException"/> was thrown.</returns>
        public static float MustBeLowerThan(this float original, float upper, string paramName)
        {
            if (original < upper)
                return
                    original;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="float"/> value is below or equal to a set <paramref name="upper"/> <see cref="float"/> value.
        /// <para>An <see cref="System.ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="upper">The value that the <see cref="float"/> must remain below or equal to.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="System.ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="float"/> value, assuming no <see cref="System.ArgumentOutOfRangeException"/> was thrown.</returns>
        public static float MustBeLowerThanOrEqualTo(this float original, float upper, string paramName)
        {
            if (original <= upper)
                return
                    original;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="float"/> value is within a range determined by a <paramref name="lower"/> 
        /// and an <paramref name="upper"/> <see cref="float"/> value.
        /// <para>An <see cref="System.ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown if the <see cref="float"/>
        /// goes outside of this range.</para>
        /// </summary>
        /// <param name="lower">The value that the <see cref="float"/> must remain above or equal to.</param>
        /// <param name="upper">The value that the <see cref="float"/> must remain below or equal to.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="System.ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="float"/> value, assuming no <see cref="System.ArgumentOutOfRangeException"/> was thrown.</returns>
        public static float MustBeWithinRange(this float original, float lower, float upper, string paramName)
        {
            if (original >= lower && original <= upper)
                return
                    original;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="float"/> value is not equal to a certain <paramref name="value"/>.
        /// <para>An <see cref="System.ArgumentOutOfRangeException"/> will be thrown if the <see cref="float"/> equals the <paramref name="value"/>.</para>
        /// </summary>
        /// <param name="value">The <see cref="float"/> value to check against.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="System.ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="float"/> value, assuming no <see cref="System.ArgumentOutOfRangeException"/> was thrown.</returns>
        public static float MustNotBeEqualTo(this float original, float value, string paramName)
        {
            if (original != value)
                return
                    original;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Returns the <see cref="float"/> raised to power <paramref name="p"/>.
        /// </summary>
        /// <param name="p">The power to raise the float to.</param>
        /// <returns>The <see cref="float"/> raised to power <paramref name="p"/>.</returns>
        public static float Pow(this float original, float p)
        {
            return 
                Mathf.Pow(original, p);
        }

        /// <summary>
        /// Rounds the <see cref="float"/> to the nearest integer.
        /// </summary>
        /// <returns>The <see cref="float"/> to the nearest integer.</returns>
        public static float Round(this float original)
        {
            return 
                Mathf.Round(original);
        }

        /// <summary>
        /// Rounds the <see cref="float"/> to the nearest integer.
        /// </summary>
        /// <returns>The <see cref="float"/> to the nearest integer.</returns>
        public static int RoundToInt(this float original)
        {
            return 
                Mathf.RoundToInt(original);
        }

        /// <summary>
        /// Returns the square root of the <see cref="float"/>.
        /// </summary>
        /// <returns>The <see cref="float"/>'s square root.</returns>
        public static float Sqrt(this float original)
        {
            return 
                Mathf.Sqrt(original);
        }
    }
}
