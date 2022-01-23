using System.Collections.Generic;
using UnityEngine;

namespace StephanHooft.Extensions
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Destroys all child <see cref="GameObject"/>s of the <see cref="Transform"/>.
        /// </summary>
        public static void DestroyChildren(this Transform transform)
        {
            var children = new List<GameObject>();
            foreach (Transform child in transform)
                children.Add(child.gameObject);
            children.ForEach(child => Object.Destroy(child));
        }

        /// <summary>
        /// Returns the normalised direction <see cref="Vector3"/> from this <see cref="Transform"/> to another.
        /// </summary>
        /// <param name="destination">The destination <see cref="Transform"/>.</param>
        /// <returns>A <see cref="Vector3"/> from the source <see cref="Transform"/> to the destination <see cref="Transform"/>.</returns>
        public static Vector3 DirectionTo(this Transform source, Transform destination)
        {
            return
                Vector3.Normalize(destination.position - source.position);
        }

        /// <summary>
        /// Returns the distance <see cref="float"/> from this <see cref="Transform"/> to another.
        /// </summary>
        /// <param name="destination">The destination <see cref="Transform"/>.</param>
        /// <returns>The <see cref="float"/> distance from the <paramref name="source"/> to the <paramref name="destination"/>.</returns>
        public static float DistanceTo(this Transform source, Transform destination)
        {
            return
                Vector3.Distance(source.position, destination.position);
        }

        /// <summary>
        /// Returns the distance <see cref="float"/> from this <see cref="Transform"/> to a <see cref="Vector3"/> point.
        /// </summary>
        /// <param name="destination">The destination <see cref="Vector3"/>.</param>
        /// <returns>The <see cref="float"/> distance from the <paramref name="source"/> to the <paramref name="destination"/>.</returns>
        public static float DistanceTo(this Transform source, Vector3 destination)
        {
            return
                Vector3.Distance(source.position, destination);
        }

        /// <summary>
        /// Sets the local position and rotation of the <see cref="Transform"/> component.
        /// </summary>
        /// <param name="position">The target <see cref="Vector3"/> position.</param>
        /// <param name="rotation">The target <see cref="Quaternion"/> rotation.</param>
        public static void SetLocalPositionAndRotation(this Transform transform, Vector3 position, Quaternion rotation)
        {
            transform.localPosition = position;
            transform.localRotation = rotation;
        }
    }
}
