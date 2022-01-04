namespace StephanHooft.Extensions
{
    public static class BoolExtensions
    {
        /// <summary>
        /// Converts the given boolean value to integer.
        /// </summary>
        /// <param name="item">The boolean variable.</param>
        /// <param name="trueInt">The int to return if the boolean is true.</param>
        /// <param name="falseInt">The int to return if the boolean is false.</param>
        /// <returns>Returns 1 if true , 0 otherwise.</returns>
        public static int ToInt(this bool item, int trueInt = 1, int falseInt = 0)
        {
            return item ? trueInt : falseInt;
        }

        /// <summary>
        /// Returns the <paramref name="trueString"/> or <paramref name="falseString"/> based on the given boolean value.
        /// </summary>
        /// <param name="item">The boolean value.</param>
        /// <param name="trueString">Value to be returned if the condition is true.</param>
        /// <param name="falseString">Value to be returned if the condition is false.</param>
        /// <returns>Returns <paramref name="trueString"/> if the given value is true otherwise <paramref name="falseString"/>.</returns>
        public static string ToString(this bool item, string trueString, string falseString)
        {
            return item ? trueString : falseString;
        }

        /// <summary>
        /// Returns the <paramref name="trueValue"/> or the <paramref name="falseValue"/> based on the given boolean value.
        /// </summary>
        /// <param name="item">The boolean value.</param>
        /// <param name="trueValue">Value to be returned if the condition is true.</param>
        /// <param name="falseValue">Value to be returned if the condition is false.</param>
        /// <typeparam name="T">Instance of any class.</typeparam>
        /// <returns>Returns <paramref name="trueValue"/> if the given value is true otherwise <paramref name="falseValue"/>.</returns>
        public static T ToType<T>(this bool item, T trueValue, T falseValue)
        {
            return item ? trueValue : falseValue;
        }
    }
}
