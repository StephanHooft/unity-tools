using UnityEngine;

namespace StephanHooft.Lines
{
    /// <summary>
    /// A control node for a <see cref="BezierSpline2D"/> with one main position and two control points.
    /// </summary>
    [System.Serializable]
    public class BezierControlPoint2D
    {
        #region Properties

        /// <summary>
        /// The control point mode enforced by the node.
        /// </summary>
        public BezierControlPointMode ControlPointMode
        {
            get => mode;
            set
            {
                mode = value;
                EnforceMode(2, positions[2]);
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        [SerializeField] private BezierControlPointMode mode;
        [SerializeField] private Vector2[] positions;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructors

        /// <summary>
        /// Create a new <see cref="BezierControlPoint2D"/>.
        /// </summary>
        /// <param name="nodePosition">Position for the main node.</param>
        /// <param name="preControlPointPosition">Position for the first control point. (Before the node.)</param>
        /// <param name="postControlPointPosition">Position for the second control point. (After the node.)</param>
        /// <param name="mode">The <see cref="BezierControlPointMode"/> to enforce.</param>
        public BezierControlPoint2D(Vector2 nodePosition, Vector2 preControlPointPosition, Vector2 postControlPointPosition, BezierControlPointMode mode)
        {
            this.mode = mode;
            positions = new Vector2[3] { nodePosition, preControlPointPosition, postControlPointPosition };
            EnforceMode(2, positions[2]);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// Get the position of one of the <see cref="BezierControlPoint2D"/>'s points.
        /// </summary>
        /// <param name="pointIndex">The index (0-2) of the point to get.</param>
        /// <returns>A <see cref="Vector2"/> position.</returns>
        public Vector2 GetPosition(int pointIndex)
        {
            if (pointIndex < 0 || pointIndex > 2)
                throw
                    new System.ArgumentOutOfRangeException("index");
            return
                positions[pointIndex];
        }

        /// <summary>
        /// Set the position of one of the <see cref="BezierControlPoint2D"/>'s points.
        /// <para>Use 0 for the main node, 1 for the first control point, and 2 for the second control point.</para>
        /// </summary>
        /// <param name="pointIndex">The index (0-2) of the point to get.</param>
        /// <param name="position">The position to set.</param>
        public void SetPosition(int pointIndex, Vector2 position)
        {
            if (pointIndex < 0 || pointIndex > 2)
                throw
                    new System.ArgumentOutOfRangeException("index");
            if (pointIndex == 0)
            {
                Vector2 delta = position - positions[pointIndex];
                positions[1] += delta;
                positions[2] += delta;
            }
            else
                EnforceMode(pointIndex, position);
            positions[pointIndex] = position;
        }

        private void EnforceMode(int pointIndex, Vector2 position)
        {
            Vector2 offset = position - positions[0];
            if (mode == BezierControlPointMode.Mirrored)
                positions[pointIndex == 1 ? 2 : 1] = positions[0] - offset;
            else if (mode == BezierControlPointMode.Aligned)
            {
                var distance = Vector2.Distance(positions[0], positions[pointIndex == 1 ? 2 : 1]);
                positions[pointIndex == 1 ? 2 : 1] = positions[0] - offset.normalized * distance;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
