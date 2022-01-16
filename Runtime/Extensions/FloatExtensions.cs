﻿using UnityEngine;

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
        public static float Clamp(this float value, float min, float max)
        {
            return 
                Mathf.Clamp(value, min, max);
        }

        /// <summary>
        /// Checks if the <see cref="float"/> has a positive value.
        /// </summary>
        /// <returns>True if the <see cref="float"/> value is greater than 0.</returns>
        public static bool IsPositive(this float value)
        {
            if (value > 0)
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
        public static bool IsNegative(this float value)
        {
            if (value < 0)
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
        public static bool IsZero(this float value)
        {
            if (value == 0)
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
        /// <param name="clampValue">Set to true to clamp <paramref name="value"/>.</param>
        /// <returns>The mapped <see cref="float"/> value.</returns>
        public static float Map(this float value, float inMin, float inMax, float outMin, float outMax, bool clampValue = false)
        {
            if (clampValue)
                value = value.Clamp(inMin, inMax);
            return 
                (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }

        /// <summary>
        /// Returns the largest of two <see cref="float"/>s.
        /// </summary>
        /// <returns>The largest value between <paramref name="value"/> and <paramref name="otherValue"/>.</returns>
        public static float Max(this float value, float otherValue)
        {
            return 
                Mathf.Max(value, otherValue);
        }

        /// <summary>
        /// Returns the smallest of two <see cref="float"/>s.
        /// </summary>
        /// <returns>The largest value between <paramref name="value"/> and <paramref name="otherValue"/>.</returns>
        public static float Min(this float value, float otherValue)
        {
            return 
                Mathf.Min(value, otherValue);
        }

        /// <summary>
        /// Ensures that the <see cref="float"/> value is above a set <paramref name="lower"/> <see cref="float"/> value.
        /// <para>A <see cref="System.ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="lower">The value that the <see cref="float"/> must remain above.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="System.ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="float"/> value, assuming no <see cref="System.ArgumentOutOfRangeException"/> was thrown.</returns>
        public static float MustBeAbove(this float value, float lower, string paramName)
        {
            if (value > lower)
                return
                    value;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="float"/> value is above or equal to a set <paramref name="lower"/> <see cref="float"/> value.
        /// <para>A <see cref="System.ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="lower">The value that the <see cref="float"/> must remain above or equal to.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="System.ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="float"/> value, assuming no <see cref="System.ArgumentOutOfRangeException"/> was thrown.</returns>
        public static float MustBeAboveOrEqualTo(this float value, float lower, string paramName)
        {
            if (value >= lower)
                return
                    value;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="float"/> value is below a set <paramref name="upper"/> <see cref="float"/> value.
        /// <para>A <see cref="System.ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="upper">The value that the <see cref="float"/> must remain below.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="System.ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="float"/> value, assuming no <see cref="System.ArgumentOutOfRangeException"/> was thrown.</returns>
        public static float MustBeLowerThan(this float value, float upper, string paramName)
        {
            if (value < upper)
                return
                    value;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="float"/> value is below or equal to a set <paramref name="upper"/> <see cref="float"/> value.
        /// <para>A <see cref="System.ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="upper">The value that the <see cref="float"/> must remain below or equal to.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="System.ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="float"/> value, assuming no <see cref="System.ArgumentOutOfRangeException"/> was thrown.</returns>
        public static float MustBeLowerThanOrEqualTo(this float value, float upper, string paramName)
        {
            if (value <= upper)
                return
                    value;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="float"/> value is within a range determined by a <paramref name="lower"/> 
        /// and an <paramref name="upper"/> <see cref="float"/> value.
        /// <para>A <see cref="System.ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown if the <see cref="float"/>
        /// goes outside of this range.</para>
        /// </summary>
        /// <param name="lower">The value that the <see cref="float"/> must remain above or equal to.</param>
        /// <param name="upper">The value that the <see cref="float"/> must remain below or equal to.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="System.ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="float"/> value, assuming no <see cref="System.ArgumentOutOfRangeException"/> was thrown.</returns>
        public static float MustBeWithinRange(this float value, float lower, float upper, string paramName)
        {
            if (value >= lower && value <= upper)
                return
                    value;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Returns the <see cref="float"/> raised to power <paramref name="p"/>.
        /// </summary>
        /// <param name="p">The power to raise the float to.</param>
        /// <returns>The <see cref="float"/> raised to power <paramref name="p"/>.</returns>
        public static float Pow(this float value, float p)
        {
            return 
                Mathf.Pow(value, p);
        }

        /// <summary>
        /// Rounds the <see cref="float"/> to the nearest integer.
        /// </summary>
        /// <returns>The <see cref="float"/> to the nearest integer.</returns>
        public static float Round(this float value)
        {
            return 
                Mathf.Round(value);
        }

        /// <summary>
        /// Rounds the <see cref="float"/> to the nearest integer.
        /// </summary>
        /// <returns>The <see cref="float"/> to the nearest integer.</returns>
        public static int RoundToInt(this float value)
        {
            return 
                Mathf.RoundToInt(value);
        }

        /// <summary>
        /// Returns the square root of the <see cref="float"/>.
        /// </summary>
        /// <returns>The <see cref="float"/>'s square root.</returns>
        public static float Sqrt(this float value)
        {
            return 
                Mathf.Sqrt(value);
        }
    }
}
