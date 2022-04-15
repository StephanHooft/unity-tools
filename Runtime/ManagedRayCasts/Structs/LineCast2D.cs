using UnityEngine;

namespace StephanHooft.ManagedRayCasts
{
    /// <summary>
    /// A helper struct to start (and encapsulate the results of) a 
    /// <see cref="Physics2D.Linecast(Vector2, Vector2, ContactFilter2D, RaycastHit2D[])"/> call.
    /// </summary>
    public readonly struct LineCast2D
    {
        #region Properties

        /// <summary>
        /// The <see cref="LineCast2D"/>'s direction.
        /// </summary>
        public Vector2 Direction => (end - start).normalized;

        /// <summary>
        /// The <see cref="LineCast2D"/>'s distance.
        /// </summary>
        public float Distance => (end - start).magnitude;

        /// <summary>
        /// Whether or not the <see cref="LineCast2D"/> hit a collider.
        /// </summary>
        public bool HitSomething => nearestHitIndex >= 0;

        /// <summary>
        /// The <see cref="LineCast2D"/>'s nearest collision (if any).
        /// </summary>
        public RaycastHit2D NearestHit => HitSomething ?
            raycastHitBuffer[nearestHitIndex]
            : throw
                new System.InvalidOperationException("No hits.");

        /// <summary>
        /// The length of the distance along the <see cref="LineCast2D"/> that is obscured by colliders, (if any).
        /// </summary>
        public float ObscuredDistance => HitSomething ?
            Distance - NearestHit.distance
            : 0f;

        /// <summary>
        /// The length of the distance along the <see cref="LineCast2D"/> that is not obscured by colliders, (if any).
        /// </summary>
        public float UnobscuredDistance => HitSomething ?
            NearestHit.distance
            : Distance;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        /// <summary>
        /// The <see cref="LineCast2D"/>'s end point.
        /// </summary>
        public readonly Vector2 end;

        /// <summary>
        /// The <see cref="LineCast2D"/>'s starting point.
        /// </summary>
        public readonly Vector2 start;

        private readonly RaycastHit2D[] raycastHitBuffer;
        private readonly int nearestHitIndex;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructors

        /// <summary>
        /// Create a new <see cref="LineCast2D"/>.
        /// </summary>
        /// <param name="start">A <see cref="Vector2"/> starting point.</param>
        /// <param name="end">A <see cref="Vector2"/> end point.</param>
        /// <param name="filter">The <see cref="ContactFilter2D"/> to use.</param>
        public LineCast2D(Vector2 start, Vector2 end, ContactFilter2D filter)
        {
            this.start = start;
            this.end = end;
            nearestHitIndex = -1;
            raycastHitBuffer = new RaycastHit2D[4];
            var collisions = Physics2D.Linecast(start, end, filter, raycastHitBuffer);
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
