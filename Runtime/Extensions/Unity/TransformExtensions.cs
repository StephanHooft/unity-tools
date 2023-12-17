using StephanHooft.EditorSafe;
using System.Collections.Generic;
using UnityEngine;

namespace StephanHooft.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Transform"/>.
    /// </summary>
    public static class TransformExtensions
    {
        #region Static Methods

        /// <summary>
        /// Creates a new <see cref="GameObject"/> with the current <see cref="Transform"/> as its parent.
        /// </summary>
        /// <returns>The newly created <see cref="GameObject"/>.</returns>
        public static GameObject AddChildGameObject
            (this Transform transform)
        {
            var childObject = new GameObject();
            childObject.transform.SetParent(transform);
            childObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            return childObject;
        }

        /// <summary>
        /// Creates a new <see cref="GameObject"/> with the current <see cref="Transform"/> as its parent.
        /// </summary>
        /// <param name="name">The name of the new <see cref="GameObject"/>.</param>
        /// <returns>The newly created <see cref="GameObject"/>.</returns>
        public static GameObject AddChildGameObject
            (this Transform transform, string name)
        {
            var childObject = new GameObject(name);
            childObject.transform.SetParent(transform);
            childObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            return childObject;
        }

        /// <summary>
        /// Creates a new <see cref="GameObject"/> with the current <see cref="Transform"/> as its parent.
        /// </summary>
        /// <param name="localPosition">The local starting position of the new <see cref="GameObject"/>.</param>
        /// <param name="localRotation">The local starting rotation of the new <see cref="GameObject"/>.</param>
        /// <returns>The newly created <see cref="GameObject"/>.</returns>
        public static GameObject AddChildGameObject
            (this Transform transform, Vector3 localPosition, Quaternion localRotation)
        {
            var childObject = new GameObject();
            childObject.transform.SetParent(transform);
            childObject.transform.SetLocalPositionAndRotation(localPosition, localRotation);
            return childObject;
        }

        /// <summary>
        /// Creates a new <see cref="GameObject"/> with the current <see cref="Transform"/> as its parent.
        /// </summary>
        /// <param name="name">The name of the new <see cref="GameObject"/>.</param>
        /// <param name="localPosition">The local starting position of the new <see cref="GameObject"/>.</param>
        /// <param name="localRotation">The local starting rotation of the new <see cref="GameObject"/>.</param>
        /// <returns>The newly created <see cref="GameObject"/>.</returns>
        public static GameObject AddChildGameObject
            (this Transform transform, string name, Vector3 localPosition, Quaternion localRotation)
        {
            var childObject = new GameObject(name);
            childObject.transform.SetParent(transform);
            childObject.transform.SetLocalPositionAndRotation(localPosition, localRotation);
            return childObject;
        }

        /// <summary>
        /// Destroys all child <see cref="GameObject"/>s of the <see cref="Transform"/>.
        /// </summary>
        public static void DestroyChildren(this Transform transform)
        {
            var children = new List<GameObject>
            {
                Capacity = transform.childCount
            };
            foreach (Transform child in transform)
                children.Add(child.gameObject);
            children.ForEach(child => EditModeSafe.Destroy(child));
        }

        /// <summary>
        /// Returns the normalised direction <see cref="Vector3"/> from this <see cref="Transform"/>
        /// to another.
        /// </summary>
        /// <param name="destination">The destination <see cref="Transform"/>.</param>
        /// <returns>A <see cref="Vector3"/> from the source <see cref="Transform"/> to the destination 
        /// <see cref="Transform"/>.</returns>
        public static Vector3 DirectionTo(this Transform transform, Transform destination)
        {
            return
                Vector3.Normalize(destination.position - transform.position);
        }

        /// <summary>
        /// Returns the normalised direction <see cref="Vector3"/> from this <see cref="Transform"/>
        /// to a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="destination">The destination <see cref="Vector3"/>.</param>
        /// <returns>A <see cref="Vector3"/> from the source <see cref="Transform"/> to the destination 
        /// <see cref="Vector3"/>.</returns>
        public static Vector3 DirectionTo(this Transform transform, Vector3 destination)
        {
            return
                Vector3.Normalize(destination - transform.position);
        }

        /// <summary>
        /// Returns the distance <see cref="float"/> from this <see cref="Transform"/> to another.
        /// </summary>
        /// <param name="destination">The destination <see cref="Transform"/>.</param>
        /// <returns>The <see cref="float"/> distance from the <paramref name="transform"/> to the 
        /// <paramref name="destination"/>.</returns>
        public static float DistanceTo(this Transform transform, Transform destination)
        {
            return
                Vector3.Distance(transform.position, destination.position);
        }

        /// <summary>
        /// Returns the distance <see cref="float"/> from this <see cref="Transform"/> to a 
        /// <see cref="Vector3"/> point.
        /// </summary>
        /// <param name="destination">The destination <see cref="Vector3"/>.</param>
        /// <returns>The <see cref="float"/> distance from the <paramref name="transform"/> to the 
        /// <paramref name="destination"/>.</returns>
        public static float DistanceTo(this Transform transform, Vector3 destination)
        {
            return
                Vector3.Distance(transform.position, destination);
        }

        /// <summary>
        /// Offsets the <see cref="Transform"/>'s position by the values of a <see cref="Vector2"/>.
        /// </summary>
        /// <param name="offset">
        /// The <see cref="Vector2"/> to offset the transform by.
        /// </param>
        public static void OffsetPosition(this Transform transform, Vector2 offset)
        {
            transform.position += (Vector3)offset;
        }

        /// <summary>
        /// Offsets the <see cref="Transform"/>'s position by the values of a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="offset">
        /// The <see cref="Vector3"/> to offset the transform by.
        /// </param>
        public static void OffsetPosition(this Transform transform, Vector3 offset)
        {
            transform.position += offset;
        }

        /// <summary>
        /// Sets the local position and rotation of the <see cref="Transform"/> component.
        /// </summary>
        /// <param name="position">The target <see cref="Vector3"/> position.</param>
        /// <param name="rotation">The target <see cref="Quaternion"/> rotation.</param>
        public static void SetLocalPositionAndRotation
            (this Transform transform, Vector3 position, Quaternion rotation)
        {
            transform.localPosition = position;
            transform.localRotation = rotation;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion        
    }
}
