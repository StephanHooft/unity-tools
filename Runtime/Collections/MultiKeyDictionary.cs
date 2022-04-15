using System.Collections.Generic;
using System.Threading;

namespace StephanHooft.Collections
{
	/// <summary>
	/// A Dictionary that has both a primary <typeparamref name="TPrimaryKey"/> and a secondary <typeparamref name="TSubKey"/>
	/// for every value.
	/// <para>Based almost entirely on the excellent work of Aron Weiler (aronweiler@gmail.com).</para>
	/// </summary>	
	/// <typeparam name="TPrimaryKey">The type of the primary keys in the dictionary.</typeparam>
	/// <typeparam name="TSubKey">The type of subkeys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	public class MultiKeyDictionary<TPrimaryKey, TSubKey, TValue>
	{
		#region Properties

		/// <summary>
		/// Gets the number of key/value pairs contained in the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.
		/// </summary>
		public int Count
		{
			get
			{
				readerWriterLock.EnterReadLock();
				try
				{
					return
						baseDictionary.Count;
				}
				finally
				{
					readerWriterLock.ExitReadLock();
				}
			}
		}

		/// <summary>
		/// Gets a collection containing the <typeparamref name="TPrimaryKey"/>s in the 
		/// <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.
		/// </summary>
		public Dictionary<TPrimaryKey, TValue>.KeyCollection PrimaryKeys
		{
			get
			{
				readerWriterLock.EnterReadLock();
				try
				{
					return
						baseDictionary.Keys;
				}
				finally
				{
					readerWriterLock.ExitReadLock();
				}
			}
		}

		/// <summary>
		/// Gets a collection containing the <typeparamref name="TSubKey"/>s in the 
		/// <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.
		/// </summary>
		public Dictionary<TSubKey, TPrimaryKey>.KeyCollection SubKeys
		{
			get
			{
				readerWriterLock.EnterReadLock();
				try
				{
					return
						subDictionary.Keys;
				}
				finally
				{
					readerWriterLock.ExitReadLock();
				}
			}
		}

		/// <summary>
		/// Gets a collection containing the <typeparamref name="TValue"/>s in the 
		/// <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.
		/// </summary>
		public Dictionary<TPrimaryKey, TValue>.ValueCollection Values
		{
			get
			{
				readerWriterLock.EnterReadLock();
				try
				{
					return
						baseDictionary.Values;
				}
				finally
				{
					readerWriterLock.ExitReadLock();
				}
			}
		}

		/// <summary>
		/// Gets/sets the <typeparamref name="TValue"/> associated with the specified <typeparamref name="TSubKey"/>.
		/// </summary>
		/// <param name="subKey">The key of the <typeparamref name="TValue"/> to get.</param>
		public TValue this[TSubKey subKey]
		{
			get
			{
				if (TryGetValue(subKey, out TValue item))
					return item;
				throw
					new KeyNotFoundException(string.Format("Sub key {0} not found.", subKey.ToString()));
			}
			set
			{
				readerWriterLock.EnterWriteLock();
				try
				{
					baseDictionary[subDictionary[subKey]] = value;
				}
				finally
				{
					readerWriterLock.ExitWriteLock();
				}
			}
		}

		/// <summary>
		/// Gets/sets the <typeparamref name="TValue"/> associated with the specified <typeparamref name="TPrimaryKey"/>.
		/// </summary>
		/// <param name="primaryKey">The key of the <typeparamref name="TValue"/> to get.</param>
		public TValue this[TPrimaryKey primaryKey]
		{
			get
			{
				if (TryGetValue(primaryKey, out TValue item))
					return
						item;
				throw
					new KeyNotFoundException(string.Format("Primary key {0} not found.", primaryKey.ToString()));
			}
			set
			{
				readerWriterLock.EnterWriteLock();
				try
				{
					baseDictionary[primaryKey] = value;
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

		private readonly Dictionary<TPrimaryKey, TValue> baseDictionary = new();
		private readonly Dictionary<TSubKey, TPrimaryKey> subDictionary = new();
		private readonly Dictionary<TPrimaryKey, TSubKey> primaryToSubkeyMapping = new();
		private readonly ReaderWriterLockSlim readerWriterLock = new();

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
		#region Constructors and Finaliser

		/// <summary>
		/// Initializes a new instance of the <see cref="MultiKeyDictionary{K, L, V}"/> class.
		/// </summary>
		public MultiKeyDictionary() { }

        ~MultiKeyDictionary() { }

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
		#region Methods

		/// <summary>
		/// Adds the specified <typeparamref name="TPrimaryKey"/>, <typeparamref name="TSubKey"/>, and <typeparamref name="TValue"/>
		/// to the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.
		/// </summary>
		/// <param name="primaryKey">The <typeparamref name="TPrimaryKey"/> of the <typeparamref name="TValue"/> to add.</param>
		/// <param name="subKey">The <typeparamref name="TSubKey"/> of the <typeparamref name="TValue"/> to add.</param>
		/// <param name="val">The value of the <typeparamref name="TValue"/> to add. The <typeparamref name="TValue"/> can be null
		/// for reference types.</param>
		public void Add(TPrimaryKey primaryKey, TSubKey subKey, TValue val)
		{
			Add(primaryKey, val);
			Associate(subKey, primaryKey);
		}

		/// <summary>
		/// Adds the specified <typeparamref name="TPrimaryKey"/> and <typeparamref name="TValue"/> to the
		/// <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.
		/// </summary>
		/// <param name="primaryKey">The <typeparamref name="TPrimaryKey"/> of the <typeparamref name="TValue"/> to add.</param>
		/// <param name="val">The value of the <typeparamref name="TValue"/> to add. The <typeparamref name="TValue"/> can be null
		/// for reference types.</param>
		public void Add(TPrimaryKey primaryKey, TValue val)
		{
			readerWriterLock.EnterWriteLock();
			try
			{
				baseDictionary.Add(primaryKey, val);
			}
			finally
			{
				readerWriterLock.ExitWriteLock();
			}
		}

		/// <summary>
		/// Associates a <typeparamref name="TSubKey"/> with an <typeparamref name="TValue"/> already added under a specific
		/// <typeparamref name="TPrimaryKey"/>.
		/// </summary>
		/// <param name="subKey">The <typeparamref name="TSubKey"/> to associate with the <typeparamref name="TPrimaryKey"/>.</param>
		/// <param name="primaryKey">The <typeparamref name="TPrimaryKey"/> to associate with.</param>
		public void Associate(TSubKey subKey, TPrimaryKey primaryKey)
		{
			readerWriterLock.EnterUpgradeableReadLock();
			try
			{
				if (!baseDictionary.ContainsKey(primaryKey))
					throw
						new KeyNotFoundException(string.Format("The base dictionary does not contain the key '{0}'", primaryKey));
				if (primaryToSubkeyMapping.ContainsKey(primaryKey)) // Remove the old mapping first
				{
					readerWriterLock.EnterWriteLock();
					try
					{
						if (subDictionary.ContainsKey(primaryToSubkeyMapping[primaryKey]))
							subDictionary.Remove(primaryToSubkeyMapping[primaryKey]);
						primaryToSubkeyMapping.Remove(primaryKey);
					}
					finally
					{
						readerWriterLock.ExitWriteLock();
					}
				}
				subDictionary[subKey] = primaryKey;
				primaryToSubkeyMapping[primaryKey] = subKey;
			}
			finally
			{
				readerWriterLock.ExitUpgradeableReadLock();
			}
		}

		/// <summary>
		/// Removes all <typeparamref name="TPrimaryKey"/>s, <typeparamref name="TSubKey"/>s and <typeparamref name="TValue"/> from the 
		/// <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.
		/// </summary>
		public void Clear()
		{
			readerWriterLock.EnterWriteLock();
			try
			{
				baseDictionary.Clear();
				subDictionary.Clear();
				primaryToSubkeyMapping.Clear();
			}
			finally
			{
				readerWriterLock.ExitWriteLock();
			}
		}

		/// <summary>
		/// Clones the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>'s <typeparamref name="TValue"/>s to a
		/// <typeparamref name="TValue"/>[].
		/// </summary>
		/// <returns>A <typeparamref name="TValue"/>[].</returns>
		public TValue[] CloneValues()
		{
			readerWriterLock.EnterReadLock();
			try
			{
				TValue[] values = new TValue[baseDictionary.Values.Count];
				baseDictionary.Values.CopyTo(values, 0);
				return
					values;
			}
			finally
			{
				readerWriterLock.ExitReadLock();
			}
		}

		/// <summary>
		/// Determines whether the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> contains the specified 
		/// <typeparamref name="TSubKey"/>.
		/// </summary>
		/// <param name="subKey">The <typeparamref name="TSubKey"/> to locate in the
		/// <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.</param>
		/// <returns><see cref="true"/> if the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> contains
		/// an element with the specified <typeparamref name="TSubKey"/>; otherwise, <see cref="false"/>.</returns>
		public bool ContainsKey(TSubKey subKey)
		{
			return
				TryGetValue(subKey, out _);
		}

		/// <summary>
		/// Determines whether the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> contains the specified
		/// <typeparamref name="TPrimaryKey"/>.
		/// </summary>
		/// <param name="primaryKey">The <typeparamref name="TPrimaryKey"/> to locate in the
		/// <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.</param>
		/// <returns><see cref="true"/> if the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> contains
		/// an element with the specified <typeparamref name="TPrimaryKey"/>; otherwise, <see cref="false"/>.</returns>
		public bool ContainsKey(TPrimaryKey primaryKey)
		{
			return
				TryGetValue(primaryKey, out _);
		}

		/// <summary>
		/// Determines whether the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> contains the specified
		/// <typeparamref name="TValue"/>.
		/// </summary>
		/// <param name="value">The <typeparamref name="TValue"/> to locate in the
		/// <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.</param>
		/// <returns><see cref="true"/> if the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> contains
		/// an element with the specified <typeparamref name="TValue"/>; otherwise, <see cref="false"/>.</returns>
		public bool ContainsValue(TValue value)
		{
			readerWriterLock.EnterReadLock();
			try
			{
				return baseDictionary.ContainsValue(value);
			}
			finally
			{
				readerWriterLock.ExitReadLock();
			}
		}

		/// <summary>
		/// Returns an enumerator that iterates through the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.
		/// </summary>
		/// <returns>An <see cref="IEnumerator{KeyValuePair{TPrimaryKey, TValue}}"/> structure for the
		/// <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.</returns>
		public IEnumerator<KeyValuePair<TPrimaryKey, TValue>> GetEnumerator()
		{
			readerWriterLock.EnterReadLock();
			try
			{
				return
					baseDictionary.GetEnumerator();
			}
			finally
			{
				readerWriterLock.ExitReadLock();
			}
		}

		/// <summary>
		/// Removes the <typeparamref name="TValue"/> with the specified <typeparamref name="TPrimaryKey"/> from the
		/// <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.
		/// </summary>
		/// <param name="primaryKey">The <typeparamref name="TPrimaryKey"/> of the <typeparamref name="TValue"/> to remove.</param>
		public void Remove(TPrimaryKey primaryKey)
		{
			readerWriterLock.EnterWriteLock();
			try
			{
				if (primaryToSubkeyMapping.ContainsKey(primaryKey))
				{
					if (subDictionary.ContainsKey(primaryToSubkeyMapping[primaryKey]))
						subDictionary.Remove(primaryToSubkeyMapping[primaryKey]);
					primaryToSubkeyMapping.Remove(primaryKey);
				}
				baseDictionary.Remove(primaryKey);
			}
			finally
			{
				readerWriterLock.ExitWriteLock();
			}
		}

		/// <summary>
		/// Removes the <typeparamref name="TValue"/> with the specified <typeparamref name="TSubKey"/> from the
		/// <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/>.
		/// </summary>
		/// <param name="subKey">The <typeparamref name="TSubKey"/> of the <typeparamref name="TValue"/> to remove.</param>
		public void Remove(TSubKey subKey)
		{
			readerWriterLock.EnterWriteLock();
			try
			{
				baseDictionary.Remove(subDictionary[subKey]);
				primaryToSubkeyMapping.Remove(subDictionary[subKey]);
				subDictionary.Remove(subKey);
			}
			finally
			{
				readerWriterLock.ExitWriteLock();
			}
		}

		/// <summary>
		/// Gets the <typeparamref name="TValue"/> associated with the specified <typeparamref name="TSubKey"/>.
		/// </summary>
		/// <param name="subKey">The <typeparamref name="TSubKey"/> of the value to get.</param>
		/// <param name="val">When this method returns, contains the <typeparamref name="TValue"/> associated with
		/// the specified <typeparamref name="TSubKey"/>, if the <typeparamref name="TSubKey"/> is found; otherwise, 
		/// the default value for the <typeparamref name="TValue"/>. This parameter is passed uninitialized.</param>
		/// <returns><see cref="true"/> if the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> contains
		/// an element with the specified <typeparamref name="TSubKey"/>; otherwise, <see cref="false"/>.</returns>
		public bool TryGetValue(TSubKey subKey, out TValue val)
		{
			val = default;
			readerWriterLock.EnterReadLock();
			try
			{
				if (subDictionary.TryGetValue(subKey, out TPrimaryKey primaryKey))
					return
						baseDictionary.TryGetValue(primaryKey, out val);
			}
			finally
			{
				readerWriterLock.ExitReadLock();
			}
			return false;
		}

		/// <summary>
		/// Gets the <typeparamref name="TValue"/> associated with the specified <typeparamref name="TPrimaryKey"/>.
		/// </summary>
		/// <param name="primaryKey">The <typeparamref name="TPrimaryKey"/> of the value to get.</param>
		/// <param name="val">When this method returns, contains the <typeparamref name="TValue"/> associated with the
		/// specified <typeparamref name="TPrimaryKey"/>, if the <typeparamref name="TPrimaryKey"/> is found; otherwise, 
		/// the default value for the <typeparamref name="TValue"/>. This parameter is passed uninitialized.</param>
		/// <returns><see cref="true"/> if the <see cref="MultiKeyDictionary{TPrimaryKey, TSubKey, TValue}"/> contains
		/// an element with the specified <typeparamref name="TPrimaryKey"/>; otherwise, <see cref="false"/>.</returns>
		public bool TryGetValue(TPrimaryKey primaryKey, out TValue val)
		{
			readerWriterLock.EnterReadLock();
			try
			{
				return
					baseDictionary.TryGetValue(primaryKey, out val);
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
