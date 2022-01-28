using StephanHooft.Exceptions;
using System;
using UnityEngine;

namespace StephanHooft.Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Adds a <typeparamref name="TComponent"/> to the <see cref="GameObject"/>, and replaces an
        /// existing one if found.
        /// </summary>
        /// <returns>A <typeparamref name="TComponent"/>.</returns>
        public static TComponent AddOrReplaceComponent<TComponent>(this GameObject gameObject) 
            where TComponent : Component
        {
            if (gameObject.TryGetComponent(out TComponent component))
                UnityEngine.Object.Destroy(component);
            component = gameObject.AddComponent<TComponent>();
            return
                component;
        }

        /// <summary>
        /// Returns the normalised direction <see cref="Vector3"/> from this <see cref="GameObject"/>
        /// to another.
        /// </summary>
        /// <param name="destination">The destination <see cref="GameObject"/>.</param>
        /// <returns>A <see cref="Vector3"/> from the source <see cref="GameObject"/> to the destination
        /// <see cref="GameObject"/>.</returns>
        public static Vector3 DirectionTo(this GameObject gameObject, GameObject destination)
        {
            return
                Vector3.Normalize(destination.transform.position - gameObject.transform.position);
        }

        /// <summary>
        /// Returns the distance <see cref="float"/> from this <see cref="GameObject"/> to another.
        /// </summary>
        /// <param name="destination">The destination <see cref="GameObject"/>.</param>
        /// <returns>The <see cref="float"/> distance from the <paramref name="gameObject"/> to the
        /// <paramref name="destination"/>.</returns>
        public static float DistanceTo(this GameObject gameObject, GameObject destination)
        {
            return
                Vector3.Distance(gameObject.transform.position, destination.transform.position);
        }

        /// <summary>
        /// Returns the distance <see cref="float"/> from this <see cref="GameObject"/> to a
        /// <see cref="Vector3"/> point.
        /// </summary>
        /// <param name="destination">The destination <see cref="Vector3"/>.</param>
        /// <returns>The <see cref="float"/> distance from the <paramref name="gameObject"/> to the
        /// <paramref name="destination"/>.</returns>
        public static float DistanceTo(this GameObject gameObject, Vector3 destination)
        {
            return
                Vector3.Distance(gameObject.transform.position, destination);
        }

        /// <summary>
        /// Gets a <typeparamref name="TComponent"/> from the <see cref="GameObject"/>'s hierarchy.
        /// </summary>
        /// <returns>A <typeparamref name="TComponent"/>, or <see cref="null"/> if none are
        /// found.</returns>
        public static TComponent GetComponentInHierarchy<TComponent>(this GameObject gameObject)
            where TComponent : Component
        {
            return
                gameObject.transform.root.GetComponentInChildren<TComponent>();
        }

        /// <summary>
        /// Gets all <typeparamref name="TComponent"/>s from the <see cref="GameObject"/>'s hierarchy.
        /// </summary>
        /// <returns>A <typeparamref name="TComponent"/>[].</returns>
        public static TComponent[] GetComponentsInHierarchy<TComponent>(this GameObject gameObject)
            where TComponent : Component
        {
            return
                gameObject.transform.root.GetComponentsInChildren<TComponent>();
        }

        /// <summary>
        /// Gets a <typeparamref name="TComponent"/> from the <see cref="GameObject"/>, and throws a
        /// <see cref="ComponentNotFoundException{TComponent}"/> if none are found.
        /// </summary>
        /// <param name="destroyGameObjectOnFailure">Set to <see cref="true"/> to destroy the 
        ///  <see cref="GameObject"/> if no <typeparamref name="TComponent"/> is found.</param>
        /// <returns>A <typeparamref name="TComponent"/>.</returns>
        public static TComponent GetEssentialComponent<TComponent>
            (this GameObject gameObject, bool destroyGameObjectOnFailure = false)
            where TComponent: Component
        {
            if (gameObject.TryGetComponent(out TComponent component))
                return
                    component;
            else
            {
                if (destroyGameObjectOnFailure)
                    UnityEngine.Object.Destroy(gameObject);
                throw
                    new ComponentNotFoundException<TComponent>();
            }
        }

        /// <summary>
        /// Gets a <typeparamref name="TComponent"/> from the <see cref="GameObject"/> or its children,
        /// and throws a <see cref="ComponentNotFoundException{TComponent}"/> if none are found.
        /// </summary>
        /// <param name="destroyGameObjectOnFailure">Set to <see cref="true"/> to destroy the 
        ///  <see cref="GameObject"/> if no <typeparamref name="TComponent"/> is found.</param>
        /// <returns>A <typeparamref name="TComponent"/>.</returns>
        public static TComponent GetEssentialComponentInChildren<TComponent>
            (this GameObject gameObject, bool destroyGameObjectOnFailure = false)
            where TComponent: Component
        {
            var component = gameObject.GetComponentInChildren<TComponent>();
            if (component != null)
                return
                    component;
            else
            {
                if(destroyGameObjectOnFailure)
                    UnityEngine.Object.Destroy(gameObject);
                throw
                    new ComponentNotFoundException<TComponent>();
            }
        }

        /// <summary>
        /// Gets a <typeparamref name="TComponent"/> from the <see cref="GameObject"/>'s hierarchy,
        /// and throws a <see cref="ComponentNotFoundException{TComponent}"/> if none are found.
        /// </summary>
        /// <param name="destroyGameObjectOnFailure">Set to <see cref="true"/> to destroy the 
        ///  <see cref="GameObject"/> if no <typeparamref name="TComponent"/> is found.</param>
        /// <returns>A <typeparamref name="TComponent"/>.</returns>
        public static TComponent GetEssentialComponentInHierarchy<TComponent>
            (this GameObject gameObject, bool destroyGameObjectOnFailure = false)
            where TComponent : Component
        {
            var component = gameObject.transform.root.GetComponentInChildren<TComponent>();
            if (component != null)
                return
                    component;
            else
            {
                if (destroyGameObjectOnFailure)
                    UnityEngine.Object.Destroy(gameObject);
                throw
                    new ComponentNotFoundException<TComponent>();
            }
        }

        /// <summary>
        /// Gets a <typeparamref name="TComponent"/> from the <see cref="GameObject"/> or its parents,
        /// and throws a <see cref="ComponentNotFoundException{TComponent}"/> if none are found.
        /// </summary>
        /// <param name="destroyGameObjectOnFailure">Set to <see cref="true"/> to destroy the 
        ///  <see cref="GameObject"/> if no <typeparamref name="TComponent"/> is found.</param>
        /// <returns>A <typeparamref name="TComponent"/>.</returns>
        public static TComponent GetEssentialComponentInParent<TComponent>
            (this GameObject gameObject, bool destroyGameObjectOnFailure = false) 
            where TComponent : Component
        {
            var component = gameObject.GetComponentInParent<TComponent>();
            if (component != null)
                return
                    component;
            else
            {
                if (destroyGameObjectOnFailure)
                    UnityEngine.Object.Destroy(gameObject);
                throw
                    new ComponentNotFoundException<TComponent>();
            }
        }

        /// <summary>
        /// Gets a <typeparamref name="TComponent"/> from the <see cref="GameObject"/>, and adds one
        /// if none are found.
        /// </summary>
        /// <returns>A <typeparamref name="TComponent"/>.</returns>
        public static TComponent GetOrAddComponent<TComponent>(this GameObject gameObject)
            where TComponent: Component
        {
            if (!gameObject.TryGetComponent(out TComponent component))
                component = gameObject.AddComponent<TComponent>();
            return
                component;
        }

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="GameObject"/> has a 
        /// <typeparamref name="TComponent"/>.
        /// </summary>
        /// <returns><see cref="true"/> if the <see cref="GameObject"/> has a <see cref="Component"/> of
        /// <see cref="Type"/> <typeparamref name="TComponent"/>.</returns>
        public static bool HasComponent<TComponent>(this GameObject gameObject)
            where TComponent: Component
        {
            return
                gameObject.GetComponent<TComponent>() != null;
        }
    }
}
