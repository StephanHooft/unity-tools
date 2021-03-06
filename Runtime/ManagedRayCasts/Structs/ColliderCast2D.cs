using StephanHooft.Extensions;
using UnityEngine;

namespace StephanHooft.ManagedRayCasts
{
    /// <summary>
    /// A helper struct to start (and encapsulate the results of) a 
    /// <see cref="Collider2D.Cast(Vector2, ContactFilter2D, RaycastHit2D[], float)"/> call.
    /// </summary>
    public readonly struct ColliderCast2D
    {
        #region Properties

        /// <summary>
        /// Whether or not the <see cref="ColliderCast2D"/> hit a collider.
        /// </summary>
        public bool HitSomething => nearestHitIndex >= 0;

        /// <summary>
        /// The <see cref="ColliderCast2D"/>'s nearest collision (if any).
        /// </summary>
        public RaycastHit2D NearestHit => HitSomething ?
            raycastHitBuffer[nearestHitIndex]
            : throw
                new System.InvalidOperationException("No hits.");

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        /// <summary>
        /// The <see cref="ColliderCast2D"/>'s direction.
        /// </summary>
        public readonly Vector2 direction;

        /// <summary>
        /// The <see cref="ColliderCast2D"/>'s distance.
        /// </summary>
        public readonly float distance;

        private readonly RaycastHit2D[] raycastHitBuffer;
        private readonly int nearestHitIndex;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructors

        /// <summary>
        /// Create a new <see cref="ColliderCast2D"/>.
        /// </summary>
        /// <param name="collider">The <see cref="Collider2D"/> to cast.</param>
        /// <param name="direction">The direction in which to cast the <see cref="Collider2D"/>.</param>
        /// <param name="contactFilter">The <see cref="ContactFilter2D"/> to use.</param>
        /// <param name="distance">The distance along which to cast.</param>
        public ColliderCast2D(Collider2D collider, Vector2 direction, ContactFilter2D contactFilter, float distance)
        {
            this.direction = direction.normalized;
            this.distance = distance.MustBeAbove(0f, "distance");
            nearestHitIndex = -1;
            raycastHitBuffer = new RaycastHit2D[4];
            var collisions = collider.Cast(direction, contactFilter, raycastHitBuffer, distance);
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
