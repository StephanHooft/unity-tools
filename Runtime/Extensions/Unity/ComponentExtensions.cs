using System;
using UnityEngine;

namespace StephanHooft.Extensions
{
    public static class ComponentExtensions
    {
        /// <summary>
        /// Adds a <typeparamref name="T"/> to the <see cref="Component"/>'s <see cref="GameObject"/>,
        /// and replaces an existing one if found.
        /// </summary>
        /// <returns>A <typeparamref name="T"/>.</returns>
        public static T AddComponent<T>(this Component component) where T: Component
        {
            return component.gameObject.AddComponent<T>();
        }

        /// <summary>
        /// Adds a <typeparamref name="T"/> to the <see cref="Component"/>'s <see cref="GameObject"/>,
        /// and replaces an existing one if found.
        /// </summary>
        /// <returns>A <typeparamref name="T"/>.</returns>
        public static T AddOrReplaceComponent<T>(this Component component) where T : Component
        {
            if (component.TryGetComponent(out T foundComponent))
                UnityEngine.Object.Destroy(foundComponent);
            foundComponent = component.gameObject.AddComponent<T>();
            return foundComponent;
        }

        /// <summary>
        /// Gets a <typeparamref name="T"/> from the <see cref="Component"/>'s <see cref="GameObject"/>,
        /// and throws an <see cref="Exception"/> if none are found.
        /// </summary>
        /// <param name="destroyGameObjectOnFailure">Set to true to destroy the <see cref="Component"/>'s<see cref="GameObject"/> if no 
        /// <typeparamref name="T"/> is found.</param>
        /// <returns>A <typeparamref name="T"/>.</returns>
        public static T GetEssentialComponent<T>(this Component component, bool destroyGameObjectOnFailure = false) where T : Component
        {
            if (component.TryGetComponent(out T otherComponent))
                return otherComponent;
            else
            {
                if (destroyGameObjectOnFailure)
                    UnityEngine.Object.Destroy(component.gameObject);
                throw new Exception("Cannot find a " + typeof(T) + ".");
            }
        }

        /// <summary>
        /// Gets a <typeparamref name="T"/> from the <see cref="Component"/>'s <see cref="GameObject"/> or its children, 
        /// and throws an <see cref="Exception"/> if none are found.
        /// </summary>
        /// <param name="destroyGameObjectOnFailure">Set to true to destroy the <see cref="GameObject"/> if no <typeparamref name="T"/> is found.</param>
        /// <returns>A <typeparamref name="T"/>.</returns>
        public static T GetEssentialComponentInChildren<T>(this Component component, bool destroyGameObjectOnFailure = false) where T : Component
        {
            T otherComponent = component.GetComponentInChildren<T>();
            if (otherComponent != null)
                return otherComponent;
            else
            {
                if (destroyGameObjectOnFailure)
                    UnityEngine.Object.Destroy(component.gameObject);
                throw new Exception("Cannot find a " + typeof(T) + ".");
            }
        }

        /// <summary>
        /// Gets a <typeparamref name="T"/> from the <see cref="Component"/>'s <see cref="GameObject"/> or its parents, 
        /// and throws an <see cref="Exception"/> if none are found.
        /// </summary>
        /// <param name="destroyGameObjectOnFailure">Set to true to destroy the <see cref="GameObject"/> if no <typeparamref name="T"/> is found.</param>
        /// <returns>A <typeparamref name="T"/>.</returns>
        public static T GetEssentialComponentInParent<T>(this Component component, bool destroyGameObjectOnFailure = false) where T : Component
        {
            T otherComponent = component.GetComponentInParent<T>();
            if (otherComponent != null)
                return otherComponent;
            else
            {
                if (destroyGameObjectOnFailure)
                    UnityEngine.Object.Destroy(component.gameObject);
                throw new Exception("Cannot find a " + typeof(T) + ".");
            }
        }

        /// <summary>
        /// Gets a <typeparamref name="T"/> from the <see cref="Component"/>'s <see cref="GameObject"/>, and adds one if none are found.
        /// </summary>
        /// <returns>A <typeparamref name="T"/>.</returns>
        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            if (!component.TryGetComponent(out T foundComponent))
                foundComponent = component.gameObject.AddComponent<T>();
            return foundComponent;
        }

        /// <summary>
        /// Returns true if the <see cref="Component"/>'s <see cref="GameObject"/> has a <typeparamref name="T"/>.
        /// </summary>
        /// <returns>True if the <see cref="Component"/>'s <see cref="GameObject"/> has a <see cref="Component"/> of <see cref="Type"/> 
        /// <typeparamref name="T"/>.</returns>
        public static bool HasComponent<T>(this Component component) where T : Component
        {
            return component.GetComponent<T>() != null;
        }
    }
}
