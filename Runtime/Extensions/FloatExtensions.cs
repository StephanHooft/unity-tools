using UnityEngine;

namespace StephanHooft.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="float"/>.
    /// </summary>
    public static class FloatExtensions
    {
        #region Static Methods

        /// <summary>
        /// Returns the absolute value of the <see cref="float"/>.
        /// </summary>
        /// <returns>The absolute value of the <see cref="float"/>.</returns>
        public static float Abs(this float f)
        {
            return
                Mathf.Abs(f);
        }

        /// <summary>
        /// Returns the angle (in either radians or degrees) whose cosine is the <see cref="float"/> value.
        /// </summary>
        /// <param name="returnDegrees">Set to <see cref="true"/> to return degrees instead of radians.</param>
        /// <returns>A <see cref="float"/> angle (in either radians or degrees).</returns>
        public static float ArcCos(this float f, bool returnDegrees = false)
        {
            var result = Mathf.Acos(f);
            if (returnDegrees)
                result *= Mathf.Rad2Deg;
            return
                result;
        }

        /// <summary>
        /// Returns the angle (in either radians or degrees) whose sine is the <see cref="float"/> value.
        /// </summary>
        /// <param name="returnDegrees">Set to <see cref="true"/> to return degrees instead of radians.</param>
        /// <returns>A <see cref="float"/> angle (in either radians or degrees).</returns>
        public static float ArcSin(this float f, bool returnDegrees = false)
        {
            var result = Mathf.Asin(f);
            if (returnDegrees)
                result *= Mathf.Rad2Deg;
            return
                result;
        }

        /// <summary>
        /// Returns the angle (in either radians or degrees) whose tangent is the <see cref="float"/> value.
        /// </summary>
        /// <param name="returnDegrees">Set to <see cref="true"/> to return degrees instead of radians.</param>
        /// <returns>A <see cref="float"/> angle (in either radians or degrees).</returns>
        public static float ArcTan(this float f, bool returnDegrees = false)
        {
            var result = Mathf.Atan(f);
            if (returnDegrees)
                result *= Mathf.Rad2Deg;
            return
                result;
        }

        /// <summary>
        /// Clamps the given <see cref="float"/> value between the given minimum <see cref="float"/> and maximum <see cref="float"/> values. 
        /// <para>Returns the given value if it is within the <paramref name="min"/> and <paramref name="max"/> range.</para>
        /// </summary>
        /// <param name="min">The minimum value of the range to clamp to.</param>
        /// <param name="max">The maximum value of the range to clamp to.</param>
        /// <returns>The <see cref="float"/> result between the <paramref name="min"/> and <paramref name="max"/> values. </returns>
        public static float Clamp(this float f, float min, float max)
        {
            return
                Mathf.Clamp(f, min, max);
        }

        /// <summary>
        /// Returns the cosine of the <see cref="float"/> angle.
        /// </summary>
        /// <param name="useDegrees">Set to <see cref="true"/> to use degrees instead of radians.</param>
        /// <returns>The return value between -1 and 1.</returns>
        public static float Cos(this float f, bool useDegrees = false)
        {
            if (useDegrees)
                f *= Mathf.Deg2Rad;
            return
                Mathf.Cos(f);
        }

        /// <summary>
        /// Checks if the <see cref="float"/> is above a certain <paramref name="lower"/> value.
        /// </summary>
        /// <param name="lower">The <see cref="float"/> value to check against.</param>
        /// <returns><see cref="true"/> if the <see cref="float"/> is greater than <paramref name="lower"/>.</returns>
        public static bool IsAbove(this float f, float lower)
        {
            return
                f > lower;
        }

        /// <summary>
        /// Checks if the <see cref="float"/> is above or equal to a certain <paramref name="lower"/> value.
        /// </summary>
        /// <param name="lower">The <see cref="float"/> value to check against.</param>
        /// <returns><see cref="true"/> if the <see cref="float"/> is greater than or equal to
        /// <paramref name="lower"/>.</returns>
        public static bool IsAboveOrEqualTo(this float f, float lower)
        {
            return
                f >= lower;
        }

        /// <summary>
        /// Checks if the <see cref="float"/> is below a certain <paramref name="upper"/> value.
        /// </summary>
        /// <param name="upper">The <see cref="float"/> value to check against.</param>
        /// <returns><see cref="true"/> if the <see cref="float"/> is smaller than <paramref name="upper"/>.</returns>
        public static bool IsBelow(this float f, float upper)
        {
            return
                f < upper;
        }

        /// <summary>
        /// Checks if the <see cref="float"/> is below or equal to a certain <paramref name="upper"/> value.
        /// </summary>
        /// <param name="upper">The <see cref="float"/> value to check against.</param>
        /// <returns><see cref="true"/> if the <see cref="float"/> is smaller than or equal to
        /// <paramref name="upper"/>.</returns>
        public static bool IsBelowOrEqualTo(this float f, float upper)
        {
            return
                f <= upper;
        }

        /// <summary>
        /// Checks if the <see cref="float"/> is equal to a certain <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="float"/> value to compare with.</param>
        /// <returns><see cref="true"/> if the <see cref="float"/> is equal to <paramref name="value"/>.</returns>
        public static bool IsEqualTo(this float f, float value)
        {
            return
                f == value;
        }

        /// <summary>
        /// Checks if the <see cref="float"/> is unequal to a certain <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="float"/> value to compare with.</param>
        /// <returns><see cref="true"/> if the <see cref="float"/> is not equal to <paramref name="value"/>.</returns>
        public static bool IsNotEqualTo(this float f, float value)
        {
            return
                f != value;
        }

        /// <summary>
        /// Checks if the <see cref="float"/> has a negative value.
        /// </summary>
        /// <returns><see cref="true"/> if the <see cref="float"/> value is smaller than 0.</returns>
        public static bool IsNegative(this float f)
        {
            if (f < 0)
                return
                    true;
            else
                return
                    false;
        }

        /// <summary>
        /// Checks if the <see cref="float"/> has a positive value.
        /// </summary>
        /// <returns><see cref="true"/> if the <see cref="float"/> value is greater than 0.</returns>
        public static bool IsPositive(this float f)
        {
            if (f > 0)
                return
                    true;
            else
                return
                    false;
        }

        /// <summary>
        /// Checks if the <see cref="float"/> is within a certain range.
        /// </summary>
        /// <param name="lower">The lower bound of the <see cref="float"/> range to check against.</param>
        /// <param name="upper">The upper bound of the <see cref="float"/> range to check against.</param>
        /// <returns><see cref="true"/> if the <see cref="float"/> value is within the specified values</returns>
        public static bool IsWithinRange(this float f, float lower, float upper)
        {
            return
                f >= lower && f <= upper;
        }

        /// <summary>
        /// Checks if the <see cref="float"/> has a value of 0.
        /// </summary>
        /// <returns>True if the <see cref="float"/> value is 0.</returns>
        public static bool IsZero(this float f)
        {
            return
                f == 0;
        }

        /// <summary>
        /// Re-maps a <see cref="float"/> from one range to another.
        /// <para>Does not clamp values within the range by default, because out-of-range values are sometimes intended and useful.</para>
        /// </summary>
        /// <param name="inMin">The lower bound of the <see cref="float"/>'s current range.</param>
        /// <param name="inMax">The upper bound of the <see cref="float"/>'s current range.</param>
        /// <param name="outMin">The lower bound of the <see cref="float"/>'s target range.</param>
        /// <param name="outMax">The upper bound of the <see cref="float"/>'s target range </param>
        /// <param name="clamp">Set to true to clamp the <see cref="float"/>.</param>
        /// <returns>The mapped <see cref="float"/> value.</returns>
        public static float Map(this float f, float inMin, float inMax, float outMin, float outMax, bool clamp = false)
        {
            if (clamp)
                f = f.Clamp(inMin, inMax);
            return
                (f - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }

        /// <summary>
        /// Returns the <see cref="float"/> with a sign matching an<paramref name="other"/> <see cref="float"/>.
        /// </summary>
        /// <param name="other">The other <see cref="float"/> whose sign to match.</param>
        /// <returns>The <see cref="float"/> with its sign matching <paramref name="other"/>.</returns>
        public static float MatchingSign(this float f, float other)
        {
            if (Mathf.Sign(f) == Mathf.Sign(other))
                return
                    f;
            else
                return
                    -f;
        }

        /// <summary>
        /// Returns the <see cref="float"/> with a sign matching a <see cref="double"/>.
        /// </summary>
        /// <param name="other">The <see cref="double"/> whose sign to match.</param>
        /// <returns>The <see cref="float"/> with its sign matching <paramref name="other"/>.</returns>
        public static float MatchingSign(this float f, double other)
        {
            if (System.Math.Sign(f) == System.Math.Sign(other))
                return
                    f;
            else
                return
                    -f;
        }

        /// <summary>
        /// Returns the largest of two <see cref="float"/>s.
        /// </summary>
        /// <param name="other">The other <see cref="float"/> to compare against.</param>
        /// <returns>The largest value between the <see cref="float"/> and <paramref name="other"/>.</returns>
        public static float Max(this float f, float other)
        {
            return
                Mathf.Max(f, other);
        }

        /// <summary>
        /// Returns the largest <see cref="float"/>.
        /// </summary>
        /// <param name="others">A range of <see cref="float"/>s to compare against.</param>
        /// <returns>The largest <see cref="float"/> value.</returns>
        public static float Max(this float f, float[] others)
        {
            var max = Mathf.Max(others);
            return
                Mathf.Max(f, max);
        }

        /// <summary>
        /// Returns the smallest of two <see cref="float"/>s.
        /// </summary>
        /// <param name="other">The other <see cref="float"/> to compare against.</param>
        /// <returns>The largest value between the <see cref="float"/> and <paramref name="other"/>.</returns>
        public static float Min(this float f, float other)
        {
            return
                Mathf.Min(f, other);
        }

        /// <summary>
        /// Returns the smallest <see cref="float"/>.
        /// </summary>
        /// <param name="others">A range of <see cref="float"/>s to compare against.</param>
        /// <returns>The smallest <see cref="float"/> value.</returns>
        public static float Min(this float f, float[] others)
        {
            var min = Mathf.Min(others);
            return
                Mathf.Min(f, min);
        }

        /// <summary>
        /// Ensures that the <see cref="float"/> value is above a set <paramref name="lower"/> <see cref="float"/> value.
        /// <para>An exception with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="lower">The value that the <see cref="float"/> must remain above.</param>
        /// <param name="paramName">The parameter name to use if an exception is thrown.</param>
        /// <returns>The original <see cref="float"/> value, assuming no exception was thrown.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">If the <see cref="float"/> is lower than or equal to
        /// <paramref name="lower"/>.</exception>
        public static float MustBeAbove(this float f, float lower, string paramName)
        {
            if (f > lower)
                return
                    f;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="float"/> value is above or equal to a set <paramref name="lower"/> <see cref="float"/> value.
        /// <para>An exception with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="lower">The value that the <see cref="float"/> must remain above or equal to.</param>
        /// <param name="paramName">The parameter name to use if an exception is thrown.</param>
        /// <returns>The original <see cref="float"/> value, assuming no exception was thrown.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">If the <see cref="float"/> is lower than
        /// <paramref name="lower"/>.</exception>
        public static float MustBeAboveOrEqualTo(this float f, float lower, string paramName)
        {
            if (f >= lower)
                return
                    f;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="float"/> value is equal to a certain <paramref name="value"/>.
        /// <para>An exception will be thrown if the <see cref="float"/> does not equal the <paramref name="value"/>.</para>
        /// </summary>
        /// <param name="value">The <see cref="float"/> value to check against.</param>
        /// <param name="paramName">The parameter name to use if an exception is thrown.</param>
        /// <returns>The original <see cref="float"/> value, assuming no exception was thrown.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">If the <see cref="float"/> is not equal to
        /// <paramref name="value"/>.</exception>
        public static float MustBeEqualTo(this float f,float value, string paramName)
        {
            if (f == value)
                return
                    f;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="float"/> value is below a set <paramref name="upper"/> <see cref="float"/> value.
        /// <para>An exception with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="upper">The value that the <see cref="float"/> must remain below.</param>
        /// <param name="paramName">The parameter name to use if an exception is thrown.</param>
        /// <returns>The original <see cref="float"/> value, assuming no exception was thrown.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">If the <see cref="float"/> is greater than or equal to
        /// <paramref name="upper"/>.</exception>
        public static float MustBeBelow(this float f, float upper, string paramName)
        {
            if (f < upper)
                return
                    f;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="float"/> value is below or equal to a set <paramref name="upper"/> <see cref="float"/> value.
        /// <para>An exception with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="upper">The value that the <see cref="float"/> must remain below or equal to.</param>
        /// <param name="paramName">The parameter name to use if an exception is thrown.</param>
        /// <returns>The original <see cref="float"/> value, assuming no exception was thrown.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">If the <see cref="float"/> is greater than
        /// <paramref name="upper"/>.</exception>
        public static float MustBeBelowOrEqualTo(this float f, float upper, string paramName)
        {
            if (f <= upper)
                return
                    f;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="float"/> value is within a range determined by a <paramref name="lower"/> 
        /// and an <paramref name="upper"/> <see cref="float"/> value.
        /// <para>An exception with <paramref name="paramName"/> is thrown if the <see cref="float"/>
        /// goes outside of this range.</para>
        /// </summary>
        /// <param name="lower">The value that the <see cref="float"/> must remain above or equal to.</param>
        /// <param name="upper">The value that the <see cref="float"/> must remain below or equal to.</param>
        /// <param name="paramName">The parameter name to use if an exception is thrown.</param>
        /// <returns>The original <see cref="float"/> value, assuming no exception was thrown.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">If the <see cref="float"/> is lower than
        /// <paramref name="lower"/> or greater than <paramref name="upper"/>.</exception>
        public static float MustBeWithinRange(this float f, float lower, float upper, string paramName)
        {
            if (f >= lower && f <= upper)
                return
                    f;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="float"/> value is not equal to a certain <paramref name="value"/>.
        /// <para>An exception will be thrown if the <see cref="float"/> equals the <paramref name="value"/>.</para>
        /// </summary>
        /// <param name="value">The <see cref="float"/> value to check against.</param>
        /// <param name="paramName">The parameter name to use if an exception is thrown.</param>
        /// <returns>The original <see cref="float"/> value, assuming no exception was thrown.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">If the <see cref="float"/> is not equal to
        /// <paramref name="value"/>.</exception>
        public static float MustNotBeEqualTo(this float f, float value, string paramName)
        {
            if (f != value)
                return
                    f;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Returns the <see cref="float"/> with a sign opposite to an <paramref name="other"/> <see cref="float"/>.
        /// </summary>
        /// <param name="other">The other <see cref="float"/> whose sign to oppose.</param>
        /// <returns>The <see cref="float"/> with its sign opposite to that of <paramref name="other"/>.</returns>
        public static float OppositeSign(this float f, float other)
        {
            if (Mathf.Sign(f) != Mathf.Sign(other))
                return
                    f;
            else
                return
                    -f;
        }

        /// <summary>
        /// Returns the <see cref="float"/> with a sign opposite to a <see cref="double"/>.
        /// </summary>
        /// <param name="other">The <see cref="double"/> whose sign to oppose.</param>
        /// <returns>The <see cref="float"/> with its sign opposite to that of <paramref name="other"/>.</returns>
        public static float OppositeSign(this float f, double other)
        {
            if (System.Math.Sign(f) != System.Math.Sign(other))
                return
                    f;
            else
                return
                    -f;
        }

        /// <summary>
        /// Returns the <see cref="float"/> raised to power <paramref name="p"/>.
        /// </summary>
        /// <param name="p">The power to raise the <see cref="float"/> to.</param>
        /// <returns>The <see cref="float"/> raised to power <paramref name="p"/>.</returns>
        public static float Pow(this float f, float p)
        {
            return
                Mathf.Pow(f, p);
        }

        /// <summary>
        /// Rounds the <see cref="float"/> to the nearest integer.
        /// </summary>
        /// <returns>The <see cref="float"/> to the nearest integer.</returns>
        public static float Round(this float f)
        {
            return
                Mathf.Round(f);
        }

        /// <summary>
        /// Rounds the <see cref="float"/> to the nearest <see cref="int"/>.
        /// </summary>
        /// <returns>The <see cref="float"/> to the nearest <see cref="int"/>.</returns>
        public static int RoundToInt(this float f)
        {
            return
                Mathf.RoundToInt(f);
        }

        /// <summary>
        /// Returns the <see cref="float"/>'s sign.
        /// </summary>
        /// <returns>The <see cref="float"/>'s sign.</returns>
        public static float Sign(this float f)
        {
            return
                Mathf.Sign(f);
        }

        /// <summary>
        /// Returns the sine of the <see cref="float"/> angle.
        /// </summary>
        /// <param name="useDegrees">Set to <see cref="true"/> to use degrees instead of radians.</param>
        /// <returns>The return value between -1 and 1.</returns>
        public static float Sin(this float f, bool useDegrees = false)
        {
            if (useDegrees)
                f *= Mathf.Deg2Rad;
            return
                Mathf.Sin(f);
        }

        /// <summary>
        /// Returns the square root of the <see cref="float"/>.
        /// </summary>
        /// <returns>The <see cref="float"/>'s square root.</returns>
        public static float Sqrt(this float f)
        {
            return
                Mathf.Sqrt(f);
        }

        /// <summary>
        /// Returns the tangent of the <see cref="float"/> angle.
        /// </summary>
        /// <param name="useDegrees">Set to <see cref="true"/> to use degrees instead of radians.</param>
        /// <returns>The tangent of the <see cref="float"/> angle.</returns>
        public static float Tan(this float f, bool useDegrees = false)
        {
            if (useDegrees)
                f *= Mathf.Deg2Rad;
            return
                Mathf.Tan(f);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
