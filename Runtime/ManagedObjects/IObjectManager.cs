using System;
using System.Collections;
using System.Collections.Generic;

namespace StephanHooft.ManagedObjects
{
    /// <summary>
    /// Interface for classes that govern the creation and management of <see cref="IManagedObject"/>s.
    /// </summary>
    public interface IObjectManager : IEnumerable
    { }

    /// <summary>
    /// Interface for classes that govern the creation and management of <typeparamref name="TManagedObject"/>s.
    /// </summary>
    public interface IObjectManager<TManagedObject> : IObjectManager, IEnumerable<TManagedObject> where TManagedObject : IManagedObject
    {
        #region Interface Events

        /// <summary>
        /// Invoked when a <typeparamref name="TManagedObject"/> is added.
        /// </summary>
        public event Action<TManagedObject> OnAdded;

        /// <summary>
        /// Invoked just before a <typeparamref name="TManagedObject"/> is removed.
        /// </summary>
        public event Action<TManagedObject> OnImpendingRemoval;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Interface Properties

        /// <summary>
        /// The number of <typeparamref name="TManagedObject"/>s managed by the
        /// <see cref="IObjectManager{TManagedObject}"/>.
        /// </summary>
        public int Count { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Interface Methods

        /// <summary>
        /// Adds a <typeparamref name="TManagedObject"/>.
        /// </summary>
        /// <returns>The new <typeparamref name="TManagedObject"/>.</returns>
        public TManagedObject Add();

        /// <summary>
        /// Counts the number of <typeparamref name="TManagedObject"/>s managed by the
        /// <see cref="IObjectManager{TManagedObject}"/> that match a specific <see cref="Predicate{IManagedObject}"/>.
        /// </summary>
        /// <param name="predicate">A <see cref="Predicate{IManagedObject}"/> to check the
        /// <see cref="IObjectManager{TManagedObject}"/>'s <typeparamref name="TManagedObject"/>s against.</param>
        /// <returns>The number of <typeparamref name="TManagedObject"/>s that match the
        /// <see cref="Predicate{IManagedObject}"/>.</returns>
        public int CountIf(Predicate<TManagedObject> predicate);

        /// <summary>
        /// Finds a <typeparamref name="TManagedObject"/> managed by the <see cref="IObjectManager{IManagedObject}"/> that
        /// matches a certain <see cref="Predicate{IManagedObject}"/>. Returns <see cref="null"/> otherwise.
        /// </summary>
        /// <param name="predicate">A <see cref="Predicate{IManagedObject}"/> to check the 
        /// <see cref="IObjectManager{IManagedObject}"/>'s <typeparamref name="TManagedObject"/>s against.</param>
        /// <returns>A <typeparamref name="TManagedObject"/> that matches the specificied
        /// <see cref="Predicate{IManagedObject}"/>, or <see cref="null"/> if no such <typeparamref name="TManagedObject"/>
        /// can be found.</returns>
        public TManagedObject Find(Predicate<TManagedObject> predicate);

        /// <summary>
        /// Finds all <typeparamref name="TManagedObject"/>s managed by the <see cref="IObjectManager{TManagedObject}"/> that match
        /// a certain <see cref="Predicate{IManagedObject}"/>.
        /// </summary>
        /// <param name="predicate">A <see cref="Predicate{IManagedObject}"/> to check the
        /// <see cref="IObjectManager{TManagedObject}"/>'s <typeparamref name="TManagedObject"/>s against.</param>
        /// <returns>A <see cref="List{IManagedObject}"/> of <typeparamref name="TManagedObject"/>s that match the 
        /// <see cref="Predicate{IManagedObject}"/>.</returns>
        public List<TManagedObject> FindAll(Predicate<TManagedObject> predicate);

        /// <summary>
        /// Removes a <typeparamref name="TManagedObject"/> managed by the <see cref="IObjectManager{TManagedObject}"/>.
        /// </summary>
        /// <param name="managedObject">The <typeparamref name="TManagedObject"/> to remove.</param>
        public void Remove(TManagedObject managedObject);

        /// <summary>
        /// Removes all <typeparamref name="TManagedObject"/>s managed by the <see cref="IObjectManager{TManagedObject}"/>.
        /// </summary>
        public void RemoveAll();

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
