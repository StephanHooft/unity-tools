using StephanHooft.Extensions;
using UnityEngine;

namespace StephanHooft.VariableRanges
{
    /// <summary>
    /// A pair of lower and upper <see cref="float"/> values that encapsulate a certain range.
    /// </summary>
    [System.Serializable]
    public struct FloatRange
    {
        #region Properties

        /// <summary>
        /// The <see cref="FloatRange"/>'s total length.
        /// </summary>
        public float Length
            => upper - lower;

        /// <summary>
        /// The lower <see cref="float"/> value of the <see cref="FloatRange"/>.
        /// </summary>
        public float Lower => lower;

        /// <summary>
        /// The upper <see cref="float"/> value of the <see cref="FloatRange"/>.
        /// </summary>
        public float Upper => upper;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        [SerializeField]
        private float
            lower,
            upper;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="FloatRange"/>.
        /// </summary>
        /// <param name="lower">
        /// A lower <see cref="float"/> value.
        /// </param>
        /// <param name="upper">
        /// An upper <see cref="float"/> value. Must be greater than <paramref name="lower"/>.
        /// </param>
        public FloatRange(float lower, float upper)
        {
            this.lower = lower;
            this.upper = upper.MustBeAboveOrEqualTo(lower, "upper");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Operators

        /// <summary>
        /// Returns a <see cref="FloatRange"/> with values all multiplied by one <see cref="float"/> value.
        /// </summary>
        /// <returns>
        /// A <see cref="FloatRange"/>.
        /// </returns>
        public static FloatRange operator *(FloatRange a, float b)
            => new(a.lower * b, a.upper * b);

        /// <summary>
        /// Returns a <see cref="FloatRange"/> with values all multiplied by one <see cref="float"/> value.
        /// </summary>
        /// <returns>
        /// A <see cref="FloatRange"/>.
        /// </returns>
        public static FloatRange operator *(float a, FloatRange b)
            => new(b.lower * a, b.upper * a);

        /// <summary>
        /// Returns a <see cref="FloatRange"/> with values all divided by one <see cref="float"/> value.
        /// </summary>
        /// <returns>
        /// A <see cref="FloatRange"/>.
        /// </returns>
        public static FloatRange operator /(FloatRange a, float b)
            => new(a.lower / b, a.upper / b);

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="FloatRange"/>s are equal.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="FloatRange"/>s are equal.
        /// </returns>
        public static bool operator ==(FloatRange a, FloatRange b)
            => a.lower == b.lower
            && a.upper == b.upper;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="FloatRange"/>s are unequal.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="FloatRange"/>s are unequal.
        /// </returns>
        public static bool operator !=(FloatRange a, FloatRange b)
            => a.lower != b.lower
            || a.upper != b.upper;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="float"/> is below the <see cref="FloatRange"/>.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="float"/> is lower than the lower <see cref="FloatRange"/> value.
        /// </returns>
        public static bool operator <(float a, FloatRange b)
            => a < b.lower;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="float"/> is below or equal to the <see cref="FloatRange"/>.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="float"/> is lower than or equal to the lower <see cref="FloatRange"/>
        /// value.
        /// </returns>
        public static bool operator <=(float a, FloatRange b)
            => a <= b.lower;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="float"/> is above the <see cref="FloatRange"/>.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="float"/> is greater than the upper <see cref="FloatRange"/> value.
        /// </returns>
        public static bool operator >(float a, FloatRange b)
            => a > b.upper;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="float"/> is above or equal to the <see cref="FloatRange"/>.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="float"/> is greater than or equal to the upper <see cref="FloatRange"/>
        /// value.
        /// </returns>
        public static bool operator >=(float a, FloatRange b)
            => a >= b.upper;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="float"/> is on the <see cref="FloatRange"/>.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="FloatRange"/> contains the <see cref="float"/> value.
        /// </returns>
        public static bool operator &(float a, FloatRange b)
            => a >= b.lower && a <= b.upper;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="float"/> is on the <see cref="FloatRange"/>.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if the <see cref="FloatRange"/> contains the <see cref="float"/> value.
        /// </returns>
        public static bool operator &(FloatRange a, float b)
            => b >= a.lower && b <= a.upper;

        public override bool Equals(object obj)
            => obj is FloatRange other
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
        /// Clamps the given <see cref="float"/> <paramref name="value"/> between the lower and upper values of the
        /// <see cref="FloatRange"/>. Returns the given <paramref name="value"/> if it is within the
        /// <see cref="FloatRange"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="float"/> value to clamp.
        /// </param>
        /// <returns>
        /// The <see cref="float"/> result within the <see cref="FloatRange"/>.
        /// </returns>
        public float Clamp(float value)
            => Mathf.Clamp(value, lower, upper);

        /// <summary>
        /// Returns <see cref="true"/> if the given <see cref="float"/> <paramref name="value"/> is within the
        /// <see cref="FloatRange"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="float"/> value to test.
        /// </param>
        /// <returns>
        /// <see cref="true"/> if the <see cref="FloatRange"/> includes the <see cref="float"/>
        /// <paramref name="value"/>.
        /// </returns>
        public bool Includes(float value)
            => this & value;

        /// <summary>
        /// Re-maps a <see cref="float"/> <paramref name="value"/> from another range to the <see cref="FloatRange"/>.
        /// <para>Values outside the range are not clamped by default, because out-of-range values are sometimes
        /// intended and useful.</para>
        /// </summary>
        /// <param name="value">
        /// The <see cref="float"/> value to clamp.
        /// </param>
        /// <param name="inMin">
        /// The lower bound of the <see cref="float"/>'s current range.
        /// </param>
        /// <param name="inMax">
        /// The upper bound of the <see cref="float"/>'s current range.
        /// </param>
        /// <param name="clamp">
        /// Set to <see cref="true"/> to clamp the <see cref="float"/> <paramref name="value"/>.
        /// </param>
        /// <returns>
        /// The mapped <see cref="float"/> value.
        /// </returns>
        public float Map(float value, float inMin, float inMax, bool clamp = false)
            => value.Map(inMin, inMax, lower, upper, clamp);

        /// <summary>
        /// Re-maps a <see cref="float"/> <paramref name="value"/> from another <see cref="FloatRange"/> to this
        /// <see cref="FloatRange"/>.
        /// <para>Values outside the range are not clamped by default, because out-of-range values are sometimes
        /// intended and useful.</para>
        /// </summary>
        /// <param name="value">
        /// The <see cref="float"/> value to clamp.
        /// </param>
        /// <param name="inRange">
        /// The other <see cref="FloatRange"/> to map from.
        /// </param>
        /// <param name="clamp">
        /// Set to <see cref="true"/> to clamp the <see cref="float"/> <paramref name="value"/>.
        /// </param>
        /// <returns>
        /// The mapped <see cref="float"/> <paramref name="value"/>.
        /// </returns>
        public float Map(float value, FloatRange inRange, bool clamp = false)
            => value.Map(inRange.lower, inRange.upper, lower, upper, clamp);

        /// <summary>
        /// Returns a different <typeparamref name="T"/> depending on whether a <see cref="float"/>
        /// <paramref name="value"/> is below, on, or above the <see cref="FloatRange"/>.
        /// </summary>
        /// <typeparam name="T">
        /// A shared return type.
        /// </typeparam>
        /// <param name="value">
        /// The <see cref="float"/> value to test.
        /// </param>
        /// <param name="under">
        /// The <typeparamref name="T"/> to return if <paramref name="value"/> is below the <see cref="FloatRange"/>.
        /// </param>
        /// <param name="on">
        /// The <typeparamref name="T"/> to return if <paramref name="value"/> is on the <see cref="FloatRange"/>.
        /// </param>
        /// <param name="over">
        /// The <typeparamref name="T"/> to return if <paramref name="value"/> is above the <see cref="FloatRange"/>.
        /// </param>
        /// <returns>
        /// Either <paramref name="under"/>, <paramref name="on"/>, or <paramref name="over"/>.
        /// </returns>
        public T UnderOnOver<T>(float value, T under, T on, T over)
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
