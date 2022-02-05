using System;
using UnityEngine;

namespace StephanHooft.Lines
{
    /// <summary>
    /// A line consisting of one-or-more straight segments between its control points.
    /// </summary>
    public class MultiLine2D : SegmentedLine2D
    {
        #region Fields

        [SerializeField] private bool loop;
        [SerializeField] private Vector2[] points;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region MonoBehaviour Implementation

        public void Reset()
        {
            points = new Vector2[2] { Vector2.zero, Vector2.right };
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
            get
            {
                if (index < 0 || index > points.Length - 1)
                    throw
                        new ArgumentOutOfRangeException("nodeIndex");
                return points[index];
            }
            set
            {
                if (index < 0 || index > points.Length - 1)
                    throw
                        new ArgumentOutOfRangeException("nodeIndex");
                points[index] = value;
            }
        }
        #endregion
        #region Methods

        public override void AddNode()
        {
            var point = points[points.Length - 1];
            var direction = GetDirectionOnLine(1f);
            var node = point + direction;
            Array.Resize(ref points, points.Length + 1);
            points[points.Length - 1] = node;
        }

        public override Vector2 GetDirectionOnLine(float t)
        {
            return GetVelocityOnLine(t).normalized;
        }

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
            return transform.TransformPoint(
                Vector2.Lerp(
                    points[i],
                    points[loop && i == NodeCount - 1 ? 0 : i + 1],
                    t)
                );
        }

        public override Vector2 GetVelocityOnLine(float t)
        {
            int i;
            if (t >= 1f)
            {
                i = SegmentCount - 1;
            }
            else
            {
                t = Mathf.Clamp01(t) * SegmentCount;
                i = (int)t;
            }
            return points[loop && i == NodeCount - 1 ? 0 : i + 1] - points[i];
        }

        public override void RemoveNode()
        {
            if (NodeCount > 2)
                Array.Resize(ref points, points.Length - 1);
            else
                Debug.LogWarning("Removing the last 2 spline nodes is not permitted.");
        }
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
