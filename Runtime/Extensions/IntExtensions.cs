using System;
using UnityEngine;

namespace StephanHooft.Extensions
{
    public static class IntExtensions
    {
        /// <summary>
        /// Returns the absolute value of the <see cref="int"/>.
        /// </summary>
        /// <returns>The absolute value of the <see cref="int"/>.</returns>
        public static int Abs(this int a)
        {
            return Math.Abs(a);
        }

        /// <summary>
        /// Returns the largest of two <see cref="int"/>s.
        /// </summary>
        /// <returns>The largest <see cref="int"/> value between <paramref name="value"/> and <paramref name="otherValue"/>.</returns>
        public static int Max(this int value, int otherValue)
        {
            return Math.Max(value, otherValue);
        }

        /// <summary>
        /// Returns the smallest of two <see cref="int"/>s.
        /// </summary>
        /// <returns>The smallest <see cref="int"/> value between <paramref name="value"/> and <paramref name="otherValue"/>.</returns>
        public static int Min(this int value, int otherValue)
        {
            return Math.Min(value, otherValue);
        }

        /// <summary>
        /// <para>Clamps the given <see cref="int"/> value between the given minimum <see cref="int"/> and maximum <see cref="int"/> values. 
        /// Returns the given value if it is within the <paramref name="min"/> and <paramref name="max"/> range.</para>
        /// </summary>
        /// <param name="min">The minimum value of the range to clamp to.</param>
        /// <param name="max">The maximum value of the range to clamp to.</param>
        /// <returns>The <see cref="int"/> result between the <paramref name="min"/> and <paramref name="max"/> values. </returns>
        public static int Clamp(this int value, int min, int max)
        {
            return Mathf.Clamp(value, min, max);
        }

        /// <summary>
        /// <para>Re-maps a <see cref="int"/> from one range to another.</para>
        /// <para>Does not constrain values to within the range by default, because out-of-range values are sometimes intended and useful.
        /// Set <paramref name="clampValue"/> to true to clamp the value.</para>
        /// </summary>
        /// <param name="inMin">The lower bound of the <see cref="int"/>'s current range.</param>
        /// <param name="inMax">The upper bound of the <see cref="int"/>'s current range.</param>
        /// <param name="outMin">The lower bound of the <see cref="int"/>'s target range.</param>
        /// <param name="outMax">The upper bound of the <see cref="int"/>'s target range </param>
        /// <param name="clampValue">Set to true to clamp <paramref name="value"/>.</param>
        /// <returns>The mapped <see cref="int"/> value.</returns>
        public static int Map(this int value, int inMin, int inMax, int outMin, int outMax, bool clampValue = false)
        {
            if (clampValue)
                value = value.Clamp(inMin, inMax);
            return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }

        /// <summary>
        /// Wraps the <see cref="int"/> value to a <paramref name="min"/>-<paramref name="max"/> range. Simulates the effect of a variable over-/underflowing.
        /// </summary>
        /// <param name="min">The minimum value of the range to wrap to.</param>
        /// <param name="max">The maximum value of the range to wrap to.</param>
        public static int Wrap(this int value, int min, int max)
        {
            if (max <= min)
                throw new ArgumentException("Value of max must be greater than min.", "max");
            int radix = max - min + 1;
            if (value < min)
                return value + ((max - value) / radix) * radix;
            else if (value > max)
                return value - ((value - min) / radix) * radix;
            else
                return value;
        }

        /// <summary>
        /// Checks if the <see cref="int"/> has a positive value.
        /// </summary>
        /// <returns>True if the <see cref="int"/> value is greater than 0.</returns>
        public static bool IsPositive(this int value)
        {
            if (value > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Checks if the <see cref="int"/> has a negative value.
        /// </summary>
        /// <returns>True if the <see cref="int"/> value is smaller than 0.</returns>
        public static bool IsNegative(this int value)
        {
            if (value < 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Checks if the <see cref="int"/> has a value of 0.
        /// </summary>
        /// <returns>True if the <see cref="int"/> value is 0.</returns>
        public static bool IsZero(this int value)
        {
            if (value == 0)
                return true;
            else
                return false;
        }
    }
}
