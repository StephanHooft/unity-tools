using StephanHooft.Collections;

namespace StephanHooft.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.
    /// </summary>
    public static class MultiKeyDictionaryExtensions
    {
        #region Static Methods

        /// <summary>
        /// Adds the given <typeparamref name="TPrimaryKey"/> and <typeparamref name="TValue"/> to the given
        /// <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> if the <typeparamref name="TPrimaryKey"/>
        /// is NOT present. If present, the <typeparamref name="TValue"/> will be replaced with the new
        /// <typeparamref name="TValue"/>.
        /// </summary>
        /// <param name="primaryKey">The given <typeparamref name="TPrimaryKey"/>.</param>
        /// <param name="value">The given <typeparamref name="TValue"/>.</param>
        public static void AddOrReplace<TPrimaryKey, TSubKey, TValue>
            (this MultiKeyDictionary<TPrimaryKey, TSubKey, TValue> d, TPrimaryKey primaryKey, TValue value)
        {
            if (d.ContainsKey(primaryKey))
                d[primaryKey] = value;
            else
                d.Add(primaryKey, value);
        }

        /// <summary>
        /// Adds the given <typeparamref name="TPrimaryKey"/>, <typeparamref name="TSubKey"/>, and
        /// <typeparamref name="TValue"/> to the given <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> if
        /// the <typeparamref name="TPrimaryKey"/> is NOT present.
        /// If present, the <typeparamref name="TValue"/> will be replaced with the new <typeparamref name="TValue"/>.
        /// </summary>
        /// <param name="primaryKey">The given <typeparamref name="TPrimaryKey"/>.</param>
        /// <param name="subKey">The given <typeparamref name="TSubKey"/>.</param>
        /// <param name="value">The given <typeparamref name="TValue"/>.</param>
        public static void AddOrReplace<TPrimaryKey, TSubKey, TValue>
            (this MultiKeyDictionary<TPrimaryKey, TSubKey, TValue> d,
            TPrimaryKey primaryKey, TSubKey subKey, TValue value)
        {
            if (d.ContainsKey(primaryKey))
                d[primaryKey] = value;
            else
                d.Add(primaryKey, subKey, value);
        }

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> is empty.
        /// </summary>
        /// <returns><see cref="true"/> if the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> is empty.
        /// </returns>
        public static bool IsEmpty<TPrimaryKey, TSubKey, TValue>
            (this MultiKeyDictionary<TPrimaryKey, TSubKey, TValue> d)
        {
            return
                d.Count == 0;
        }

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> is
        /// <see cref="null"/>
        /// or empty.
        /// </summary>
        /// <returns><see cref="true"/> if the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> is
        /// <see cref="null"/>
        /// or empty.</returns>
        public static bool IsNullOrEmpty<TPrimaryKey, TSubKey, TValue>
            (this MultiKeyDictionary<TPrimaryKey, TSubKey, TValue> d)
        {
            return
                d == null || d.Count == 0;
        }

        /// <summary>
        /// Ensures that the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> contains a set 
        /// <typeparamref name="TPrimaryKey"/>.
        /// </summary>
        /// <param name="primaryKey">The <typeparamref name="TPrimaryKey"/> to check for.</param>
        /// <param name="dictionaryName">The <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> name to use
        /// if an exception is thrown.</param>
        /// <returns>The original <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.</returns>
        /// <exception cref="System.ArgumentException">If the <typeparamref name="TPrimaryKey"/> is not present.
        /// </exception>
        public static MultiKeyDictionary<TPrimaryKey, TSubKey, TValue> MustContainKey<TPrimaryKey, TSubKey, TValue>
            (this MultiKeyDictionary<TPrimaryKey, TSubKey, TValue> d, TPrimaryKey primaryKey, string dictionaryName)
        {
            if (d.ContainsKey(primaryKey))
                return
                    d;
            else
                throw
                    new System.ArgumentException(string.Format(
                        "Primary Key {0} must be present in MultiKeyDictionary<{1},{2},{3}> {4}.",
                    primaryKey, typeof(TPrimaryKey), typeof(TSubKey), typeof(TValue), dictionaryName));
        }

        /// <summary>
        /// Ensures that the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> contains a set 
        /// <typeparamref name="TSubKey"/>.
        /// </summary>
        /// <param name="subKey">The <typeparamref name="TSubKey"/> to check for.</param>
        /// <param name="dictionaryName">The <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> name to use
        /// if an exception is thrown.</param>
        /// <returns>The original <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.</returns>
        /// <exception cref="System.ArgumentException">If the <typeparamref name="TSubKey"/> is not present.
        /// </exception>
        public static MultiKeyDictionary<TPrimaryKey, TSubKey, TValue> MustContainKey<TPrimaryKey, TSubKey, TValue>
            (this MultiKeyDictionary<TPrimaryKey, TSubKey, TValue> d, TSubKey subKey, string dictionaryName)
        {
            if (d.ContainsKey(subKey))
                return
                    d;
            else
                throw
                    new System.ArgumentException(string.Format(
                        "Sub Key {0} must be present in MultiKeyDictionary<{1},{2},{3}> {4}.",
                    subKey, typeof(TPrimaryKey), typeof(TSubKey), typeof(TValue), dictionaryName));
        }

        /// <summary>
        /// Ensures that the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> contains a set 
        /// <typeparamref name="TValue"/>.
        /// </summary>
        /// <param name="value">The <typeparamref name="TValue"/> to check for.</param>
        /// <param name="dictionaryName">The <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> name to use
        /// if an exception is thrown.</param>
        /// <returns>The original <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.</returns>
        /// <exception cref="System.ArgumentException">If the <typeparamref name="TValue"/> is not present.
        /// </exception>
        public static MultiKeyDictionary<TPrimaryKey, TSubKey, TValue> MustContainValue<TPrimaryKey, TSubKey, TValue>
            (this MultiKeyDictionary<TPrimaryKey, TSubKey, TValue> d, TValue value, string dictionaryName)
        {
            if (d.ContainsValue(value))
                return
                    d;
            else
                throw
                    new System.ArgumentException(string.Format(
                        "Value {0} must be present in MultiKeyDictionary<{1},{2},{3}> {4}.",
                    value, typeof(TPrimaryKey), typeof(TSubKey), typeof(TValue), dictionaryName));
        }

        ///////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Ensures that the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> does not contain a set 
        /// <typeparamref name="TPrimaryKey"/>.
        /// </summary>
        /// <param name="primaryKey">The <typeparamref name="TPrimaryKey"/> to check against.</param>
        /// <param name="dictionaryName">The <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> name to use
        /// if an exception is thrown.</param>
        /// <returns>The original <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.</returns>
        /// <exception cref="System.ArgumentException">If the <typeparamref name="TPrimaryKey"/> is present.</exception>
        public static MultiKeyDictionary<TPrimaryKey, TSubKey, TValue> MustNotContainKey<TPrimaryKey, TSubKey, TValue>
            (this MultiKeyDictionary<TPrimaryKey, TSubKey, TValue> d, TPrimaryKey primaryKey, string dictionaryName)
        {
            if (!d.ContainsKey(primaryKey))
                return
                    d;
            else
                throw
                    new System.ArgumentException(string.Format(
                        "Primary Key {0} must not be present in MultiKeyDictionary<{1},{2},{3}> {4}.",
                    primaryKey, typeof(TPrimaryKey), typeof(TSubKey), typeof(TValue), dictionaryName));
        }

        /// <summary>
        /// Ensures that the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> does not contain a set 
        /// <typeparamref name="TSubKey"/>.
        /// </summary>
        /// <param name="subKey">The <typeparamref name="TSubKey"/> to check against.</param>
        /// <param name="dictionaryName">The <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> name to use
        /// if an exception is thrown.</param>
        /// <returns>The original <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.</returns>
        /// <exception cref="System.ArgumentException">If the <typeparamref name="TSubKey"/> is present.</exception>
        public static MultiKeyDictionary<TPrimaryKey, TSubKey, TValue> MustNotContainKey<TPrimaryKey, TSubKey, TValue>
            (this MultiKeyDictionary<TPrimaryKey, TSubKey, TValue> d, TSubKey subKey, string dictionaryName)
        {
            if (!d.ContainsKey(subKey))
                return
                    d;
            else
                throw
                    new System.ArgumentException(string.Format(
                        "Sub Key {0} must not be present in MultiKeyDictionary<{1},{2},{3}> {4}.",
                    subKey, typeof(TPrimaryKey), typeof(TSubKey), typeof(TValue), dictionaryName));
        }

        /// <summary>
        /// Ensures that the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> does not contain a set 
        /// <typeparamref name="TValue"/>.
        /// </summary>
        /// <param name="value">The <typeparamref name="TValue"/> to check against.</param>
        /// <param name="dictionaryName">The <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> name to use
        /// if an exception is thrown.</param>
        /// <returns>The original <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.</returns>
        /// <exception cref="System.ArgumentException">If the <typeparamref name="TValue"/> is present.</exception>
        public static MultiKeyDictionary<TPrimaryKey, TSubKey, TValue> MustNotContainValue<TPrimaryKey, TSubKey, TValue>
            (this MultiKeyDictionary<TPrimaryKey, TSubKey, TValue> d, TValue value, string dictionaryName)
        {
            if (!d.ContainsValue(value))
                return
                    d;
            else
                throw
                    new System.ArgumentException(string.Format(
                        "Value {0} must not be present in MultiKeyDictionary<{1},{2},{3}> {4}.",
                    value, typeof(TPrimaryKey), typeof(TSubKey), typeof(TValue), dictionaryName));
        }

        /// <summary>
        /// Adds the given <typeparamref name="TPrimaryKey"/> and <typeparamref name="TValue"/> to the given 
        /// <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> only if the
        /// <typeparamref name="TPrimaryKey"/> is NOT present.
        /// </summary>
        /// <param name="key">The given <typeparamref name="TPrimaryKey"/>.</param>
        /// <param name="value">The given <typeparamref name="TValue"/>.</param>
        /// <returns><see cref="true"/> if added successfully, <see cref="false"/> otherwise.</returns>
        public static bool TryAdd<TPrimaryKey, TSubKey, TValue>
            (this MultiKeyDictionary<TPrimaryKey, TSubKey, TValue> d, TPrimaryKey primaryKey, TValue value)
        {
            if (d.ContainsKey(primaryKey))
                return
                    false;
            d.Add(primaryKey, value);
            return
                true;
        }

        /// <summary>
        /// Adds the given <typeparamref name="TPrimaryKey"/>, <typeparamref name="TSubKey"/> and
        /// <typeparamref name="TValue"/> to the given <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>
        /// only if the <typeparamref name="TPrimaryKey"/> is NOT present.
        /// </summary>
        /// <param name="primaryKey">The given <typeparamref name="TPrimaryKey"/>.</param>
        /// <param name="subKey">The given <typeparamref name="TSubKey"/>.</param>
        /// <param name="value">The given <typeparamref name="TValue"/>.</param>
        /// <returns><see cref="true"/> if added successfully, <see cref="false"/> otherwise.</returns>
        public static bool TryAdd<TPrimaryKey, TSubKey, TValue>
            (this MultiKeyDictionary<TPrimaryKey, TSubKey, TValue> d, 
            TPrimaryKey primaryKey, TSubKey subKey, TValue value)
        {
            if (d.ContainsKey(primaryKey))
                return
                    false;
            d.Add(primaryKey, subKey, value);
            return
                true;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
