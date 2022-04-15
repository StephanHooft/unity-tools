namespace StephanHooft.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="double"/>.
    /// </summary>
    public static class DoubleExtensions
    {
        #region Constants

        private const double deg2Rad = 0.0174532924;
        private const double rad2Deg = 57.29578;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Static Methods

        /// <summary>
        /// Returns the absolute value of the <see cref="double"/>.
        /// </summary>
        /// <returns>The absolute value of the <see cref="double"/>.</returns>
        public static double Abs(this double d)
        {
            return
                System.Math.Abs(d);
        }

        /// <summary>
        /// Returns the angle (in either radians or degrees) whose cosine is the <see cref="double"/> value.
        /// </summary>
        /// <param name="returnDegrees">Set to <see cref="true"/> to return degrees instead of radians.</param>
        /// <returns>A <see cref="double"/> angle (in either radians or degrees).</returns>
        public static double ArcCos(this double d, bool returnDegrees = false)
        {
            var result = System.Math.Acos(d);
            if (returnDegrees)
                result *= rad2Deg;
            return
                result;
        }

        /// <summary>
        /// Returns the angle (in either radians or degrees) whose sine is the <see cref="double"/> value.
        /// </summary>
        /// <param name="returnDegrees">Set to <see cref="true"/> to return degrees instead of radians.</param>
        /// <returns>A <see cref="double"/> angle (in either radians or degrees).</returns>
        public static double ArcSin(this double d, bool returnDegrees = false)
        {
            var result = System.Math.Asin(d);
            if (returnDegrees)
                result *= rad2Deg;
            return
                result;
        }

        /// <summary>
        /// Returns the angle (in either radians or degrees) whose tangent is the <see cref="double"/> value.
        /// </summary>
        /// <param name="returnDegrees">Set to <see cref="true"/> to return degrees instead of radians.</param>
        /// <returns>A <see cref="double"/> angle (in either radians or degrees).</returns>
        public static double ArcTan(this double d, bool returnDegrees = false)
        {
            var result = System.Math.Atan(d);
            if (returnDegrees)
                result *= rad2Deg;
            return
                result;
        }

        /// <summary>
        /// Clamps the given <see cref="double"/> value between the given minimum <see cref="double"/> and maximum
        /// <see cref="double"/> values. 
        /// <para>Returns the given value if it is within the <paramref name="min"/> and <paramref name="max"/> range.</para>
        /// </summary>
        /// <param name="min">The minimum value of the range to clamp to.</param>
        /// <param name="max">The maximum value of the range to clamp to.</param>
        /// <returns>The <see cref="double"/> result between the <paramref name="min"/> and <paramref name="max"/>
        /// values.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">If <paramref name="min"/> is greater than
        /// <paramref name="max"/>.</exception>
        public static double Clamp(this double d, double min, double max)
        {
            if (max <= min)
                throw
                    new System.ArgumentOutOfRangeException("max");
            if (d < min)
                d = min;
            else if (d > max)
                d = max;
            return
                d;
        }

        /// <summary>
        /// Returns the cosine of the <see cref="double"/> angle.
        /// </summary>
        /// <param name="useDegrees">Set to <see cref="true"/> to use degrees instead of radians.</param>
        /// <returns>The return value between -1 and 1.</returns>
        public static double Cos(this double d, bool useDegrees = false)
        {
            if (useDegrees)
                d *= deg2Rad;
            return
                System.Math.Cos(d);
        }

        /// <summary>
        /// Checks if the <see cref="double"/> is above a certain <paramref name="lower"/> value.
        /// </summary>
        /// <param name="lower">The <see cref="double"/> value to check against.</param>
        /// <returns><see cref="true"/> if the <see cref="double"/> is greater than <paramref name="lower"/>.</returns>
        public static bool IsAbove(this double d, double lower)
        {
            return
                d > lower;
        }

        /// <summary>
        /// Checks if the <see cref="double"/> is above or equal to a certain <paramref name="lower"/> value.
        /// </summary>
        /// <param name="lower">The <see cref="double"/> value to check against.</param>
        /// <returns><see cref="true"/> if the <see cref="double"/> is greater than or equal to
        /// <paramref name="lower"/>.</returns>
        public static bool IsAboveOrEqualTo(this double d, double lower)
        {
            return
                d >= lower;
        }

        /// <summary>
        /// Checks if the <see cref="double"/> is below a certain <paramref name="upper"/> value.
        /// </summary>
        /// <param name="upper">The <see cref="double"/> value to check against.</param>
        /// <returns><see cref="true"/> if the <see cref="double"/> is smaller than <paramref name="upper"/>.</returns>
        public static bool IsBelow(this double d, double upper)
        {
            return
                d < upper;
        }

        /// <summary>
        /// Checks if the <see cref="double"/> is below or equal to a certain <paramref name="upper"/> value.
        /// </summary>
        /// <param name="upper">The <see cref="double"/> value to check against.</param>
        /// <returns><see cref="true"/> if the <see cref="double"/> is smaller than or equal to
        /// <paramref name="upper"/>.</returns>
        public static bool IsBelowOrEqualTo(this double d, double upper)
        {
            return
                d <= upper;
        }

        /// <summary>
        /// Checks if the <see cref="double"/> is equal to a certain <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="double"/> value to compare with.</param>
        /// <returns><see cref="true"/> if the <see cref="double"/> is equal to <paramref name="value"/>.</returns>
        public static bool IsEqualTo(this double d, double value)
        {
            return
                d == value;
        }

        /// <summary>
        /// Checks if the <see cref="double"/> is unequal to a certain <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="double"/> value to compare with.</param>
        /// <returns><see cref="true"/> if the <see cref="double"/> is not equal to <paramref name="value"/>.</returns>
        public static bool IsNotEqualTo(this double d, double value)
        {
            return
                d != value;
        }

        /// <summary>
        /// Checks if the <see cref="double"/> has a negative value.
        /// </summary>
        /// <returns>True if the <see cref="double"/> value is smaller than 0.</returns>
        public static bool IsNegative(this double d)
        {
            return
                d < 0d;
        }

        /// <summary>
        /// Checks if the <see cref="double"/> has a positive value.
        /// </summary>
        /// <returns>True if the <see cref="double"/> value is greater than 0.</returns>
        public static bool IsPositive(this double d)
        {
            return
                d > 0d;
        }

        /// <summary>
        /// Checks if the <see cref="double"/> is within a certain range.
        /// </summary>
        /// <param name="lower">The lower bound of the <see cref="double"/> range to check against.</param>
        /// <param name="upper">The upper bound of the <see cref="double"/> range to check against.</param>
        /// <returns><see cref="true"/> if the <see cref="double"/> value is within the specified values</returns>
        public static bool IsWithinRange(this double d, double lower, double upper)
        {
            return
                d >= lower && d <= upper;
        }

        /// <summary>
        /// Checks if the <see cref="double"/> has a value of 0.
        /// </summary>
        /// <returns>True if the <see cref="double"/> value is 0.</returns>
        public static bool IsZero(this double d)
        {
            return
                d == 0d;
        }

        /// <summary>
        /// Re-maps a <see cref="double"/> from one range to another.
        /// <para>Does not clamp values within the range by default, because out-of-range values are sometimes intended
        /// and useful.</para>
        /// </summary>
        /// <param name="inMin">The lower bound of the <see cref="double"/>'s current range.</param>
        /// <param name="inMax">The upper bound of the <see cref="double"/>'s current range.</param>
        /// <param name="outMin">The lower bound of the <see cref="double"/>'s target range.</param>
        /// <param name="outMax">The upper bound of the <see cref="double"/>'s target range </param>
        /// <param name="clamp">Set to true to clamp the <see cref="double"/>.</param>
        /// <returns>The mapped <see cref="double"/> value.</returns>
        public static double Map
            (this double d, double inMin, double inMax, double outMin, double outMax, bool clamp = false)
        {
            if (clamp)
                d = d.Clamp(inMin, inMax);
            return
                (d - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }

        /// <summary>
        /// Returns the <see cref="double"/> with a sign matching an<paramref name="other"/> <see cref="double"/>.
        /// </summary>
        /// <param name="other">The other <see cref="double"/> whose sign to match.</param>
        /// <returns>The <see cref="double"/> with its sign matching <paramref name="other"/>.</returns>
        public static double MatchingSign(this double d, double other)
        {
            if(System.Math.Sign(d) == System.Math.Sign(other))
                return
                    d;
            else
                return
                    -d;
        }

        /// <summary>
        /// Returns the <see cref="double"/> with a sign matching a <see cref="float"/>.
        /// </summary>
        /// <param name="other">The <see cref="float"/> whose sign to match.</param>
        /// <returns>The <see cref="double"/> with its sign matching <paramref name="other"/>.</returns>
        public static double MatchingSign(this double d, float other)
        {
            if (System.Math.Sign(d) == System.Math.Sign(other))
                return
                    d;
            else
                return
                    -d;
        }

        /// <summary>
        /// Returns the largest of two <see cref="double"/>s.
        /// </summary>
        /// <param name="other">The other <see cref="double"/> to compare against.</param>
        /// <returns>The largest value between the <see cref="double"/> and <paramref name="other"/>.</returns>
        public static double Max(this double d, double other)
        {
            return
                System.Math.Max(d, other);
        }

        /// <summary>
        /// Returns the largest <see cref="double"/>.
        /// </summary>
        /// <param name="others">A range of <see cref="double"/>s to compare against.</param>
        /// <returns>The largest <see cref="double"/> value.</returns>
        public static double Max(this double d, double[] others)
        {
            foreach (double other in others)
                if (other > d)
                    d = other;
            return
                d;
        }

        /// <summary>
        /// Returns the smallest of two <see cref="double"/>s.
        /// </summary>
        /// <param name="other">The other <see cref="double"/> to compare against.</param>
        /// <returns>The largest value between the <see cref="double"/> and <paramref name="other"/>.</returns>
        public static double Min(this double d, double other)
        {
            return
                System.Math.Min(d, other);
        }

        /// <summary>
        /// Returns the smallest <see cref="double"/>.
        /// </summary>
        /// <param name="others">A range of <see cref="double"/>s to compare against.</param>
        /// <returns>The smallest <see cref="double"/> value.</returns>
        public static double Min(this double d, double[] others)
        {
            foreach (double other in others)
                if (other < d)
                    d = other;
            return
                d;
        }

        /// <summary>
        /// Ensures that the <see cref="double"/> value is above a set <paramref name="lower"/> <see cref="double"/>
        /// value.
        /// <para>An exception with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="lower">The value that the <see cref="double"/> must remain above.</param>
        /// <param name="paramName">The parameter name to use if an exception is thrown.</param>
        /// <returns>The original <see cref="double"/> value, assuming no exception was thrown.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">If the <see cref="double"/> is lower than or equal to
        /// <paramref name="lower"/>.</exception>
        public static double MustBeAbove(this double d, double lower, string paramName)
        {
            if (d > lower)
                return
                    d;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="double"/> value is above or equal to a set <paramref name="lower"/>
        /// <see cref="double"/> value.
        /// <para>An exception with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="lower">The value that the <see cref="double"/> must remain above or equal to.</param>
        /// <param name="paramName">The parameter name to use if an exception is thrown.</param>
        /// <returns>The original <see cref="double"/> value, assuming no exception was thrown.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">If the <see cref="double"/> is lower than
        /// <paramref name="lower"/>.</exception>
        public static double MustBeAboveOrEqualTo(this double d, double lower, string paramName)
        {
            if (d >= lower)
                return
                    d;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="double"/> value is equal to a certain <paramref name="value"/>.
        /// <para>An exception will be thrown if the <see cref="double"/> does not equal the <paramref name="value"/>.</para>
        /// </summary>
        /// <param name="value">The <see cref="double"/> value to check against.</param>
        /// <param name="paramName">The parameter name to use if an exception is thrown.</param>
        /// <returns>The original <see cref="double"/> value, assuming no exception was thrown.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">If the <see cref="double"/> is not equal to
        /// <paramref name="value"/>.</exception>
        public static double MustBeEqualTo(this double d, double value, string paramName)
        {
            if (d == value)
                return
                    d;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="double"/> value is below a set <paramref name="upper"/> <see cref="double"/> value.
        /// <para>An exception with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="upper">The value that the <see cref="double"/> must remain below.</param>
        /// <param name="paramName">The parameter name to use if an exception is thrown.</param>
        /// <returns>The original <see cref="double"/> value, assuming no exception was thrown.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">If the <see cref="double"/> is greater than or equal to
        /// <paramref name="upper"/>.</exception>
        public static double MustBeBelow(this double d, double upper, string paramName)
        {
            if (d < upper)
                return
                    d;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="double"/> value is below or equal to a set <paramref name="upper"/> <see cref="double"/> value.
        /// <para>An exception with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="upper">The value that the <see cref="double"/> must remain below or equal to.</param>
        /// <param name="paramName">The parameter name to use if an exception is thrown.</param>
        /// <returns>The original <see cref="double"/> value, assuming no exception was thrown.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">If the <see cref="double"/> is greater than
        /// <paramref name="upper"/>.</exception>
        public static double MustBeBelowOrEqualTo(this double d, double upper, string paramName)
        {
            if (d <= upper)
                return
                    d;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="double"/> value is within a range determined by a <paramref name="lower"/> 
        /// and an <paramref name="upper"/> <see cref="double"/> value.
        /// <para>An exception with <paramref name="paramName"/> is thrown if the <see cref="double"/>
        /// goes outside of this range.</para>
        /// </summary>
        /// <param name="lower">The value that the <see cref="double"/> must remain above or equal to.</param>
        /// <param name="upper">The value that the <see cref="double"/> must remain below or equal to.</param>
        /// <param name="paramName">The parameter name to use if an exception is thrown.</param>
        /// <returns>The original <see cref="double"/> value, assuming no exception was thrown.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">If the <see cref="double"/> is lower than
        /// <paramref name="lower"/> or greater than <paramref name="upper"/>.</exception>
        public static double MustBeWithinRange(this double d, double lower, double upper, string paramName)
        {
            if (d >= lower && d <= upper)
                return
                    d;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Ensures that the <see cref="double"/> value is not equal to a certain <paramref name="value"/>.
        /// <para>An exception will be thrown if the <see cref="double"/> equals the <paramref name="value"/>.</para>
        /// </summary>
        /// <param name="value">The <see cref="double"/> value to check against.</param>
        /// <param name="paramName">The parameter name to use if an exception is thrown.</param>
        /// <returns>The original <see cref="double"/> value, assuming no exception was thrown.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">If the <see cref="double"/> is not equal to
        /// <paramref name="value"/>.</exception>
        public static double MustNotBeEqualTo(this double d, double value, string paramName)
        {
            if (d != value)
                return
                    d;
            else
                throw
                    new System.ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Returns the <see cref="double"/> with a sign opposite to an <paramref name="other"/> <see cref="double"/>.
        /// </summary>
        /// <param name="other">The other <see cref="double"/> whose sign to oppose.</param>
        /// <returns>The <see cref="double"/> with its sign opposite to that of <paramref name="other"/>.</returns>
        public static double OppositeSign(this double d, double other)
        {
            if (System.Math.Sign(d) != System.Math.Sign(other))
                return
                    d;
            else
                return
                    -d;
        }

        /// <summary>
        /// Returns the <see cref="double"/> with a sign opposite to a <see cref="float"/>.
        /// </summary>
        /// <param name="other">The <see cref="float"/> whose sign to oppose.</param>
        /// <returns>The <see cref="double"/> with its sign opposite to that of <paramref name="other"/>.</returns>
        public static double OppositeSign(this double d, float other)
        {
            if (System.Math.Sign(d) != System.Math.Sign(other))
                return
                    d;
            else
                return
                    -d;
        }

        /// <summary>
        /// Returns the <see cref="double"/> raised to power <paramref name="p"/>.
        /// </summary>
        /// <param name="p">The power to raise the <see cref="double"/> to.</param>
        /// <returns>The <see cref="double"/> raised to power <paramref name="p"/>.</returns>
        public static double Pow(this double d, double p)
        {
            return
                System.Math.Pow(d, p);
        }

        /// <summary>
        /// Rounds the <see cref="double"/> to the nearest integer.
        /// </summary>
        /// <returns>The <see cref="double"/> to the nearest integer.</returns>
        public static double Round(this double d)
        {
            return
                System.Math.Round(d);
        }

        /// <summary>
        /// Rounds the <see cref="double"/> to the nearest <see cref="int"/>.
        /// </summary>
        /// <returns>The <see cref="double"/> to the nearest <see cref="int"/>.</returns>
        public static int RoundToInt(this double d)
        {
            return
                (int)System.Math.Round(d);
        }

        /// <summary>
        /// Returns the <see cref="double"/>'s sign.
        /// </summary>
        /// <returns>The <see cref="double"/>'s sign.</returns>
        public static double Sign(this double d)
        {
            return
                System.Math.Sign(d);
        }

        /// <summary>
        /// Returns the sine of the <see cref="double"/> angle.
        /// </summary>
        /// <param name="useDegrees">Set to <see cref="true"/> to use degrees instead of radians.</param>
        /// <returns>The return value between -1 and 1.</returns>
        public static double Sin(this double d, bool useDegrees = false)
        {
            if (useDegrees)
                d *= deg2Rad;
            return
                System.Math.Sin(d);
        }

        /// <summary>
        /// Returns the square root of the <see cref="double"/>.
        /// </summary>
        /// <returns>The <see cref="double"/>'s square root.</returns>
        public static double Sqrt(this double d)
        {
            return
                System.Math.Sqrt(d);
        }

        /// <summary>
        /// Returns the tangent of the <see cref="double"/> angle.
        /// </summary>
        /// <param name="useDegrees">Set to <see cref="true"/> to use degrees instead of radians.</param>
        /// <returns>The tangent of the <see cref="double"/> angle.</returns>
        public static double Tan(this double d, bool useDegrees = false)
        {
            if (useDegrees)
                d *= deg2Rad;
            return
                System.Math.Tan(d);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
