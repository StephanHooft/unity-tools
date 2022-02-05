using UnityEngine;

namespace StephanHooft.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="bool"/>.
    /// </summary>
    public static class BoolExtensions
    {
        #region Static Methods

        /// <summary>
        /// Converts the given <see cref="bool"/> value to a <see cref="float"/>.
        /// </summary>
        /// <param name="trueFloat">The <see cref="float"/> to return if the condition is <see cref="true"/>.</param>
        /// <param name="falseFloat">The <see cref="float"/> to return if the condition is <see cref="false"/>.</param>
        /// <returns>Returns <paramref name="trueFloat"/> if the given value is <see cref="true"/>; <paramref name="falseFloat"/>
        /// otherwise.</returns>
        public static float ToFloat(this bool b, float trueFloat = 1, float falseFloat = 0)
        {
            return
                b ? trueFloat : falseFloat;
        }

        /// <summary>
        /// Converts the given <see cref="bool"/> value to an <see cref="int"/>.
        /// </summary>
        /// <param name="trueInt">The <see cref="int"/> to return if the condition is <see cref="true"/>.</param>
        /// <param name="falseInt">The <see cref="int"/> to return if the condition is <see cref="false"/>.</param>
        /// <returns>Returns <paramref name="trueInt"/> if the given value is <see cref="true"/>, <paramref name="falseInt"/>
        /// otherwise.</returns>
        public static int ToInt(this bool b, int trueInt = 1, int falseInt = 0)
        {
            return
                b ? trueInt : falseInt;
        }

        /// <summary>
        /// Converts the given <see cref="bool"/> value to a <see cref="string"/>.
        /// </summary>
        /// <param name="trueString">The <see cref="string"/> to be returned if the condition is <see cref="true"/>.</param>
        /// <param name="falseString">The <see cref="string"/> to be returned if the condition is <see cref="false"/>.</param>
        /// <returns>Returns <paramref name="trueString"/> if the given value is <see cref="true"/>; <paramref name="falseString"/>
        /// otherwise.</returns>
        public static string ToString(this bool b, string trueString, string falseString)
        {
            return
                b ? trueString : falseString;
        }

        /// <summary>
        /// Converts the given <see cref="bool"/> value to a <typeparamref name="T"/>.
        /// </summary>
        /// <param name="trueValue">The <typeparamref name="T"/> to be returned if the condition is <see cref="true"/>.</param>
        /// <param name="falseValue">The <typeparamref name="T"/> to be returned if the condition is <see cref="false"/>.</param>
        /// <typeparam name="T">Instance of any class.</typeparam>
        /// <returns>Returns <paramref name="trueValue"/> if the given value is <see cref="true"/>; <paramref name="falseValue"/>
        /// otherwise.</returns>
        public static T ToType<T>(this bool b, T trueValue, T falseValue)
        {
            return
                b ? trueValue : falseValue;
        }

        /// <summary>
        /// Converts the given <see cref="bool"/> value to a <see cref="Vector2"/>.
        /// </summary>
        /// <param name="trueVector">The <see cref="Vector2"/> to be returned if the condition is <see cref="true"/>.</param>
        /// <param name="falseVector">The <see cref="Vector2"/> to be returned if the condition is <see cref="false"/>.</param>
        /// <typeparam name="T">Instance of any class.</typeparam>
        /// <returns>Returns <paramref name="trueVector"/> if the given value is <see cref="true"/>; <paramref name="falseVector"/>
        /// otherwise.</returns>
        public static Vector2 ToVector2(this bool b, Vector2 trueVector, Vector2 falseVector)
        {
            return
                b ? trueVector : falseVector;
        }

        /// <summary>
        /// Converts the given <see cref="bool"/> value to a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="trueVector">The <see cref="Vector3"/> to be returned if the condition is <see cref="true"/>.</param>
        /// <param name="falseVector">The <see cref="Vector3"/> to be returned if the condition is <see cref="false"/>.</param>
        /// <typeparam name="T">Instance of any class.</typeparam>
        /// <returns>Returns <paramref name="trueVector"/> if the given value is <see cref="true"/>; <paramref name="falseVector"/>
        /// otherwise.</returns>
        public static Vector3 ToVector3(this bool b, Vector3 trueVector, Vector3 falseVector)
        {
            return
                b ? trueVector : falseVector;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
