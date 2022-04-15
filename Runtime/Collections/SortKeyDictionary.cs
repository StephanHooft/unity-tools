using StephanHooft.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace StephanHooft.Collections
{
    /// <summary>
    /// A <typeparamref name="TKey"/>-keyed Dictionary that sorts its <typeparamref name="TValue"/>s based on
    /// <typeparamref name="TSortKey"/> sorting keys.
    /// <para>Assigned <typeparamref name="TKey"/> and <typeparamref name="TSortKey"/> keys must be unique.</para>
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TSortKey">The type of the secondary (sorting) keys in the dictionary. Must inherit from
    /// <see cref="System.IComparable"/>.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    public class SortKeyDictionary<TKey, TSortKey, TValue> : IEnumerable<TValue> where TSortKey : System.IComparable
    {
        #region Properties

        /// <summary>
        /// Gets the number of values contained in the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.
        /// </summary>
        public int Count
        {
            get
            {
                readerWriterLock.EnterReadLock();
                try
                {
                    return
                        sortedList.Count;
                }
                finally
                {
                    readerWriterLock.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// Gets a collection containing the <typeparamref name="TKey"/>s in the
		/// <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.
        /// </summary>
        public Dictionary<TKey, TValue>.KeyCollection Keys
        {
            get
            {
                readerWriterLock.EnterReadLock();
                try
                {
                    return
                        dictionary.Keys;
                }
                finally
                {
                    readerWriterLock.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// Gets a collection containing the <typeparamref name="TSortKey"/>s in the
		/// <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.
        /// </summary>
        public IList<TSortKey> SortKeys
        {
            get
            {
                readerWriterLock.EnterReadLock();
                try
                {
                    return
                        sortedList.Keys;
                }
                finally
                {
                    readerWriterLock.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// Gets a collection containing the <typeparamref name="TValue"/>s in the
		/// <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.
        /// </summary>
        public IList<TValue> Values
        {
            get
            {
                readerWriterLock.EnterReadLock();
                try
                {
                    return
                        sortedList.Values;
                }
                finally
                {
                    readerWriterLock.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// Gets/sets the <typeparamref name="TValue"/> associated with the specified <typeparamref name="TKey"/>.
        /// </summary>
        /// <param name="key">The key of the <typeparamref name="TValue"/> to get.</param>
        /// <returns>A <typeparamref name="TValue"/>.</returns>
        public TValue this[TKey key]
        {
            get
            {
                key.MustNotBeNull("key");
                if (TryGetValue(key, out TValue item))
                    return
                        item;
                throw
                    new KeyNotFoundException(string.Format("{0} {1} not found.", typeof(TKey), key));
            }
            set
            {
                key.MustNotBeNull("key");
                value.MustNotBeNull("value");
                readerWriterLock.EnterWriteLock();
                try
                {
                    dictionary[key] = value;
                }
                finally
                {
                    readerWriterLock.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Gets/sets the <typeparamref name="TValue"/> associated with the specified <typeparamref name="TSortKey"/>.
        /// </summary>
        /// <param name="sortKey">The sorting key of the <typeparamref name="TValue"/> to get.</param>
        /// <returns>A <typeparamref name="TValue"/>.</returns>
        public TValue this[TSortKey sortKey]
        {
            get
            {
                sortKey.MustNotBeNull("sortKey");
                if (TryGetValue(sortKey, out TValue item))
                    return
                        item;
                throw
                    new KeyNotFoundException(string.Format("{0} {1} not found.", typeof(TSortKey), sortKey));
            }
            set
            {
                sortKey.MustNotBeNull("sortKey");
                value.MustNotBeNull("value");
                readerWriterLock.EnterWriteLock();
                try
                {
                    sortedList[sortKey] = value;
                }
                finally
                {
                    readerWriterLock.ExitWriteLock();
                }
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        private readonly Dictionary<TKey, TValue> dictionary = new();
        private readonly SortedList<TSortKey, TValue> sortedList = new();
        private readonly ReaderWriterLockSlim readerWriterLock = new();

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.
        /// </summary>
        public SortKeyDictionary() { }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region IEnumerable Implementation

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>'s
        /// <typeparamref name="TValue"/>s.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{TValue}"/> structure for the
        /// <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.</returns>
        public IEnumerator<TValue> GetEnumerator()
        {
            readerWriterLock.EnterReadLock();
            try
            {
                return
                    sortedList.Values.GetEnumerator();
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            readerWriterLock.EnterReadLock();
            try
            {
                return
                    ((IEnumerable)sortedList.Values).GetEnumerator();
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// Adds a <typeparamref name="TKey"/>, <typeparamref name="TSortKey"/>, and <typeparamref name="TValue"/>
        /// to the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> of the <typeparamref name="TValue"/> to add.</param>
        /// <param name="sortKey">The <typeparamref name="TSortKey"/> of the <typeparamref name="TValue"/> to add.
        /// </param>
        /// <param name="value">The value of the <typeparamref name="TValue"/> to add.</param>
        public void Add(TKey key, TSortKey sortKey, TValue value)
        {
            key.MustNotBeNull("key");
            sortKey.MustNotBeNull("sort");
            readerWriterLock.EnterWriteLock();
            try
            {
                dictionary.MustNotContainKey(key, "dictionary");
                sortedList.MustNotContainKey(sortKey, "sortedList");
                dictionary.Add(key, value);
                sortedList.Add(sortKey, value);
            }
            finally
            {
                readerWriterLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Removes all <typeparamref name="TKey"/>s, <typeparamref name="TSortKey"/>s and
        /// <typeparamref name="TValue"/>s from the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.
        /// </summary>
        public void Clear()
        {
            readerWriterLock.EnterWriteLock();
            try
            {
                dictionary.Clear();
                sortedList.Clear();
            }
            finally
            {
                readerWriterLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Clones the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>'s <typeparamref name="TValue"/>s to a
		/// <typeparamref name="TValue"/>[].
        /// </summary>
        /// <returns>A <typeparamref name="TValue"/>[].</returns>
        public TValue[] CloneValues()
        {
            readerWriterLock.EnterReadLock();
            try
            {
                TValue[] values = new TValue[sortedList.Values.Count];
                sortedList.Values.CopyTo(values, 0);
                return
                    values;
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Determines whether the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> contains the specified 
		/// <typeparamref name="TKey"/>.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> to locate in the
        /// <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.</param>
        /// <returns><see cref="true"/> if the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> contains
		/// an element with the specified <typeparamref name="TKey"/>; otherwise, <see cref="false"/>.</returns>
        public bool ContainsKey(TKey key)
        {
            return
                TryGetValue(key, out _);
        }

        /// <summary>
        /// Determines whether the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> contains the specified 
		/// <typeparamref name="TSortKey"/>.
        /// </summary>
        /// <param name="sortKey">The <typeparamref name="TSortKey"/> to locate in the
        /// <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.</param>
        /// <returns><see cref="true"/> if the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> contains
		/// an element with the specified <typeparamref name="TSortKey"/>; otherwise, <see cref="false"/>.</returns>
        public bool ContainsSortKey(TSortKey sortKey)
        {
            return
                TryGetValue(sortKey, out _);
        }

        /// <summary>
        /// Determines whether the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> contains the specified 
		/// <typeparamref name="TValue"/>.
        /// </summary>
        /// <param name="value">The <typeparamref name="TValue"/> to locate in the
        /// <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.</param>
        /// <returns><see cref="true"/> if the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> contains
		/// an element with the specified <typeparamref name="TValue"/>; otherwise, <see cref="false"/>.</returns>
        public bool ContainsValue(TValue value)
        {
            value.MustNotBeNull("value");
            readerWriterLock.EnterReadLock();
            try
            {
                return
                    sortedList.ContainsValue(value);
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Gets a <typeparamref name="TValue"/> by its index value.
        /// </summary>
        /// <param name="index">The index value of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <returns>A <typeparamref name="TValue"/>.</returns>
        public TValue GetValueByIndex(int index)
        {
            readerWriterLock.EnterReadLock();
            try
            {
                var lastIndex = sortedList.LastIndex();
                index.MustBeWithinRange(0, lastIndex, "index");
                return
                    sortedList.Values[index];
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Retrieves the index value that matches a certain <typeparamref name="TKey"/>.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> to retrieve an index value for.</param>
        /// <returns>A <see cref="int"/> index value.</returns>
        public int IndexOf(TKey key)
        {
            key.MustNotBeNull("key");
            readerWriterLock.EnterReadLock();
            try
            {
                dictionary.MustContainKey(key, "dictionary");
                return
                    sortedList.IndexOfValue(dictionary[key]);
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Retrieves the index value that matches a certain <typeparamref name="TSortKey"/>.
        /// </summary>
        /// <param name="sortKey">The <typeparamref name="TSortKey"/> to retrieve an index value for.</param>
        /// <returns>A <see cref="int"/> index value.</returns>
        public int IndexOf(TSortKey sortKey)
        {
            sortKey.MustNotBeNull("sortKey");
            readerWriterLock.EnterReadLock();
            try
            {
                sortedList.MustContainKey(sortKey, "sortedList");
                return
                    sortedList.IndexOfKey(sortKey);
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Retrieves the index value that matches a certain <typeparamref name="TValue"/>.
        /// </summary>
        /// <param name="value">The <typeparamref name="TValue"/> to retrieve an index value for.</param>
        /// <returns>A <see cref="int"/> index value.</returns>
        public int IndexOf(TValue value)
        {
            readerWriterLock.EnterReadLock();
            try
            {
                sortedList.MustContainValue(value, "sortedList");
                return
                    sortedList.IndexOfValue(value);
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Removes the <typeparamref name="TValue"/> and <typeparamref name="TSortKey"/> with the specified
        /// <typeparamref name="TKey"/> from the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the <typeparamref name="TValue"/> to remove.</param>
        public void Remove(TKey key)
        {
            key.MustNotBeNull("key");
            readerWriterLock.EnterWriteLock();
            try
            {
                dictionary.MustContainKey(key, "dictionary");
                var value = dictionary[key];
                var index = sortedList.IndexOfValue(value);
                sortedList.RemoveAt(index);
                dictionary.Remove(key);
            }
            finally
            {
                readerWriterLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Retrieves the <typeparamref name="TSortKey"/> matching a certain <typeparamref name="TKey"/> in the
        /// <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key to retrieve a <typeparamref name="TSortKey"/> for.</param>
        /// <returns>A <typeparamref name="TSortKey"/>.</returns>
        public TSortKey SortKeyOf(TKey key)
        {
            key.MustNotBeNull("key");
            readerWriterLock.EnterReadLock();
            try
            {
                dictionary.MustContainKey(key, "dictionary");
                var value = dictionary[key];
                var sortKey = default(TSortKey);
                foreach (var kvp in sortedList)
                    if (kvp.Value.Equals(value))
                    {
                        sortKey = kvp.Key;
                        break;
                    }
                return
                    sortKey;
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Retrieves the <typeparamref name="TSortKey"/> matching a certain <typeparamref name="TValue"/> in the
        /// <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/>.
        /// </summary>
        /// <param name="value">The value to retrieve a <typeparamref name="TSortKey"/> for.</param>
        /// <returns>A <typeparamref name="TSortKey"/>.</returns>
        public TSortKey SortKeyOf(TValue value)
        {
            readerWriterLock.EnterReadLock();
            try
            {
                sortedList.MustContainValue(value, "sortedList");
                var sortKey = default(TSortKey);
                foreach (var kvp in sortedList)
                    if (kvp.Value.Equals(value))
                    {
                        sortKey = kvp.Key;
                        break;
                    }
                return
                    sortKey;
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Gets the <typeparamref name="TValue"/> associated with the specified <typeparamref name="TKey"/>.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> of the <typeparamref name="TValue"/> to get.</param>
        /// <param name="value">When this method returns, contains the <typeparamref name="TValue"/> associated with
		/// the specified <typeparamref name="TKey"/>, if the <typeparamref name="TKey"/> is found; otherwise, 
		/// the default value for the <typeparamref name="TValue"/>.</param>
        /// <returns><see cref="true"/> if the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> contains
		/// an element with the specified <typeparamref name="TKey"/>; otherwise, <see cref="false"/>.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            key.MustNotBeNull("key");
            readerWriterLock.EnterReadLock();
            try
            {
                return
                    dictionary.TryGetValue(key, out value);
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Gets the <typeparamref name="TValue"/> associated with the specified <typeparamref name="TSortKey"/>.
        /// </summary>
        /// <param name="sortKey">The <typeparamref name="TSortKey"/> of the <typeparamref name="TValue"/> to get.
        /// </param>
        /// <param name="value">When this method returns, contains the <typeparamref name="TValue"/> associated with
		/// the specified <typeparamref name="TSortKey"/>, if the <typeparamref name="TSortKey"/> is found; otherwise,
		/// the default value for the <typeparamref name="TValue"/>.</param>
        /// <returns><see cref="true"/> if the <see cref="SortKeyDictionary{TKey, TSortKey, TValue}"/> contains
		/// an element with the specified <typeparamref name="TSortKey"/>; otherwise, <see cref="false"/>.</returns>
        public bool TryGetValue(TSortKey sortKey, out TValue value)
        {
            sortKey.MustNotBeNull("sortKey");
            readerWriterLock.EnterReadLock();
            try
            {
                return
                    sortedList.TryGetValue(sortKey, out value);
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
