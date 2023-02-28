using StephanHooft.Extensions;
using UnityEngine;

namespace StephanHooft.Variables.Ranges
{
    /// <summary>
    /// A pair of lower and upper <see cref="int"/> values that encapsulate a certain range.
    /// </summary>
    [System.Serializable]
    public readonly struct IntRange
    {
        #region Properties

        /// <summary>
        /// The <see cref="IntRange"/>'s total length.
        /// </summary>
        public int Length
            => upper - lower;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        /// <summary>
        /// The lower <see cref="int"/> value of the <see cref="IntRange"/>.
        /// </summary>
        public readonly int lower;

        /// <summary>
        /// The upper <see cref="int"/> value of the <see cref="IntRange"/>.
        /// </summary>
        public readonly int upper;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="IntRange"/>.
        /// </summary>
        /// <param name="lower">
        /// A lower <see cref="int"/> value.
        /// </param>
        /// <param name="upper">
        /// An upper <see cref="int"/> value. Must be greater than <paramref name="lower"/>.
        /// </param>
        public IntRange(int lower, int upper)
        {
            this.lower = lower;
            this.upper = upper.MustBeAboveOrEqualTo(lower, "upper");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Operators

        /// <summary>
        /// Returns a <see cref="IntRange"/> with values all multiplied by one <see cref="int"/> value.
        /// </summary>
        /// <returns>
        /// A <see cref="IntRange"/>.
        /// </returns>
        public static IntRange operator *(IntRange a, int b)
            => new(a.lower * b, a.upper * b);

        /// <summary>
        /// Returns a <see cref="IntRange"/> with values all multiplied by one <see cref="int"/> value.
        /// </summary>
        /// <returns>
        /// A <see cref="IntRange"/>.
        /// </returns>
        public static IntRange operator *(int a, IntRange b)
            => new(b.lower * a, b.upper * a);

        /// <summary>
        /// Returns a <see cref="IntRange"/> with values all divided by one <see cref="int"/> value.
        /// </summary>
        /// <returns>
        /// A <see cref="IntRange"/>.
        /// </returns>
        public static IntRange operator /(IntRange a, int b)
            => new(a.lower / b, a.upper / b);

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="IntRange"/>s are equal.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="IntRange"/>s are equal.
        /// </returns>
        public static bool operator ==(IntRange a, IntRange b)
            => a.lower == b.lower
            && a.upper == b.upper;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="IntRange"/>s are unequal.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="IntRange"/>s are unequal.
        /// </returns>
        public static bool operator !=(IntRange a, IntRange b)
            => a.lower != b.lower
            || a.upper != b.upper;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="int"/> is below the <see cref="IntRange"/>.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="int"/> is lower than the lower <see cref="IntRange"/> value.
        /// </returns>
        public static bool operator <(int a, IntRange b)
            => a < b.lower;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="int"/> is below or equal to the <see cref="IntRange"/>.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="int"/> is lower than or equal to the lower <see cref="IntRange"/>
        /// value.
        /// </returns>
        public static bool operator <=(int a, IntRange b)
            => a <= b.lower;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="int"/> is above the <see cref="IntRange"/>.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="int"/> is greater than the upper <see cref="IntRange"/> value.
        /// </returns>
        public static bool operator >(int a, IntRange b)
            => a > b.upper;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="int"/> is above or equal to the <see cref="IntRange"/>.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="int"/> is greater than or equal to the upper <see cref="IntRange"/>
        /// value.
        /// </returns>
        public static bool operator >=(int a, IntRange b)
            => a >= b.upper;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="int"/> is on the <see cref="IntRange"/>.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="IntRange"/> contains the <see cref="int"/> value.
        /// </returns>
        public static bool operator &(int a, IntRange b)
            => a >= b.lower && a <= b.upper;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="int"/> is on the <see cref="IntRange"/>.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="IntRange"/> contains the <see cref="int"/> value.
        /// </returns>
        public static bool operator &(IntRange a, int b)
            => b >= a.lower && b <= a.upper;

        public override bool Equals(object obj)
            => obj is IntRange other
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
        /// Clamps the given <see cref="int"/> <paramref name="value"/> between the lower and upper values of the
        /// <see cref="IntRange"/>. Returns the given <paramref name="value"/> if it is within the
        /// <see cref="IntRange"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="int"/> value to clamp.
        /// </param>
        /// <returns>
        /// The <see cref="int"/> result within the <see cref="IntRange"/>.
        /// </returns>
        public int Clamp(int value)
            => Mathf.Clamp(value, lower, upper);

        /// <summary>
        /// Returns <see cref="true"/> if the given <see cref="int"/> <paramref name="value"/> is within the
        /// <see cref="IntRange"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="int"/> value to test.
        /// </param>
        /// <returns>
        /// <see cref="true"/> if the <see cref="IntRange"/> includes the <see cref="int"/>
        /// <paramref name="value"/>.
        /// </returns>
        public bool Includes(int value)
            => this & value;

        /// <summary>
        /// Re-maps a <see cref="int"/> <paramref name="value"/> from another range to the <see cref="IntRange"/>.
        /// <para>Values outside the range are not clamped by default, because out-of-range values are sometimes
        /// intended and useful.</para>
        /// </summary>
        /// <param name="value">
        /// The <see cref="int"/> value to clamp.
        /// </param>
        /// <param name="inMin">
        /// The lower bound of the <see cref="int"/>'s current range.
        /// </param>
        /// <param name="inMax">
        /// The upper bound of the <see cref="int"/>'s current range.
        /// </param>
        /// <param name="clamp">
        /// Set to <see cref="true"/> to clamp the <see cref="int"/> <paramref name="value"/>.
        /// </param>
        /// <returns>
        /// The mapped <see cref="int"/> value.
        /// </returns>
        public int Map(int value, int inMin, int inMax, bool clamp = false)
            => value.Map(inMin, inMax, lower, upper, clamp);

        /// <summary>
        /// Re-maps a <see cref="int"/> <paramref name="value"/> from another <see cref="IntRange"/> to this
        /// <see cref="IntRange"/>.
        /// <para>Values outside the range are not clamped by default, because out-of-range values are sometimes
        /// intended and useful.</para>
        /// </summary>
        /// <param name="value">
        /// The <see cref="int"/> value to clamp.
        /// </param>
        /// <param name="inRange">
        /// The other <see cref="IntRange"/> to map from.
        /// </param>
        /// <param name="clamp">
        /// Set to <see cref="true"/> to clamp the <see cref="int"/> <paramref name="value"/>.
        /// </param>
        /// <returns>
        /// The mapped <see cref="int"/> <paramref name="value"/>.
        /// </returns>
        public int Map(int value, IntRange inRange, bool clamp = false)
            => value.Map(inRange.lower, inRange.upper, lower, upper, clamp);

        /// <summary>
        /// Returns a different <typeparamref name="T"/> depending on whether a <see cref="int"/>
        /// <paramref name="value"/> is below, on, or above the <see cref="IntRange"/>.
        /// </summary>
        /// <typeparam name="T">
        /// A shared return type.
        /// </typeparam>
        /// <param name="value">
        /// The <see cref="int"/> value to test.
        /// </param>
        /// <param name="under">
        /// The <typeparamref name="T"/> to return if <paramref name="value"/> is below the <see cref="IntRange"/>.
        /// </param>
        /// <param name="on">
        /// The <typeparamref name="T"/> to return if <paramref name="value"/> is on the <see cref="IntRange"/>.
        /// </param>
        /// <param name="over">
        /// The <typeparamref name="T"/> to return if <paramref name="value"/> is above the <see cref="IntRange"/>.
        /// </param>
        /// <returns>
        /// Either <paramref name="under"/>, <paramref name="on"/>, or <paramref name="over"/>.
        /// </returns>
        public T UnderOnOver<T>(int value, T under, T on, T over)
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
