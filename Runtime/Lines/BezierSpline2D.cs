using UnityEngine;

namespace StephanHooft.Lines
{
    /// <summary>
    /// An <see cref="ISegmentedLine2D"/> consisting of one or more cubic Bezier curves.
    /// </summary>
    public class BezierSpline2D : SegmentedLine2D
    {
        #region Fields

        [SerializeField] private bool loop;
        [SerializeField] private BezierControlPoint2D[] points;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region MonoBehaviour Implementation

        public void Reset()
        {
            points = new BezierControlPoint2D[2]
            {
                new BezierControlPoint2D(Vector2.zero, Vector2.left, Vector2.right, BezierControlPointMode.Aligned),
                new BezierControlPoint2D(new Vector2(3, 0), new Vector2(2, 0), new Vector2(4, 0), BezierControlPointMode.Aligned)
            };
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region ISegmentedLine2D Implementation

        #region Properties

        public override bool Loop
        {
            get => loop;
            set => loop = value;
        }

        public override int NodeCount => points.Length;

        public override int SegmentCount => points.Length - (loop ? 0 : 1);

        public override Vector2 this[int index]
        {
            get => GetControlPoint(0, index);
            set => SetControlPoint(0, index, value);
        }

        #endregion
        #region Methods

        public override Vector2 GetPositionOnLine(float t)
        {
            int i;
            if (t >= 1f)
            {
                t = 1f;
                i = SegmentCount - 1;
            }
            else
            {
                t = Mathf.Clamp01(t) * SegmentCount;
                i = (int)t;
                t -= i;
            }
            return
                transform.TransformPoint(BezierMath.GetPoint(
                    points[i].GetPosition(0),
                    points[i].GetPosition(2),
                    points[loop && i == NodeCount - 1 ? 0 : i + 1].GetPosition(1),
                    points[loop && i == NodeCount - 1 ? 0 : i + 1].GetPosition(0), t));
        }

        public override Vector2 GetDirectionOnLine(float t)
        {
            return
                GetVelocityOnLine(t).normalized;
        }

        public override Vector2 GetVelocityOnLine(float t)
        {
            int i;
            if (t >= 1f)
            {
                t = 1f;
                i = SegmentCount - 1;
            }
            else
            {
                t = Mathf.Clamp01(t) * SegmentCount;
                i = (int)t;
                t -= i;
            }
            return transform.TransformPoint(BezierMath.GetFirstDerivative(
                points[i].GetPosition(0),
                points[i].GetPosition(2),
                points[loop && i == NodeCount - 1 ? 0 : i + 1].GetPosition(1),
                points[loop && i == NodeCount - 1 ? 0 : i + 1].GetPosition(0), t))
                - transform.position;
        }

        public override void AddNode()
        {
            var point = points[points.Length - 1].GetPosition(2);
            var direction = GetDirectionOnLine(1f);
            BezierControlPoint2D node = new BezierControlPoint2D(
                point + (direction * 2),
                point + (direction * 1),
                point + (direction * 3),
                points[points.Length - 1].ControlPointMode);
            System.Array.Resize(ref points, points.Length + 1);
            points[points.Length - 1] = node;
        }

        public override void RemoveNode()
        {
            if (NodeCount > 2)
                System.Array.Resize(ref points, points.Length - 1);
            else
                Debug.LogWarning("Removing the last 2 spline nodes is not permitted.");
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// Gets the control point mode of one of the <see cref="BezierSpline2D"/>'s nodes.
        /// </summary>
        /// <param name="nodeIndex">The index of the node to check.</param>
        /// <returns>A <see cref="BezierControlPointMode"/> value.</returns>
        public BezierControlPointMode GetControlPointMode(int nodeIndex)
        {
            if (nodeIndex < 0 || nodeIndex > points.Length - 1)
                throw
                    new System.ArgumentOutOfRangeException("nodeIndex");
            return
                points[nodeIndex].ControlPointMode;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Sets the control point mode of one of the <see cref="BezierSpline2D"/>'s nodes.
        /// </summary>
        /// <param name="nodeIndex">The index of the node to set.</param>
        /// <param name="mode">The <see cref="BezierControlPointMode"/> to set.</param>
        public void SetControlPointMode(int nodeIndex, BezierControlPointMode mode)
        {
            if (nodeIndex < 0 || nodeIndex > points.Length - 1)
                throw
                    new System.ArgumentOutOfRangeException("nodeIndex");
            points[nodeIndex].ControlPointMode = mode;
        }

        /// <summary>
        /// Gets the position of one of the <see cref="BezierSpline2D"/>'s control points.
        /// </summary>
        /// <param name="nodeIndex">The index of the <see cref="BezierControlPoint2D"/> to get a position from.</param>
        /// <param name="pointIndex">The index of the point to retrieve.
        /// <para>0 for the main node. 1 for the first control point. 2 for the second control point.</para></param>
        /// <returns>A <see cref="Vector2"/> position.</returns>
        public Vector2 GetControlPoint(int nodeIndex, int pointIndex)
        {
            if (nodeIndex < 0 || nodeIndex > points.Length - 1)
                throw
                    new System.ArgumentOutOfRangeException("nodeIndex");
            if (pointIndex < 0 || pointIndex > 2)
                throw
                    new System.ArgumentOutOfRangeException("pointIndex");
            return
                points[nodeIndex].GetPosition(pointIndex);
        }

        /// <summary>
        /// Sets the position of one of the <see cref="BezierSpline2D"/>'s control points.
        /// </summary>
        /// <param name="nodeIndex">The index of the <see cref="BezierControlPoint2D"/> to set a position from.</param>
        /// <param name="pointIndex">The index of the point to set.
        /// <para>0 for the main node. 1 for the first control point. 2 for the second control point.</param>
        /// <param name="position">The position to set.</param>
        public void SetControlPoint(int nodeIndex, int pointIndex, Vector2 position)
        {
            if (nodeIndex < 0 || nodeIndex > points.Length - 1)
                throw
                    new System.ArgumentOutOfRangeException("nodeIndex");
            if (pointIndex < 0 || pointIndex > 2)
                throw
                    new System.ArgumentOutOfRangeException("pointIndex");
            points[nodeIndex].SetPosition(pointIndex, position);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
