using StephanHooft.Extensions;
using UnityEngine;

namespace StephanHooft.VariableRanges
{
    /// <summary>
    /// A pair of lower and upper <see cref="double"/> values that encapsulate a certain range.
    /// </summary>
    [System.Serializable]
    public readonly struct DoubleRange
    {
        #region Properties

        /// <summary>
        /// The <see cref="DoubleRange"/>'s total length.
        /// </summary>
        public double Length
            => upper - lower;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        /// <summary>
        /// The lower <see cref="double"/> value of the <see cref="DoubleRange"/>.
        /// </summary>
        [SerializeField]
        public readonly double lower;

        /// <summary>
        /// The upper <see cref="double"/> value of the <see cref="DoubleRange"/>.
        /// </summary>
        [SerializeField]
        public readonly double upper;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="DoubleRange"/>.
        /// </summary>
        /// <param name="lower">
        /// A lower <see cref="double"/> value.
        /// </param>
        /// <param name="upper">
        /// An upper <see cref="double"/> value. Must be greater than <paramref name="lower"/>.
        /// </param>
        public DoubleRange(double lower, double upper)
        {
            this.lower = lower;
            this.upper = upper.MustBeAboveOrEqualTo(lower, "upper");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Operators

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="DoubleRange"/>s are equal.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="DoubleRange"/>s are equal.
        /// </returns>
        public static bool operator ==(DoubleRange a, DoubleRange b)
            => a.lower == b.lower
            && a.upper == b.upper;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="DoubleRange"/>s are unequal.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="DoubleRange"/>s are unequal.
        /// </returns>
        public static bool operator !=(DoubleRange a, DoubleRange b)
            => a.lower != b.lower
            || a.upper != b.upper;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="double"/> is below the <see cref="DoubleRange"/>.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="double"/> is lower than the lower <see cref="DoubleRange"/> value.
        /// </returns>
        public static bool operator <(double a, DoubleRange b)
            => a < b.lower;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="double"/> is below or equal to the <see cref="DoubleRange"/>.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="double"/> is lower than or equal to the lower <see cref="DoubleRange"/>
        /// value.
        /// </returns>
        public static bool operator <=(double a, DoubleRange b)
            => a <= b.lower;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="double"/> is above the <see cref="DoubleRange"/>.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="double"/> is greater than the upper <see cref="DoubleRange"/> value.
        /// </returns>
        public static bool operator >(double a, DoubleRange b)
            => a > b.upper;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="double"/> is above or equal to the <see cref="DoubleRange"/>.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="double"/> is greater than or equal to the upper <see cref="DoubleRange"/>
        /// value.
        /// </returns>
        public static bool operator >=(double a, DoubleRange b)
            => a >= b.upper;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="double"/> is on the <see cref="DoubleRange"/>.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="DoubleRange"/> contains the <see cref="double"/> value.
        /// </returns>
        public static bool operator &(double a, DoubleRange b)
            => a >= b.lower && a <= b.upper;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="double"/> is on the <see cref="DoubleRange"/>.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="DoubleRange"/> contains the <see cref="double"/> value.
        /// </returns>
        public static bool operator &(DoubleRange a, double b)
            => b >= a.lower && b <= a.upper;

        public override bool Equals(object obj)
            => obj is DoubleRange other
            && other.lower == lower
            && other.upper == upper;

        public override int GetHashCode()
            => (lower, upper).GetHashCode();

        public override string ToString()
            => string.Format("[{0} - {1}]", lower, upper);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// Clamps the given <see cref="double"/> <paramref name="value"/> between the lower and upper values of the
        /// <see cref="DoubleRange"/>. Returns the given <paramref name="value"/> if it is within the
        /// <see cref="DoubleRange"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="double"/> value to clamp.
        /// </param>
        /// <returns>
        /// The <see cref="double"/> result within the <see cref="DoubleRange"/>.
        /// </returns>
        public double Clamp(double value)
            => value.Clamp(lower, upper);

        /// <summary>
        /// Returns <see cref="true"/> if the given <see cref="double"/> <paramref name="value"/> is within the
        /// <see cref="DoubleRange"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="double"/> value to test.
        /// </param>
        /// <returns>
        /// <see cref="true"/> if the <see cref="DoubleRange"/> includes the <see cref="double"/>
        /// <paramref name="value"/>.
        /// </returns>
        public bool Includes(double value)
            => this & value;

        /// <summary>
        /// Re-maps a <see cref="double"/> <paramref name="value"/> from another range to the <see cref="DoubleRange"/>.
        /// <para>Values outside the range are not clamped by default, because out-of-range values are sometimes
        /// intended and useful.</para>
        /// </summary>
        /// <param name="value">
        /// The <see cref="double"/> value to clamp.
        /// </param>
        /// <param name="inMin">
        /// The lower bound of the <see cref="double"/>'s current range.
        /// </param>
        /// <param name="inMax">
        /// The upper bound of the <see cref="double"/>'s current range.
        /// </param>
        /// <param name="clamp">
        /// Set to <see cref="true"/> to clamp the <see cref="double"/> <paramref name="value"/>.
        /// </param>
        /// <returns>
        /// The mapped <see cref="double"/> value.
        /// </returns>
        public double Map(double value, double inMin, double inMax, bool clamp = false)
            => value.Map(inMin, inMax, lower, upper, clamp);

        /// <summary>
        /// Re-maps a <see cref="double"/> <paramref name="value"/> from another <see cref="DoubleRange"/> to this
        /// <see cref="DoubleRange"/>.
        /// <para>Values outside the range are not clamped by default, because out-of-range values are sometimes
        /// intended and useful.</para>
        /// </summary>
        /// <param name="value">
        /// The <see cref="double"/> value to clamp.
        /// </param>
        /// <param name="inRange">
        /// The other <see cref="DoubleRange"/> to map from.
        /// </param>
        /// <param name="clamp">
        /// Set to <see cref="true"/> to clamp the <see cref="double"/> <paramref name="value"/>.
        /// </param>
        /// <returns>
        /// The mapped <see cref="double"/> <paramref name="value"/>.
        /// </returns>
        public double Map(double value, DoubleRange inRange, bool clamp = false)
            => value.Map(inRange.lower, inRange.upper, lower, upper, clamp);

        /// <summary>
        /// Returns a different <typeparamref name="T"/> depending on whether a <see cref="double"/>
        /// <paramref name="value"/> is below, on, or above the <see cref="DoubleRange"/>.
        /// </summary>
        /// <typeparam name="T">
        /// A shared return type.
        /// </typeparam>
        /// <param name="value">
        /// The <see cref="double"/> value to test.
        /// </param>
        /// <param name="under">
        /// The <typeparamref name="T"/> to return if <paramref name="value"/> is below the <see cref="DoubleRange"/>.
        /// </param>
        /// <param name="on">
        /// The <typeparamref name="T"/> to return if <paramref name="value"/> is on the <see cref="DoubleRange"/>.
        /// </param>
        /// <param name="over">
        /// The <typeparamref name="T"/> to return if <paramref name="value"/> is above the <see cref="DoubleRange"/>.
        /// </param>
        /// <returns>
        /// Either <paramref name="under"/>, <paramref name="on"/>, or <paramref name="over"/>.
        /// </returns>
        public T UnderOnOver<T>(double value, T under, T on, T over)
        {
            if (value < lower)
                return
                    under;
            if (value > upper)
                return
                    over;
            return
                on;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
