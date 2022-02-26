namespace StephanHooft.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="double"/>.
    /// </summary>
    public static class DoubleExtensions
    {
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
        /// <para>Clamps the given <see cref="double"/> value between the given minimum <see cref="double"/> and maximum <see cref="double"/> values. 
        /// Returns the given value if it is within the <paramref name="min"/> and <paramref name="max"/> range.</para>
        /// </summary>
        /// <param name="min">The minimum value of the range to clamp to.</param>
        /// <param name="max">The maximum value of the range to clamp to.</param>
        /// <returns>The <see cref="double"/> result between the <paramref name="min"/> and <paramref name="max"/> values. </returns>
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
        /// Checks if the <see cref="double"/> has a positive value.
        /// </summary>
        /// <returns>True if the <see cref="double"/> value is greater than 0.</returns>
        public static bool IsPositive(this double d)
        {
            if (d > 0)
                return
                    true;
            else
                return
                    false;
        }

        /// <summary>
        /// Checks if the <see cref="double"/> has a negative value.
        /// </summary>
        /// <returns>True if the <see cref="double"/> value is smaller than 0.</returns>
        public static bool IsNegative(this double d)
        {
            if (d < 0)
                return
                    true;
            else
                return
                    false;
        }

        /// <summary>
        /// Checks if the <see cref="double"/> has a value of 0.
        /// </summary>
        /// <returns>True if the <see cref="double"/> value is 0.</returns>
        public static bool IsZero(this double d)
        {
            if (d == 0)
                return
                    true;
            else
                return
                    false;
        }

        /// <summary>
        /// <para>Re-maps a <see cref="double"/> from one range to another.</para>
        /// <para>Does not clamp values within the range by default, because out-of-range values are sometimes intended and useful.
        /// </summary>
        /// <param name="inMin">The lower bound of the <see cref="double"/>'s current range.</param>
        /// <param name="inMax">The upper bound of the <see cref="double"/>'s current range.</param>
        /// <param name="outMin">The lower bound of the <see cref="double"/>'s target range.</param>
        /// <param name="outMax">The upper bound of the <see cref="double"/>'s target range </param>
        /// <param name="clamp">Set to true to clamp the <see cref="double"/>.</param>
        /// <returns>The mapped <see cref="double"/> value.</returns>
        public static double Map(this double d, double inMin, double inMax, double outMin, double outMax, bool clamp = false)
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
        /// Ensures that the <see cref="double"/> value is above a set <paramref name="lower"/> <see cref="double"/> value.
        /// <para>An <see cref="System.ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="lower">The value that the <see cref="double"/> must remain above.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="System.ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="double"/> value, assuming no <see cref="System.ArgumentOutOfRangeException"/> was thrown.</returns>
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
        /// Ensures that the <see cref="double"/> value is above or equal to a set <paramref name="lower"/> <see cref="double"/> value.
        /// <para>An <see cref="System.ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="lower">The value that the <see cref="double"/> must remain above or equal to.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="System.ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="double"/> value, assuming no <see cref="System.ArgumentOutOfRangeException"/> was thrown.</returns>
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
        /// <para>An <see cref="System.ArgumentOutOfRangeException"/> will be thrown if the <see cref="double"/> does not equal the <paramref name="value"/>.</para>
        /// </summary>
        /// <param name="value">The <see cref="double"/> value to check against.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="System.ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="double"/> value, assuming no <see cref="System.ArgumentOutOfRangeException"/> was thrown.</returns>
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
        /// <para>An <see cref="System.ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="upper">The value that the <see cref="double"/> must remain below.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="System.ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="double"/> value, assuming no <see cref="System.ArgumentOutOfRangeException"/> was thrown.</returns>
        public static double MustBeLowerThan(this double d, double upper, string paramName)
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
        /// <para>An <see cref="System.ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown otherwise.</para>
        /// </summary>
        /// <param name="upper">The value that the <see cref="double"/> must remain below or equal to.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="System.ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="double"/> value, assuming no <see cref="System.ArgumentOutOfRangeException"/> was thrown.</returns>
        public static double MustBeLowerThanOrEqualTo(this double d, double upper, string paramName)
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
        /// <para>An <see cref="System.ArgumentOutOfRangeException"/> with <paramref name="paramName"/> is thrown if the <see cref="double"/>
        /// goes outside of this range.</para>
        /// </summary>
        /// <param name="lower">The value that the <see cref="double"/> must remain above or equal to.</param>
        /// <param name="upper">The value that the <see cref="double"/> must remain below or equal to.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="System.ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="double"/> value, assuming no <see cref="System.ArgumentOutOfRangeException"/> was thrown.</returns>
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
        /// <para>An <see cref="System.ArgumentOutOfRangeException"/> will be thrown if the <see cref="double"/> equals the <paramref name="value"/>.</para>
        /// </summary>
        /// <param name="value">The <see cref="double"/> value to check against.</param>
        /// <param name="paramName">The parameter name to use if an <see cref="System.ArgumentOutOfRangeException"/> is thrown.</param>
        /// <returns>The original <see cref="double"/> value, assuming no <see cref="System.ArgumentOutOfRangeException"/> was thrown.</returns>
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
        public static double Pow(this double d, float p)
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
        /// Returns the square root of the <see cref="double"/>.
        /// </summary>
        /// <returns>The <see cref="double"/>'s square root.</returns>
        public static double Sqrt(this double d)
        {
            return
                System.Math.Sqrt(d);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
