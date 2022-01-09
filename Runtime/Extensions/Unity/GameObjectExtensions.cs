using System;
using UnityEngine;

namespace StephanHooft.Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Adds a <typeparamref name="T"/> to the <see cref="GameObject"/>, and replaces an existing one if found.
        /// </summary>
        /// <returns>A <typeparamref name="T"/>.</returns>
        public static T AddOrReplaceComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.TryGetComponent(out T component))
                UnityEngine.Object.Destroy(component);
            component = gameObject.AddComponent<T>();
            return component;
        }

        /// <summary>
        /// Returns the normalised direction <see cref="Vector3"/> from this <see cref="GameObject"/> to another.
        /// </summary>
        /// <param name="destination">The destination <see cref="GameObject"/>.</param>
        /// <returns>A <see cref="Vector3"/> from the source <see cref="GameObject"/> to the destination <see cref="GameObject"/>.</returns>
        public static Vector3 DirectionTo(this GameObject source, GameObject destination)
        {
            return Vector3.Normalize(destination.transform.position - source.transform.position);
        }

        /// <summary>
        /// Returns the distance <see cref="float"/> from this <see cref="GameObject"/> to another.
        /// </summary>
        /// <param name="destination">The destination <see cref="GameObject"/>.</param>
        /// <returns>The <see cref="float"/> distance from the <paramref name="source"/> to the <paramref name="destination"/>.</returns>
        public static float DistanceTo(this GameObject source, GameObject destination)
        {
            return Vector3.Distance(source.transform.position, destination.transform.position);
        }

        /// <summary>
        /// Returns the distance <see cref="float"/> from this <see cref="GameObject"/> to a <see cref="Vector3"/> point.
        /// </summary>
        /// <param name="destination">The destination <see cref="Vector3"/>.</param>
        /// <returns>The <see cref="float"/> distance from the <paramref name="source"/> to the <paramref name="destination"/>.</returns>
        public static float DistanceTo(this GameObject source, Vector3 destination)
        {
            return Vector3.Distance(source.transform.position, destination);
        }

        /// <summary>
        /// Gets a <typeparamref name="T"/> from the <see cref="GameObject"/>, and throws an <see cref="Exception"/> if none are found.
        /// </summary>
        /// <param name="destroyGameObjectOnFailure">Set to true to destroy the <see cref="GameObject"/> if no <typeparamref name="T"/> is found.</param>
        /// <returns>A <typeparamref name="T"/>.</returns>
        public static T GetEssentialComponent<T>(this GameObject gameObject, bool destroyGameObjectOnFailure = false) where T: Component
        {
            if (gameObject.TryGetComponent(out T component))
                return component;
            else
            {
                if (destroyGameObjectOnFailure)
                    UnityEngine.Object.Destroy(gameObject);
                throw new Exception("Cannot find a " + typeof(T) + ".");
            }
        }

        /// <summary>
        /// Gets a <typeparamref name="T"/> from the <see cref="GameObject"/> or its children, and throws an <see cref="Exception"/> if none are found.
        /// </summary>
        /// <param name="destroyGameObjectOnFailure">Set to true to destroy the <see cref="GameObject"/> if no <typeparamref name="T"/> is found.</param>
        /// <returns>A <typeparamref name="T"/>.</returns>
        public static T GetEssentialComponentInChildren<T>(this GameObject gameObject, bool destroyGameObjectOnFailure = false) where T: Component
        {
            T component = gameObject.GetComponentInChildren<T>();
            if (component != null)
                return component;
            else
            {
                if(destroyGameObjectOnFailure)
                    UnityEngine.Object.Destroy(gameObject);
                throw new Exception("Cannot find a " + typeof(T) + ".");
            }
        }

        /// <summary>
        /// Gets a <typeparamref name="T"/> from the <see cref="GameObject"/> or its parents, and throws an <see cref="Exception"/> if none are found.
        /// </summary>
        /// <param name="destroyGameObjectOnFailure">Set to true to destroy the <see cref="GameObject"/> if no <typeparamref name="T"/> is found.</param>
        /// <returns>A <typeparamref name="T"/>.</returns>
        public static T GetEssentialComponentInParent<T>(this GameObject gameObject, bool destroyGameObjectOnFailure = false) where T : Component
        {
            T component = gameObject.GetComponentInParent<T>();
            if (component != null)
                return component;
            else
            {
                if (destroyGameObjectOnFailure)
                    UnityEngine.Object.Destroy(gameObject);
                throw new Exception("Cannot find a " + typeof(T) + ".");
            }
        }

        /// <summary>
        /// Gets a <typeparamref name="T"/> from the <see cref="GameObject"/>, and adds one if none are found.
        /// </summary>
        /// <returns>A <typeparamref name="T"/>.</returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T: Component
        {
            if (!gameObject.TryGetComponent(out T component))
                component = gameObject.AddComponent<T>();
            return component;
        }

        /// <summary>
        /// Returns true if the <see cref="GameObject"/> has a <typeparamref name="T"/>.
        /// </summary>
        /// <returns>True if the <see cref="GameObject"/> has a <see cref="Component"/> of <see cref="Type"/> <typeparamref name="T"/>.</returns>
        public static bool HasComponent<T>(this GameObject gameObject)
        {
            return gameObject.GetComponent<T>() != null;
        }
    }
}
