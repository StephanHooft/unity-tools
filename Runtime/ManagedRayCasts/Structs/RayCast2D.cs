using StephanHooft.Extensions;
using UnityEngine;

namespace StephanHooft.ManagedRayCasts
{
    /// <summary>
    /// A helper struct to start (and encapsulate the results of) a 
    /// <see cref="Physics2D.Raycast(Vector2, Vector2, ContactFilter2D, RaycastHit2D[], float)"/> call.
    /// </summary>
    public readonly struct RayCast2D
    {
        #region Properties

        /// <summary>
        /// The <see cref="RayCast2D"/>'s end point.
        /// </summary>
        public Vector2 End => origin + direction.WithMagnitude(distance);

        /// <summary>
        /// Whether or not the <see cref="RayCast2D"/> hit a collider.
        /// </summary>
        public bool HitSomething => nearestHitIndex >= 0;

        /// <summary>
        /// The <see cref="RayCast2D"/>'s nearest collision (if any).
        /// </summary>
        public RaycastHit2D NearestHit => HitSomething ?
            raycastHitBuffer[nearestHitIndex]
            : throw
                new System.InvalidOperationException("No hits.");

        /// <summary>
        /// The length of the distance along the <see cref="RayCast2D"/> that is obscured by colliders, (if any).
        /// </summary>
        public float ObscuredDistance => HitSomething ?
            distance - NearestHit.distance
            : 0f;

        /// <summary>
        /// The length of the distance along the <see cref="RayCast2D"/> that is not obscured by colliders, (if any).
        /// </summary>
        public float UnobscuredDistance => HitSomething ?
            NearestHit.distance
            : distance;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        private readonly Vector2 origin;

        /// <summary>
        /// The <see cref="RayCast2D"/>'s direction.
        /// </summary>
        public readonly Vector2 direction;

        /// <summary>
        /// The <see cref="RayCast2D"/>'s distance.
        /// </summary>
        public readonly float distance;

        private readonly RaycastHit2D[] raycastHitBuffer;
        private readonly int nearestHitIndex;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructors

        /// <summary>
        /// Create a new <see cref="RayCast2D"/>.
        /// </summary>
        /// <param name="origin">The <see cref="Vector2"/> point from which to raycast.</param>
        /// <param name="direction">The direction to raycast in.</param>
        /// <param name="filter">The <see cref="ContactFilter2D"/> to use.</param>
        /// <param name="distance">The distance along which to raycast.</param>
        public RayCast2D(Vector2 origin, Vector2 direction, ContactFilter2D filter, float distance)
        {
            this.origin = origin;
            this.direction = direction.normalized;
            this.distance = distance.MustBeAbove(0f, "distance");
            nearestHitIndex = -1;
            raycastHitBuffer = new RaycastHit2D[4];
            var collisions = Physics2D.Raycast(origin, direction, filter, raycastHitBuffer, distance);
            if (collisions < 4)
                System.Array.Resize(ref raycastHitBuffer, collisions);
            if (collisions > 0)
            {
                nearestHitIndex = 0;
                for (int i = 1; i < collisions; i++)
                    if (raycastHitBuffer[i].distance < raycastHitBuffer[nearestHitIndex].distance)
                        nearestHitIndex = i;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
