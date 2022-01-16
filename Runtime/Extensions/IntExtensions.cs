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
            return 
                Math.Abs(a);
        }

        /// <summary>
        /// <para>Clamps the given <see cref="int"/> value between the given minimum <see cref="int"/> and maximum <see cref="int"/> values. 
        /// Returns the given value if it is within the <paramref name="min"/> and <paramref name="max"/> range.</para>
        /// </summary>
        /// <param name="min">The minimum value of the range to clamp to.</param>
        /// <param name="max">The maximum value of the range to clamp to.</param>
        /// <returns>The <see cref="int"/> result between the <paramref name="min"/> and <paramref name="max"/> values. </returns>
        public static int Clamp(this int original, int min, int max)
        {
            return 
                Mathf.Clamp(original, min, max);
        }

        /// <summary>
        /// Checks if the <see cref="int"/> has a positive value.
        /// </summary>
        /// <returns>True if the <see cref="int"/> value is greater than 0.</returns>
        public static bool IsPositive(this int original)
        {
            if (original > 0)
                return
                    true;
            else
                return
                    false;
        }

        /// <summary>
        /// Checks if the <see cref="int"/> has a negative value.
        /// </summary>
        /// <returns>True if the <see cref="int"/> value is smaller than 0.</returns>
        public static bool IsNegative(this int original)
        {
            if (original < 0)
                return
                    true;
            else
                return
                    false;
        }

        /// <summary>
        /// Checks if the <see cref="int"/> has a value of 0.
        /// </summary>
        /// <returns>True if the <see cref="int"/> value is 0.</returns>
        public static bool IsZero(this int original)
        {
            if (original == 0)
                return
                    true;
            else
                return
                    false;
        }

        /// <summary>
        /// <para>Re-maps a <see cref="int"/> from one range to another.</para>
        /// <para>Does not clamp values within the range by default, because out-of-range values are sometimes intended and useful.
        /// </summary>
        /// <param name="inMin">The lower bound of the <see cref="int"/>'s current range.</param>
        /// <param name="inMax">The upper bound of the <see cref="int"/>'s current range.</param>
        /// <param name="outMin">The lower bound of the <see cref="int"/>'s target range.</param>
        /// <param name="outMax">The upper bound of the <see cref="int"/>'s target range </param>
        /// <param name="clampValue">Set to true to clamp <paramref name="original"/>.</param>
        /// <returns>The mapped <see cref="int"/> value.</returns>
        public static int Map(this int original, int inMin, int inMax, int outMin, int outMax, bool clampValue = false)
        {
            if (clampValue)
                original = original.Clamp(inMin, inMax);
            return 
                (original - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }

        /// <summary>
        /// Returns the largest of two <see cref="int"/>s.
        /// </summary>
        /// <returns>The largest <see cref="int"/> value between <paramref name="original"/> and <paramref name="otherValue"/>.</returns>
        public static int Max(this int original, int otherValue)
        {
            return 
                Math.Max(original, otherValue);
        }

        /// <summary>
        /// Returns the smallest of two <see cref="int"/>s.
        /// </summary>
        /// <returns>The smallest <see cref="int"/> value between <paramref name="original"/> and <paramref name="otherValue"/>.</returns>
        public static int Min(this int original, int otherValue)
        {
            return 
                Math.Min(original, otherValue);
        }

        /// <summary>
        /// Ensures that the <see cref="int"/> value is above a set <paramref name="lower"/> <see cref="int"/> value.
        /// <para>An <see cref="ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="lower">The value that the <see cref="int"/> must remain above.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="int"/> value, assuming no <see cref="ArgumentOutOfRangeException"/> was thrown.</returns>
        public static int MustBeAbove(this int original, int lower, string paramName)
        {
            if (original > lower)
                return
                    original;
            else
                throw
                    new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="int"/> value is above or equal to a set <paramref name="lower"/> <see cref="int"/> value.
        /// <para>An <see cref="ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="lower">The value that the <see cref="int"/> must remain above or equal to.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="int"/> value, assuming no <see cref="ArgumentOutOfRangeException"/> was thrown.</returns>
        public static int MustBeAboveOrEqualTo(this int original, int lower, string paramName)
        {
            if (original >= lower)
                return
                    original;
            else
                throw
                    new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="int"/> value is below a set <paramref name="upper"/> <see cref="int"/> value.
        /// <para>An <see cref="ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="upper">The value that the <see cref="int"/> must remain below.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="int"/> value, assuming no <see cref="ArgumentOutOfRangeException"/> was thrown.</returns>
        public static int MustBeLowerThan(this int original, int upper, string paramName)
        {
            if (original < upper)
                return
                    original;
            else
                throw
                    new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="int"/> value is below or equal to a set <paramref name="upper"/> <see cref="int"/> value.
        /// <para>An <see cref="ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="upper">The value that the <see cref="int"/> must remain below or equal to.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="int"/> value, assuming no <see cref="ArgumentOutOfRangeException"/> was thrown.</returns>
        public static int MustBeLowerThanOrEqualTo(this int original, int upper, string paramName)
        {
            if (original <= upper)
                return
                    original;
            else
                throw
                    new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="int"/> value is within a range determined by a <paramref name="lower"/> 
        /// and an <paramref name="upper"/> <see cref="int"/> value.
        /// <para>An <see cref="ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown if the <see cref="int"/>
        /// goes outside of this range.</para>
        /// </summary>
        /// <param name="lower">The value that the <see cref="int"/> must remain above or equal to.</param>
        /// <param name="upper">The value that the <see cref="int"/> must remain below or equal to.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="int"/> value, assuming no <see cref="ArgumentOutOfRangeException"/> was thrown.</returns>
        public static int MustBeWithinRange(this int original, int lower, int upper, string paramName)
        {
            if (original >= lower && original <= upper)
                return
                    original;
            else
                throw
                    new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="int"/> value is not equal to a certain <paramref name="value"/>.
        /// <para>An <see cref="ArgumentOutOfRangeException"/> will be thrown if the <see cref="int"/> equals the <paramref name="value"/>.</para>
        /// </summary>
        /// <param name="value">The <see cref="int"/> value to check against.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="int"/> value, assuming no <see cref="ArgumentOutOfRangeException"/> was thrown.</returns>
        public static int MustNotBeEqualTo(this int original, int value, string paramName)
        {
            if (original != value)
                return
                    original;
            else
                throw
                    new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Wraps the <see cref="int"/> value to a <paramref name="min"/>-<paramref name="max"/> range. Simulates the effect of a variable over-/underflowing.
        /// </summary>
        /// <param name="min">The minimum value of the range to wrap to.</param>
        /// <param name="max">The maximum value of the range to wrap to.</param>
        public static int Wrap(this int original, int min, int max)
        {
            if (max <= min)
                throw 
                    new ArgumentException("Value of max must be greater than min.", "max");
            int radix = max - min + 1;
            if (original < min)
                return 
                    original + ((max - original) / radix) * radix;
            else if (original > max)
                return 
                    original - ((original - min) / radix) * radix;
            else
                return 
                    original;
        }
    }
}
