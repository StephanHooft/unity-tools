using StephanHooft.EditorSafe;
using System.Collections.Generic;
using UnityEngine;

namespace StephanHooft.ComponentPool
{
    /// <summary>
    /// A generic pooler class for objects with a <see cref="Component"/>-inheriting class instance attached to it.
    /// Can be used as an alternative to repeatedly creating/destroying <see cref="GameObject"/>s with a certain 
    /// <see cref="Component"/>.
    /// <para><see cref="GameObject"/>s attached to <see cref="Component"/>s provided by the
    /// <see cref="ComponentPool{T}"/> are deactivated when returned to it, and activated upon being retrieved
    /// again.</para>
    /// <para>The class also offers method overloads to change a <see cref="GameObject"/>'s name or
    /// <see cref="Transform"/> parent when their associated <see cref="Component"/>s are retrieved from (or returned
    /// to) the <see cref="ComponentPool{T}"/>.</para>
    /// </summary>
    public sealed class ComponentPool<T> where T : Component
    {
        #region Properties

        /// <summary>
        /// The maximum amount of <typeparamref name="T"/>s the <see cref="ComponentPool{T}"/> will store before
        /// destroying excess <typeparamref name="T"/>s.
        /// </summary>
        public int Limit { get; private set; }

        /// <summary>
        /// Returns the amount of <typeparamref name="T"/>s in the <see cref="ComponentPool{T}"/>.
        /// </summary>
        public int Count => componentQueue.Count;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        private readonly List<T> ownedComponents = new List<T>();
        private readonly Queue<T> componentQueue = new Queue<T>();

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructors and Finaliser

        /// <summary>
        /// Create a new <see cref="ComponentPool{T}"/>.
        /// </summary>
        /// <param name="limit">The amount of <typeparamref name="T"/>s that the <see cref="ComponentPool{T}"/> 
        /// should store before destroying excess <typeparamref name="T"/>s.</param>
        public ComponentPool(int limit = 10)
        {
            if (limit <= 0)
                throw new System.ArgumentOutOfRangeException("limit");
            Limit = limit;
        }

        /// <summary>
        /// Finaliser.
        /// </summary>
        ~ComponentPool()
        { }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Static Methods

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// Create a new <see cref="GameObject"/> with a <typeparamref name="T"/> attached to it, and add it to the
        /// <see cref="ComponentPool{T}"/>.
        /// </summary>
        public void Add()
        {
            if (componentQueue.Count < Limit)
            {
                var newGameObject = new GameObject();
                newGameObject.SetActive(false);
                var newComponent = (T)newGameObject.AddComponent(typeof(T));
                ownedComponents.Add(newComponent);
                componentQueue.Enqueue(newComponent);
            }
            else
                Debug.LogWarning("Cannot add Components in excess of the ComponentPool's Limit.");
        }

        /// <summary>
        /// Add a manually-created <see cref="GameObject"/> with a <typeparamref name="T"/> to the
        /// <see cref="ComponentPool{T}"/>.
        /// <para>This method should be used if you don't want the <see cref="ComponentPool{T}"/> to automatically
        /// create a <see cref="GameObject"/>
        /// with a matching <typeparamref name="T"/>, but want to configure the <see cref="GameObject"/> yourself
        /// before adding them to the 
        /// <see cref="ComponentPool{T}"/>.</para>
        /// </summary>
        /// <param name="component">The <typeparamref name="T"/> to add to the <see cref="ComponentPool{T}"/>.</param>
        public void Add(T component)
        {
            if (component == null)
                throw
                    new System.ArgumentNullException("component");
            if (ownedComponents.Contains(component))
                throw
                    new System.InvalidOperationException(
                        string.Format("Component of {0} already added to ComponentPool.", component.name));
            component.gameObject.SetActive(false);
            ownedComponents.Add(component);
            componentQueue.Enqueue(component);
        }

        /// <summary>
        /// Get a <typeparamref name="T"/> from the <see cref="ComponentPool{T}"/> and activate it.
        /// </summary>
        /// <returns>A <typeparamref name="T"/> attached to an active <see cref="GameObject"/>.</returns>
        public T Get()
        {
            if (componentQueue.Count == 0)
                Add();
            var component = componentQueue.Dequeue();
            component.gameObject.SetActive(true);
            return
                component;
        }

        /// <summary>
        /// Get a <typeparamref name="T"/> from the <see cref="ComponentPool{T}"/> and set its <see cref="Transform"/>
        /// <paramref name="parent"/>.
        /// </summary>
        /// <param name="parent">The <see cref="Transform"/> to parent the <typeparamref name="T"/>'s
        /// <see cref="GameObject"/> to.</param>
        /// <returns>A <typeparamref name="T"/> attached to an active <see cref="GameObject"/>.</returns>
        public T Get(Transform parent)
        {
            if (parent == null)
                throw
                    new System.ArgumentNullException("parent");
            if (componentQueue.Count == 0)
                Add();
            var component = componentQueue.Dequeue();
            component.transform.SetParent(parent);
            component.transform.localPosition = Vector3.zero;
            component.transform.localRotation = Quaternion.identity;
            component.gameObject.SetActive(true);
            return
                component;
        }

        /// <summary>
        /// Get a <typeparamref name="T"/> from the <see cref="ComponentPool{T}"/> and set its <see cref="GameObject"/>
        /// 's <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The <see cref="string"/> name to assign to the <typeparamref name="T"/>'s
        /// <see cref="GameObject"/>.</param>
        /// <returns>A <typeparamref name="T"/> attached to an active <see cref="GameObject"/>.</returns>
        public T Get(string name)
        {
            if (name == null || name == "")
                throw
                    new System.ArgumentNullException("name");
            if (componentQueue.Count == 0)
                Add();
            var component = componentQueue.Dequeue();
            component.name = name;
            component.gameObject.SetActive(true);
            return
                component;
        }

        /// <summary>
        /// Get a <typeparamref name="T"/> from the <see cref="ComponentPool{T}"/>, set its <see cref="Transform"/>
        /// <paramref name="parent"/>, and set its <see cref="GameObject"/>'s <paramref name="name"/>.
        /// </summary>
        /// <param name="parent">The <see cref="Transform"/> to parent the <typeparamref name="T"/>'s
        /// <see cref="GameObject"/> to.</param>
        /// <param name="name">The <see cref="string"/> name to assign to the <typeparamref name="T"/>'s
        /// <see cref="GameObject"/>.</param>
        /// <returns>A <typeparamref name="T"/> attached to an active <see cref="GameObject"/>.</returns>
        public T Get(Transform parent, string name)
        {
            if (parent == null)
                throw
                    new System.ArgumentNullException("parent");
            if (name == null || name == "")
                throw
                    new System.ArgumentNullException("name");
            if (componentQueue.Count == 0)
                Add();
            var component = componentQueue.Dequeue();
            component.transform.SetParent(parent);
            component.transform.localPosition = Vector3.zero;
            component.transform.localRotation = Quaternion.identity;
            component.name = name;
            component.gameObject.SetActive(true);
            return
                component;
        }

        /// <summary>
        /// Deactivate a <typeparamref name="T"/>'s associated <see cref="GameObject"/> and send the
        /// <typeparamref name="T"/> back to the <see cref="ComponentPool{T}"/>.
        /// </summary>
        /// <param name="component">The <typeparamref name="T"/> to return to the <see cref="ComponentPool{T}"/>.
        /// </param>
        public void Return(T component)
        {
            if (component == null)
                throw
                    new System.ArgumentNullException("component");
            if (!FromThisPool(component))
                throw
                    new System.InvalidOperationException(NotFromThisPool(component));
            if (componentQueue.Count >= Limit)
                DestroyComponentObject(component);
            else
            {
                component.gameObject.SetActive(false);
                componentQueue.Enqueue(component);
            }
        }

        /// <summary>
        /// Deactivate a <typeparamref name="T"/>'s associated <see cref="GameObject"/>, set its
        /// <see cref="Transform"/> <paramref name="parent"/>, and send the <typeparamref name="T"/> back to the
        /// <see cref="ComponentPool{T}"/>.
        /// </summary>
        /// <param name="component">The <typeparamref name="T"/> to return to the <see cref="ComponentPool{T}"/>.
        /// </param>
        /// <param name="parent">The <see cref="Transform"/> to parent the <typeparamref name="T"/>'s
        /// <see cref="GameObject"/> to.</param>
        public void Return(T component, Transform parent)
        {
            if (component == null)
                throw
                    new System.ArgumentNullException("component");
            if (!FromThisPool(component))
                throw
                    new System.InvalidOperationException(NotFromThisPool(component));
            if (parent == null)
                throw
                    new System.ArgumentNullException("parent");
            if (componentQueue.Count >= Limit)
                DestroyComponentObject(component);
            else
            {
                component.gameObject.SetActive(false);
                component.transform.SetParent(parent);
                component.transform.localPosition = Vector3.zero;
                component.transform.localRotation = Quaternion.identity;
                componentQueue.Enqueue(component);
            }
        }

        /// <summary>
        /// Deactivate a <typeparamref name="T"/>'s associated <see cref="GameObject"/>, set its
        /// <see cref="GameObject"/>'s <paramref name="name"/>, and send the <typeparamref name="T"/> back to the
        /// <see cref="ComponentPool{T}"/>.
        /// </summary>
        /// <param name="component">The <typeparamref name="T"/> to return to the <see cref="ComponentPool{T}"/>.
        /// </param>
        /// <param name="name">The <see cref="string"/> name to assign to the <typeparamref name="T"/>'s
        /// <see cref="GameObject"/>.</param>
        public void Return(T component, string name)
        {
            if (component == null)
                throw
                    new System.ArgumentNullException("component");
            if (!FromThisPool(component))
                throw
                    new System.InvalidOperationException(NotFromThisPool(component));
            component.name = name ??
                throw
                    new System.ArgumentNullException("name");
            if (componentQueue.Count >= Limit)
                DestroyComponentObject(component);
            else
            {
                component.gameObject.SetActive(false);
                component.name = name;
                componentQueue.Enqueue(component);
            }
        }

        /// <summary>
        /// Deactivate a <typeparamref name="T"/>'s associated <see cref="GameObject"/>, set its
        /// <see cref="Transform"/> <paramref name="parent"/>, set its <see cref="GameObject"/>'s
        /// <paramref name="name"/>, and send the <typeparamref name="T"/> back to the <see cref="ComponentPool{T}"/>.
        /// </summary>
        /// <param name="component">The <typeparamref name="T"/> to return to the <see cref="ComponentPool{T}"/>.
        /// </param>
        /// <param name="parent">The <see cref="Transform"/> to parent the <typeparamref name="T"/>'s
        /// <see cref="GameObject"/> to.</param>
        /// <param name="name">The <see cref="string"/> name to assign to the <typeparamref name="T"/>'s
        /// <see cref="GameObject"/>.</param>
        public void Return(T component, Transform parent, string name)
        {
            if (component == null)
                throw
                    new System.ArgumentNullException("component");
            if (!FromThisPool(component))
                throw
                    new System.InvalidOperationException(NotFromThisPool(component));
            if (parent == null)
                throw
                    new System.ArgumentNullException("parent");
            component.name = name ??
                throw
                    new System.ArgumentNullException("name");
            if (componentQueue.Count >= Limit)
                DestroyComponentObject(component);
            else
            {
                component.gameObject.SetActive(false);
                component.transform.SetParent(parent);
                component.transform.localPosition = Vector3.zero;
                component.transform.localRotation = Quaternion.identity;
                component.name = name;
                componentQueue.Enqueue(component);
            }
        }

        /// <summary>
        /// Deactivate the <see cref="GameObject"/>s of all <typeparamref name="T"/>s that were dispensed from the
        /// <see cref="ComponentPool{T}"/>, and return the <typeparamref name="T"/>s to the pool.
        /// </summary>
        public void ReturnAll()
        {
            foreach (T component in ownedComponents)
                if (component != null && !componentQueue.Contains(component))
                    Return(component);
            ownedComponents.RemoveAll(item => item == null);
        }

        /// <summary>
        /// Deactivate the <see cref="GameObject"/>s of all <typeparamref name="T"/>s that were dispensed from the
        /// <see cref="ComponentPool{T}"/>, set their <see cref="Transform"/> <paramref name="parent"/>s, and return
        /// the <typeparamref name="T"/>s to the pool.
        /// </summary>
        /// <param name="parent">The <see cref="Transform"/> to parent the collected <typeparamref name="T"/>s'
        /// <see cref="GameObject"/>s to.</param>
        public void ReturnAll(Transform parent)
        {
            if (parent == null)
                throw
                    new System.ArgumentNullException("parent");
            foreach (T component in ownedComponents)
                if (component != null && !componentQueue.Contains(component))
                    Return(component, parent);
            ownedComponents.RemoveAll(item => item == null);
        }

        /// <summary>
        /// Deactivate the <see cref="GameObject"/>s of all <typeparamref name="T"/>s that were dispensed from the
        /// <see cref="ComponentPool{T}"/>, set their <see cref="GameObject"/>s' <paramref name="name"/>s, and return
        /// the <typeparamref name="T"/>s to the pool.
        /// </summary>
        /// <param name="name">The <see cref="string"/> name to assign to the collected <typeparamref name="T"/>s'
        /// <see cref="GameObject"/>s.</param>
        public void ReturnAll(string name)
        {
            if (name == null || name == "")
                throw
                    new System.ArgumentNullException("name");
            foreach (T component in ownedComponents)
                if (component != null && !componentQueue.Contains(component))
                    Return(component, name);
            ownedComponents.RemoveAll(item => item == null);
        }

        /// <summary>
        /// Deactivate the <see cref="GameObject"/>s of all <typeparamref name="T"/>s that were dispensed from the
        /// <see cref="ComponentPool{T}"/>, set their <see cref="Transform"/> <paramref name="parent"/>s, set their
        /// <see cref="GameObject"/>s' <paramref name="name"/>s, and return the <typeparamref name="T"/>s to the pool.
        /// </summary>
        /// <param name="parent">The <see cref="Transform"/> to parent the collected <typeparamref name="T"/>s'
        /// <see cref="GameObject"/>s to.</param>
        /// <param name="name">The <see cref="string"/> name to assign to the collected <typeparamref name="T"/>s'
        /// <see cref="GameObject"/>s.</param>
        public void ReturnAll(Transform parent, string name)
        {
            if (parent == null)
                throw
                    new System.ArgumentNullException("parent");
            if (name == null || name == "")
                throw
                    new System.ArgumentNullException("name");
            foreach (T component in ownedComponents)
                if (component != null && !componentQueue.Contains(component))
                    Return(component, parent, name);
            ownedComponents.RemoveAll(item => item == null);
        }

        /// <summary>
        /// Set the value for <see cref="Limit"/>.
        /// <para>Pooled <typeparamref name="T"/>s in excess of the new limit will be destroyed automatically.</para>
        /// </summary>
        /// <param name="limit">The amount of <typeparamref name="T"/>s that the <see cref="ComponentPool{T}"/> 
        /// should store before destroying excess <typeparamref name="T"/>s.</param>
        public void SetLimit(int limit)
        {
            if (limit <= 0)
                throw
                    new System.ArgumentOutOfRangeException("limit", 
                    "The passed value for limit needs to be 1 or higher.");
            Limit = limit;
            while (componentQueue.Count > limit)
                EditModeSafe.Destroy(componentQueue.Dequeue().gameObject);
        }

        /// <summary>
        /// Destroy the <see cref="GameObject"/> of a <typeparamref name="T"/> that came from this
        /// <see cref="ComponentPool{T}"/>.
        /// </summary>
        /// <param name="component">The <see cref="Component"/> to destroy.</param>
        private void DestroyComponentObject(T component)
        {
            if (componentQueue.Contains(component))
                throw
                    new System.InvalidOperationException(
                        "ComponentPool may not destroy a Component while it's in a Queue.");
            if (FromThisPool(component))
                ownedComponents.Remove(component);
            else
                throw
                    new System.InvalidOperationException(NotFromThisPool(component));
            EditModeSafe.Destroy(component.gameObject);
        }

        /// <summary>
        /// Check if a specific <typeparamref name="T"/> <paramref name="component"/> is from this
        /// <see cref="ComponentPool{T}"/> or not.
        /// </summary>
        /// <param name="component">The <typeparamref name="T"/> to check.</param>
        /// <returns>True if the <typeparamref name="T"/> is from this <see cref="ComponentPool{T}"/></returns>
        private bool FromThisPool(T component)
        {
            return
                ownedComponents.Contains(component);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Exception Messages

        private string NotFromThisPool(Component component)
            => string.Format("The {0} was not generated by this ComponentPool.", component.GetType().Name);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
