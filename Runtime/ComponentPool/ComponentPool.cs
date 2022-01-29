using System;
using System.Collections.Generic;
using UnityEngine;

namespace StephanHooft.ComponentPool
{
    /// <summary>
    /// A generic pooler class for objects with a <see cref="Component"/>-inheriting class instance attached to it. Can be used as an alternative 
    /// to repeatedly creating/destroying <see cref="GameObject"/>s with a certain <see cref="Component"/>.
    /// <para><see cref="GameObject"/>s attached to <see cref="Component"/>s provided by the <see cref="ComponentPool{T}"/> are deactivated when returned to it, and
    ///  activated upon being retrieved again.</para>
    /// <para>The class also offers method overloads to change a <see cref="GameObject"/>'s name or <see cref="Transform"/> parent
    /// when their associated <see cref="Component"/>s are retrieved from (or returned to) the <see cref="ComponentPool{T}"/>.</para>
    /// </summary>
    public sealed class ComponentPool<T> where T : Component
    {
        /// <summary>
        /// The maximum amount of <typeparamref name="T"/>s the <see cref="ComponentPool{T}"/> will store before destroying excess <typeparamref name="T"/>s.
        /// </summary>
        public int Limit { get; private set; }

        /// <summary>
        /// Returns the amount of <typeparamref name="T"/>s in the <see cref="ComponentPool{T}"/>.
        /// </summary>
        public int Count => componentQueue.Count;

        /// <summary>
        /// A list of every <typeparamref name="T"/> known to the <see cref="ComponentPool{T}"/>, both active and inactive.
        /// </summary>
        private readonly List<T> components = new List<T>();

        /// <summary>
        /// A First-In-First-Out <see cref="Queue{T}"/> of inactive <typeparamref name="T"/>s to draw from.
        /// </summary>
        private readonly Queue<T> componentQueue = new Queue<T>();

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Create a new <see cref="ComponentPool{T}"/>.
        /// </summary>
        /// <param name="limit">The amount of <typeparamref name="T"/>s that the <see cref="ComponentPool{T}"/> 
        /// should store before destroying excess <typeparamref name="T"/>s.</param>
        public ComponentPool(int limit = 10)
        {
            if (limit <= 0) 
                throw new ArgumentOutOfRangeException("queueLimit", "queueLimit must be greater than 0.");
            Limit = limit;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Create a new <see cref="GameObject"/> with a <typeparamref name="T"/> attached to it, and add it to the <see cref="ComponentPool{T}"/>.
        /// </summary>
        public void AddToPool()
        {
            if (componentQueue.Count < Limit)
            {
                var newGameObject = new GameObject();
                newGameObject.SetActive(false);
                var newComponent = (T)newGameObject.AddComponent(typeof(T));
                components.Add(newComponent);
                componentQueue.Enqueue(newComponent);
            }
            else 
                Debug.LogWarning("Cannot add Components in excess of the ComponentPool's QueueLimit.");
        }

        /// <summary>
        /// Add a manually-created <see cref="GameObject"/> with a <typeparamref name="T"/> to the <see cref="ComponentPool{T}"/>.
        /// <para>This method should be used if you don't want the <see cref="ComponentPool{T}"/> to automatically create a <see cref="GameObject"/>
        /// with a matching <typeparamref name="T"/>, but want to configure the <see cref="GameObject"/> yourself before adding them to the 
        /// <see cref="ComponentPool{T}"/>.</para>
        /// </summary>
        /// <param name="component">The <typeparamref name="T"/> to add to the <see cref="ComponentPool{T}"/>.</param>
        public void AddToPool(T component)
        {
            if (component == null) 
                throw 
                    new ArgumentNullException("component");
            if (components.Contains(component))
                throw 
                    new InvalidOperationException(string.Format("Component of {0} already added to ComponentPool.", component.name));
            component.gameObject.SetActive(false);
            components.Add(component);
            componentQueue.Enqueue(component);
        }

        /// <summary>
        /// Get a <typeparamref name="T"/> from the <see cref="ComponentPool{T}"/> and activate it.
        /// </summary>
        /// <returns>A <typeparamref name="T"/> attached to an active <see cref="GameObject"/>.</returns>
        public T GetFromPool()
        {
            if (componentQueue.Count == 0)
                AddToPool();
            var component = componentQueue.Dequeue();
            component.gameObject.SetActive(true);
            return
                component;
        }

        /// <summary>
        /// Get a <typeparamref name="T"/> from the <see cref="ComponentPool{T}"/> and set its <see cref="Transform"/> <paramref name="parent"/>.
        /// </summary>
        /// <param name="parent">The <see cref="Transform"/> to parent the <typeparamref name="T"/>'s <see cref="GameObject"/> to.</param>
        /// <returns>A <typeparamref name="T"/> attached to an active <see cref="GameObject"/>.</returns>
        public T GetFromPool(Transform parent)
        {
            if (parent == null)
                throw
                    new ArgumentNullException("parent");
            if (componentQueue.Count == 0) AddToPool();
            var component = componentQueue.Dequeue();
            component.transform.SetParent(parent);
            component.gameObject.SetActive(true);
            return
                component;
        }

        /// <summary>
        /// Get a <typeparamref name="T"/> from the <see cref="ComponentPool{T}"/> and set its <see cref="GameObject"/>'s <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The <see cref="string"/> name to assign to the <typeparamref name="T"/>'s <see cref="GameObject"/>.</param>
        /// <returns>A <typeparamref name="T"/> attached to an active <see cref="GameObject"/>.</returns>
        public T GetFromPool(string name)
        {
            if (name == null || name == "")
                throw
                    new ArgumentNullException("name");
            if (componentQueue.Count == 0)
                AddToPool();
            var component = componentQueue.Dequeue();
            component.name = name;
            component.gameObject.SetActive(true);
            return
                component;
        }

        /// <summary>
        /// Get a <typeparamref name="T"/> from the <see cref="ComponentPool{T}"/>, set its <see cref="Transform"/> <paramref name="parent"/>,
        /// and set its <see cref="GameObject"/>'s <paramref name="name"/>.
        /// </summary>
        /// <param name="parent">The <see cref="Transform"/> to parent the <typeparamref name="T"/>'s <see cref="GameObject"/> to.</param>
        /// <param name="name">The <see cref="string"/> name to assign to the <typeparamref name="T"/>'s <see cref="GameObject"/>.</param>
        /// <returns>A <typeparamref name="T"/> attached to an active <see cref="GameObject"/>.</returns>
        public T GetFromPool(Transform parent, string name)
        {
            if (parent == null)
                throw
                    new ArgumentNullException("parent");
            if (name == null || name == "")
                throw
                    new ArgumentNullException("name");
            if (componentQueue.Count == 0)
                AddToPool();
            var component = componentQueue.Dequeue();
            component.transform.SetParent(parent);
            component.name = name;
            component.gameObject.SetActive(true);
            return
                component;
        }

        /// <summary>
        /// Deactivate a <typeparamref name="T"/>'s associated <see cref="GameObject"/> and send the <typeparamref name="T"/> back to the <see cref="ComponentPool{T}"/>.
        /// </summary>
        /// <param name="component">The <typeparamref name="T"/> to return to the <see cref="ComponentPool{T}"/>.</param>
        public void ReturnToPool(T component)
        {
            if (component == null)
                throw
                    new ArgumentNullException("component");
            if (!FromThisPool(component))
                throw
                    new ArgumentException("Cannot return a component to a ComponentPool that didn't dispense it.", "component");
            if (componentQueue.Count >= Limit)
                DestroyComponentObject(component);
            else
            {
                component.gameObject.SetActive(false);
                componentQueue.Enqueue(component);
            }
        }

        /// <summary>
        /// Deactivate a <typeparamref name="T"/>'s associated <see cref="GameObject"/>, set its <see cref="Transform"/> <paramref name="parent"/>,
        /// and send the <typeparamref name="T"/> back to the <see cref="ComponentPool{T}"/>.
        /// </summary>
        /// <param name="component">The <typeparamref name="T"/> to return to the <see cref="ComponentPool{T}"/>.</param>
        /// <param name="parent">The <see cref="Transform"/> to parent the <typeparamref name="T"/>'s <see cref="GameObject"/> to.</param>
        public void ReturnToPool(T component, Transform parent)
        {
            if (component == null)
                throw
                    new ArgumentNullException("component");
            if (!FromThisPool(component))
                throw
                    new ArgumentException("Cannot return a component to a ComponentPool that didn't dispense it.", "component");
            if (parent == null)
                throw
                    new ArgumentNullException("parent");
            if (componentQueue.Count >= Limit)
                DestroyComponentObject(component);
            else
            {
                component.gameObject.SetActive(false);
                component.transform.SetParent(parent);
                componentQueue.Enqueue(component);
            }
        }

        /// <summary>
        /// Deactivate a <typeparamref name="T"/>'s associated <see cref="GameObject"/>, set its <see cref="GameObject"/>'s <paramref name="name"/>,
        /// and send the <typeparamref name="T"/> back to the <see cref="ComponentPool{T}"/>.
        /// </summary>
        /// <param name="component">The <typeparamref name="T"/> to return to the <see cref="ComponentPool{T}"/>.</param>
        /// <param name="name">The <see cref="string"/> name to assign to the <typeparamref name="T"/>'s <see cref="GameObject"/>.</param>
        public void ReturnToPool(T component, string name)
        {
            if (component == null)
                throw
                    new ArgumentNullException("component");
            if (!FromThisPool(component))
                throw
                    new ArgumentException("Cannot return a component to a ComponentPool that didn't dispense it.", "component");
            component.name = name ??
                throw
                    new ArgumentNullException("name");
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
        /// Deactivate a <typeparamref name="T"/>'s associated <see cref="GameObject"/>, set its <see cref="Transform"/> <paramref name="parent"/>,
        /// set its <see cref="GameObject"/>'s <paramref name="name"/>, and send the <typeparamref name="T"/> back to the <see cref="ComponentPool{T}"/>.
        /// </summary>
        /// <param name="component">The <typeparamref name="T"/> to return to the <see cref="ComponentPool{T}"/>.</param>
        /// <param name="parent">The <see cref="Transform"/> to parent the <typeparamref name="T"/>'s <see cref="GameObject"/> to.</param>
        /// <param name="name">The <see cref="string"/> name to assign to the <typeparamref name="T"/>'s <see cref="GameObject"/>.</param>
        public void ReturnToPool(T component, Transform parent, string name)
        {
            if (component == null)
                throw
                    new ArgumentNullException("component");
            if (!FromThisPool(component))
                throw
                    new ArgumentException("Cannot return a component to a ComponentPool that didn't dispense it.", "component");
            if (parent == null)
                throw
                    new ArgumentNullException("parent");
            component.name = name ?? 
                throw
                    new ArgumentNullException("name");
            if (componentQueue.Count >= Limit)
                DestroyComponentObject(component);
            else
            {
                component.gameObject.SetActive(false);
                component.transform.SetParent(parent);
                component.name = name;
                componentQueue.Enqueue(component);
            }
        }

        /// <summary>
        /// Deactivate the <see cref="GameObject"/>s of all <typeparamref name="T"/>s that were dispensed from the <see cref="ComponentPool{T}"/>,
        /// and return the <typeparamref name="T"/>s to the pool.
        /// </summary>
        public void CollectAndReturnAllToPool()
        {
            foreach (T component in components)
                if (component != null && !componentQueue.Contains(component))
                    ReturnToPool(component);
            components.RemoveAll(item => item == null); // Remove all "null" entries from the list, in case one of the Components got destroyed somehow.
        }

        /// <summary>
        /// Deactivate the <see cref="GameObject"/>s of all <typeparamref name="T"/>s that were dispensed from the <see cref="ComponentPool{T}"/>,
        /// set their <see cref="Transform"/> <paramref name="parent"/>s, and return the <typeparamref name="T"/>s to the pool.
        /// </summary>
        /// <param name="parent">The <see cref="Transform"/> to parent the collected <typeparamref name="T"/>s' <see cref="GameObject"/>s to.</param>
        public void CollectAndReturnAllToPool(Transform parent)
        {
            if (parent == null)
                throw
                    new ArgumentNullException("parent");
            foreach (T component in components)
                if (component != null && !componentQueue.Contains(component)) 
                    ReturnToPool(component, parent);
            components.RemoveAll(item => item == null); // Remove all "null" entries from the list, in case one of the Components got destroyed somehow.
        }

        /// <summary>
        /// Deactivate the <see cref="GameObject"/>s of all <typeparamref name="T"/>s that were dispensed from the <see cref="ComponentPool{T}"/>,
        /// set their <see cref="GameObject"/>s' <paramref name="name"/>s, and return the <typeparamref name="T"/>s to the pool.
        /// </summary>
        /// <param name="name">The <see cref="string"/> name to assign to the collected <typeparamref name="T"/>s' <see cref="GameObject"/>s.</param>
        public void CollectAndReturnAllToPool(string name)
        {
            if (name == null || name == "")
                throw
                    new ArgumentNullException("name");
            foreach (T component in components)
                if (component != null && !componentQueue.Contains(component))
                    ReturnToPool(component, name);
            components.RemoveAll(item => item == null); // Remove all "null" entries from the list, in case one of the Components got destroyed somehow.
        }

        /// <summary>
        /// Deactivate the <see cref="GameObject"/>s of all <typeparamref name="T"/>s that were dispensed from the <see cref="ComponentPool{T}"/>,
        /// set their <see cref="Transform"/> <paramref name="parent"/>s, set their <see cref="GameObject"/>s' <paramref name="name"/>s, 
        /// and return the <typeparamref name="T"/>s to the pool.
        /// </summary>
        /// <param name="parent">The <see cref="Transform"/> to parent the collected <typeparamref name="T"/>s' <see cref="GameObject"/>s to.</param>
        /// <param name="name">The <see cref="string"/> name to assign to the collected <typeparamref name="T"/>s' <see cref="GameObject"/>s.</param>
        public void CollectAndReturnAllToPool(Transform parent, string name)
        {
            if (parent == null)
                throw
                    new ArgumentNullException("parent");
            if (name == null || name == "")
                throw
                    new ArgumentNullException("name");
            foreach (T component in components)
                if (component != null && !componentQueue.Contains(component))
                    ReturnToPool(component, parent, name);
            components.RemoveAll(item => item == null); // Remove all "null" entries from the list, in case one of the Components got destroyed somehow.
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
                    new ArgumentOutOfRangeException("limit", "The passed value for limit needs to be 1 or higher.");
            Limit = limit;
            while (componentQueue.Count > limit)
                Extensions.EditModeSafe.Destroy(componentQueue.Dequeue().gameObject);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Destroy the <see cref="GameObject"/> of a <typeparamref name="T"/> that came from this <see cref="ComponentPool{T}"/>.
        /// </summary>
        /// <param name="componentToDestroy"></param>
        private void DestroyComponentObject(T componentToDestroy)
        {
            if (componentQueue.Contains(componentToDestroy))
                throw
                    new InvalidOperationException("ComponentPool may not destroy a Component while it's in a Queue.");
            if (FromThisPool(componentToDestroy))
                components.Remove(componentToDestroy);
            else 
                throw
                    new InvalidOperationException("ComponentPool may only destroy Components that it itself has created.");
            Extensions.EditModeSafe.Destroy(componentToDestroy.gameObject);
        }

        /// <summary>
        /// Check if a specific <typeparamref name="T"/> <paramref name="component"/> is from this <see cref="ComponentPool{T}"/> or not.
        /// </summary>
        /// <param name="component">The <typeparamref name="T"/> to check.</param>
        /// <returns>True if the <typeparamref name="T"/> is from this <see cref="ComponentPool{T}"/></returns>
        private bool FromThisPool(T component)
        {
            return
                components.Contains(component);
        }
    }
}
