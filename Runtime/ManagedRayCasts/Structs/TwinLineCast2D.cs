using UnityEngine;

namespace StephanHooft.ManagedRayCasts
{
    /// <summary>
    /// A helper struct to start (and encapsulate the results of) an opposite pair of 
    /// <see cref="Physics2D.Linecast(Vector2, Vector2, ContactFilter2D, RaycastHit2D[])"/> calls between a start- and an endpoint.
    /// </summary>
    public readonly struct TwinLineCast2D
    {
        #region Properties

        /// <summary>
        /// The <see cref="TwinLineCast2D"/>'s distance.
        /// </summary>
        public float Distance => (end - start).magnitude;

        /// <summary>
        /// The <see cref="TwinLineCast2D"/>'s forward direction (from start to end).
        /// </summary>
        public Vector2 ForwardDirection => (end - start).normalized;

        /// <summary>
        /// Whether or not the <see cref="TwinLineCast2D"/> hit a collider.
        /// </summary>
        public bool HitSomething => (nearestHitIndex[0] >= 0) && (nearestHitIndex[1] >= 0);

        /// <summary>
        /// The <see cref="TwinLineCast2D"/>'s nearest collision (if any) from the direction of the start point.
        /// </summary>
        public RaycastHit2D NearestForwardHit => nearestHitIndex[0] >= 0
            ? raycastHitBuffer[0][nearestHitIndex[0]]
            : throw
                new System.InvalidOperationException("No hits.");

        /// <summary>
        /// The <see cref="TwinLineCast2D"/>'s nearest collision (if any) from the direction of the end point.
        /// </summary>
        public RaycastHit2D NearestReverseHit => nearestHitIndex[1] >= 0
            ? raycastHitBuffer[1][nearestHitIndex[1]]
            : throw
                new System.InvalidOperationException("No hits.");

        /// <summary>
        /// The length of the distance along the <see cref="TwinLineCast2D"/> that is obscured by colliders, (if any).
        /// </summary>
        public float ObscuredDistance => HitSomething
            ? Distance - (NearestForwardHit.distance + NearestReverseHit.distance)
            : 0f;

        /// <summary>
        /// The <see cref="TwinLineCast2D"/>'s reverse direction (from end to start).
        /// </summary>
        public Vector2 ReverseDirection => (start - end).normalized;

        /// <summary>
        /// The length of the distance along the <see cref="TwinLineCast2D"/> that is not obscured by colliders, (if any).
        /// </summary>
        public float UnobscuredDistance => HitSomething
            ? NearestForwardHit.distance + NearestReverseHit.distance
            : Distance;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        /// <summary>
        /// The <see cref="TwinLineCast2D"/>'s end point.
        /// </summary>
        public readonly Vector2 end;

        /// <summary>
        /// The <see cref="TwinLineCast2D"/>'s starting point.
        /// </summary>
        public readonly Vector2 start;

        private readonly RaycastHit2D[][] raycastHitBuffer;
        private readonly int[] nearestHitIndex;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructors

        /// <summary>
        /// Create a new <see cref="TwinLineCast2D"/>.
        /// </summary>
        /// <param name="start">A <see cref="Vector2"/> starting point.</param>
        /// <param name="end">A <see cref="Vector2"/> end point.</param>
        /// <param name="filter">The <see cref="ContactFilter2D"/> to use.</param>
        public TwinLineCast2D(Vector2 start, Vector2 end, ContactFilter2D filter)
        {
            this.start = start;
            this.end = end;
            nearestHitIndex = new int[2] { -1, -1 };
            raycastHitBuffer = new RaycastHit2D[2][]
            {
                new RaycastHit2D[4],
                new RaycastHit2D[4]
            };
            var collisions = new int[2]
            {
                Physics2D.Linecast(start, end, filter, raycastHitBuffer[0]),
                Physics2D.Linecast(end, start, filter, raycastHitBuffer[1])
            };
            ProcessRaycastHitBuffers(ref collisions);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        private void ProcessRaycastHitBuffers(ref int[] collisions)
        {
            for (int i = 0; i < collisions.Length; i++)
            {
                if (collisions[i] < 4)
                    System.Array.Resize(ref raycastHitBuffer[i], collisions[i]);
                if (collisions[i] > 0)
                {
                    nearestHitIndex[i] = 0;
                    for (int j = 1; j < collisions[i]; j++)
                        if (raycastHitBuffer[i][j].distance < raycastHitBuffer[i][nearestHitIndex[i]].distance)
                            nearestHitIndex[i] = j;
                }
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}