﻿using System;
using UnityEngine;

namespace StephanHooft.Extensions
{
    public static class IntExtensions
    {
        /// <summary>
        /// Returns the absolute value of the <see cref="int"/>.
        /// </summary>
        /// <returns>The absolute value of the <see cref="int"/>.</returns>
        public static int Abs(this int i)
        {
            return 
                Math.Abs(i);
        }

        /// <summary>
        /// <para>Clamps the given <see cref="int"/> value between the given <paramref name="min"/> <see cref="int"/> and <paramref name="max"/> <see cref="int"/> values. 
        /// Returns the given value if it is within the <paramref name="min"/> and <paramref name="max"/> range.</para>
        /// </summary>
        /// <param name="min">The minimum <see cref="int"/> value of the range to clamp to.</param>
        /// <param name="max">The maximum <see cref="int"/> value of the range to clamp to.</param>
        /// <returns>The <see cref="int"/> result between the <paramref name="min"/> and <paramref name="max"/> values. </returns>
        public static int Clamp(this int i, int min, int max)
        {
            return 
                Mathf.Clamp(i, min, max);
        }

        /// <summary>
        /// Checks if the <see cref="int"/> has a positive value.
        /// </summary>
        /// <returns><see cref="true"/> if the <see cref="int"/> value is greater than 0.</returns>
        public static bool IsPositive(this int i)
        {
            if (i > 0)
                return
                    true;
            else
                return
                    false;
        }

        /// <summary>
        /// Checks if the <see cref="int"/> has a negative value.
        /// </summary>
        /// <returns><see cref="true"/> if the <see cref="int"/> value is smaller than 0.</returns>
        public static bool IsNegative(this int i)
        {
            if (i < 0)
                return
                    true;
            else
                return
                    false;
        }

        /// <summary>
        /// Checks if the <see cref="int"/> has a value of 0.
        /// </summary>
        /// <returns><see cref="true"/> if the <see cref="int"/> value is 0.</returns>
        public static bool IsZero(this int i)
        {
            if (i == 0)
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
        /// <param name="clamp">Set to true to clamp the <see cref="int"/>.</param>
        /// <returns>The mapped <see cref="int"/> value.</returns>
        public static int Map(this int i, int inMin, int inMax, int outMin, int outMax, bool clamp = false)
        {
            if (clamp)
                i = i.Clamp(inMin, inMax);
            return 
                (i - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }

        /// <summary>
        /// Returns the <see cref="int"/> with a sign matching an<paramref name="other"/> <see cref="int"/>.
        /// </summary>
        /// <param name="other">The other <see cref="int"/> whose sign to match.</param>
        /// <returns>The <see cref="int"/> with its sign matching <paramref name="other"/>.</returns>
        public static int MatchingSign(this int i, int other)
        {
            if (Mathf.Sign(i) == Mathf.Sign(other))
                return
                    i;
            else
                return
                    -i;
        }

        /// <summary>
        /// Returns the largest of two <see cref="int"/>s.
        /// </summary>
        /// <param name="other">The other <see cref="int"/> to compare against.</param>
        /// <returns>The largest value between the <see cref="int"/> and <paramref name="other"/>.</returns>
        public static int Max(this int i, int other)
        {
            return
                Math.Max(i, other);
        }

        /// <summary>
        /// Returns the largest <see cref="int"/>.
        /// </summary>
        /// <param name="others">A range of <see cref="int"/>s to compare against.</param>
        /// <returns>The largest <see cref="int"/> value.</returns>
        public static int Max(this int i, int[] others)
        {
            var max = Mathf.Max(others);
            return
                Math.Max(i, max);
        }

        /// <summary>
        /// Returns the smallest of two <see cref="int"/>s.
        /// </summary>
        /// <param name="other">The other <see cref="int"/> to compare against.</param>
        /// <returns>The largest value between the <see cref="int"/> and <paramref name="other"/>.</returns>
        public static int Min(this int i, int other)
        {
            return
                Math.Min(i, other);
        }

        /// <summary>
        /// Returns the smallest <see cref="int"/>.
        /// </summary>
        /// <param name="others">A range of <see cref="int"/>s to compare against.</param>
        /// <returns>The smallest <see cref="int"/> value.</returns>
        public static int Min(this int i, int[] others)
        {
            var min = Mathf.Min(others);
            return
                Math.Min(i, min);
        }

        /// <summary>
        /// Ensures that the <see cref="int"/> value is above a set <paramref name="lower"/> <see cref="int"/> value.
        /// <para>An <see cref="ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="lower">The value that the <see cref="int"/> must remain above.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="int"/> value.</returns>
        public static int MustBeAbove(this int i, int lower, string paramName)
        {
            if (i > lower)
                return
                    i;
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
        /// <returns>The original <see cref="int"/> value.</returns>
        public static int MustBeAboveOrEqualTo(this int i, int lower, string paramName)
        {
            if (i >= lower)
                return
                    i;
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
        /// <returns>The original <see cref="int"/> value.</returns>
        public static int MustBeLowerThan(this int i, int upper, string paramName)
        {
            if (i < upper)
                return
                    i;
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
        /// <returns>The original <see cref="int"/> value.</returns>
        public static int MustBeLowerThanOrEqualTo(this int i, int upper, string paramName)
        {
            if (i <= upper)
                return
                    i;
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
        /// <returns>The original <see cref="int"/> value.</returns>
        public static int MustBeWithinRange(this int i, int lower, int upper, string paramName)
        {
            if (i >= lower && i <= upper)
                return
                    i;
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
        /// <returns>The original <see cref="int"/> value.</returns>
        public static int MustNotBeEqualTo(this int i, int value, string paramName)
        {
            if (i != value)
                return
                    i;
            else
                throw
                    new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Returns the <see cref="int"/> with a sign opposite to an<paramref name="other"/> <see cref="int"/>.
        /// </summary>
        /// <param name="other">The other <see cref="int"/> whose sign to oppose.</param>
        /// <returns>The <see cref="int"/> with its sign oppisite to that of <paramref name="other"/>.</returns>
        public static int OppositeSign(this int i, int other)
        {
            if (Math.Sign(i) != Math.Sign(other))
                return
                    i;
            else
                return
                    -i;
        }

        /// <summary>
        /// Returns the <see cref="int"/>'s sign.
        /// </summary>
        /// <returns>The <see cref="int"/>'s sign.</returns>
        public static int Sign(this int i)
        {
            return
                Math.Sign(i);
        }

        /// <summary>
        /// Wraps the <see cref="int"/> value to a <paramref name="min"/>-<paramref name="max"/> range. Simulates the effect of a variable over-/underflowing.
        /// </summary>
        /// <param name="min">The minimum <see cref="int"/> value of the range to wrap to.</param>
        /// <param name="max">The maximum <see cref="int"/> value of the range to wrap to.</param>
        public static int Wrap(this int i, int min, int max)
        {
            if (max <= min)
                throw 
                    new ArgumentException("Value of max must be greater than min.", "max");
            int radix = max - min + 1;
            if (i < min)
                return 
                    i + ((max - i) / radix) * radix;
            else if (i > max)
                return 
                    i - ((i - min) / radix) * radix;
            else
                return 
                    i;
        }
    }
}
