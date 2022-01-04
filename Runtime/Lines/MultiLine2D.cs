using System;
using UnityEngine;

namespace StephanHooft.Lines
{
    /// <summary>
    /// A line consisting of one-or-more straight segments between its control points.
    /// </summary>
    public class MultiLine2D : SegmentedLine2D
    {
        public override int PointCount => points.Length;

        public override int SegmentCount => points.Length - (loop ? 0 : 1);

        public override bool Loop
        {
            get { return loop; }
            set { loop = value; }
        }

        [SerializeField] private bool loop;
        [SerializeField] private Vector2[] points;

        public override void AddNode()
        {
            Vector2 point = points[points.Length - 1];
            Vector2 direction = GetDirection(1f);

            Vector2 node = point + direction;
            Array.Resize(ref points, points.Length + 1);
            points[points.Length - 1] = node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeIndex"></param>
        /// <returns></returns>
        public Vector2 GetControlPoint(int nodeIndex)
        {
            if (nodeIndex < 0 || nodeIndex > points.Length - 1)
                throw new ArgumentOutOfRangeException("nodeIndex");
            return points[nodeIndex];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeIndex"></param>
        /// <param name="position"></param>
        public void SetControlPoint(int nodeIndex, Vector2 position)
        {
            if (nodeIndex < 0 || nodeIndex > points.Length - 1)
                throw new ArgumentOutOfRangeException("nodeIndex");
            points[nodeIndex] = position;
        }

        public override Vector2 GetDirection(float t)
        {
            return GetVelocity(t).normalized;
        }

        public override Vector2 GetPoint(float t)
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
                    points[loop && i == PointCount - 1 ? 0 : i + 1], 
                    t)
                );
        }

        public override Vector2 GetVelocity(float t)
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
            return points[loop && i == PointCount - 1 ? 0 : i + 1] - points[i];
        }

        public override void RemoveNode()
        {
            if (PointCount > 2)
                Array.Resize(ref points, points.Length - 1);
            else
                Debug.LogWarning("Removing the last 2 spline nodes is not permitted.");
        }

        public void Reset()
        {
            points = new Vector2[2] { Vector2.zero, Vector2.right };
        }
    }
}
