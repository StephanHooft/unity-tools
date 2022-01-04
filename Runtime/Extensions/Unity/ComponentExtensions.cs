using System;
using UnityEngine;

namespace StephanHooft.Extensions
{
    public static class ComponentExtensions
    {
        /// <summary>
        /// Gets a <typeparamref name="T"/> from the <see cref="Component"/>, and throws an <see cref="Exception"/> it none can be found.
        /// </summary>
        /// <returns>A <typeparamref name="T"/>.</returns>
        public static T GetEssentialComponent<T>(this Component component) where T: Component
        {
            if (component.TryGetComponent(out T requestedComponent))
                return requestedComponent;
            else
                throw new Exception("Cannot find a " + typeof(T) + ".");
        }

        /// <summary>
        /// Gets a <typeparamref name="T"/> from the <see cref="Component"/>, and throws an <see cref="Exception"/> it none can be found.
        /// <para><paramref name="objectToDestroy"/> will also be destroyed if no <typeparamref name="T"/> is found.</para>
        /// </summary>
        /// <param name="objectToDestroy">The <see cref="GameObject"/> to destroy if no <typeparamref name="T"/> could be found.</param>
        /// <returns>A <typeparamref name="T"/>.</returns>
        public static T GetEssentialComponent<T>(this Component component, GameObject objectToDestroy) where T: Component
        {
            if (component.TryGetComponent(out T requestedComponent))
                return requestedComponent;
            else
            {
                UnityEngine.Object.Destroy(objectToDestroy);
                throw new Exception("Cannot find a " + typeof(T) + ".");
            }
        }
    }
}
