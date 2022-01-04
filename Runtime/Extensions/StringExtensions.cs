using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace StephanHooft.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns true if value is a <see cref="DateTime"/>.
        /// </summary>
        /// <returns>True if the value is a <see cref="DateTime"/>.</returns>
        public static bool IsDateTime(this string value)
        {
            try
            {
                return DateTime.TryParse(value, out DateTime tempDate);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if the value is an <see cref="int"/>.
        /// </summary>
        /// <returns>True if the value is an <see cref="int"/>.</returns>
        public static bool IsInt(this string value)
        {
            try
            {
                return int.TryParse(value, out int tempInt);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if the <see cref="string"/> is null or empty.
        /// </summary>
        /// <returns>True if the <see cref="string"/> is null or empty.</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Reverses the <see cref="string"/>.
        /// </summary>
        /// <returns>The <see cref="string"/> in reverse.</returns>
        public static string Reverse(this string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>
        /// Extracts the sub<see cref="string"/> starting from 'start' position to 'end' position.
        /// </summary>
        /// <param name="start">The start position.</param>
        /// <param name="end">The end position.</param>
        /// <returns>The sub<see cref="string"/>.</returns>
        public static string SubstringFromXToY(this string s, int start, int end)
        {
            if (s.IsNullOrEmpty())
                return string.Empty;
            if (start >= s.Length)
                return string.Empty;
            if (end >= s.Length)
                end = s.Length - 1;
            return s.Substring(start, end - start);
        }

        /// <summary>
        /// Returns a comma-delimited <see cref="string"/>.
        /// </summary>
        /// <returns>A comma-delimited <see cref="string"/>.</returns>
        public static string ToStringPretty<T>(this IEnumerable<T> source)
        {
            return (source == null) ? string.Empty : ToStringPretty(source, string.Empty, ",", string.Empty);
        }

        /// <summary>
        /// Returns a delimited string, appending <paramref name="before"/> at the start,
        /// and <paramref name="after"/> at the end of the <see cref="string"/>.
        /// <para>Ex: Enumerable.Range(0, 10).ToStringPretty("From 0 to 9: [", ",", "]")
        /// returns: From 0 to 9: [0,1,2,3,4,5,6,7,8,9].</para>
        /// </summary>
        /// <param name="before">A <see cref="string"/> to enter before the enumeration.</param>
        /// <param name="delimiter">Delimiter <see cref="string"/> to enter in between elements of the enumeration.</param>
        /// <param name="after">A <see cref="string"/> to enter after the enumeration.</param>
        /// <returns>A delimited <see cref="string"/>.</returns>
        public static string ToStringPretty<T>(this IEnumerable<T> source, string before, string delimiter, string after)
        {
            if (source == null)
                return string.Empty;
            StringBuilder result = new StringBuilder();
            result.Append(before);
            bool firstElement = true;
            foreach (T elem in source)
            {
                if (firstElement) firstElement = false;
                else result.Append(delimiter);
                result.Append(elem.ToString());
            }
            result.Append(after);
            return result.ToString();
        }

        /// <summary>
        /// Returns the string with a given <paramref name="colour"/>.
        /// </summary>
        /// <param name="colour">The colour to assign.</param>
        /// <returns>The string, with the assigned colour.</returns>
        public static string WithColour(this string text, Color colour) 
            => $"<color=#{ColorUtility.ToHtmlStringRGB(colour)}>{text}</color>";
    }
}
