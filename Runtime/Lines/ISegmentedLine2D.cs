using UnityEngine;

namespace StephanHooft.Lines
{
    /// <summary>
    /// An interface for segmented lines in twodimensional space.
    /// </summary>
    public interface ISegmentedLine2D
    {
        /// <summary>
        /// The amount of points on the segmented line.
        /// </summary>
        public int PointCount { get; }

        /// <summary>
        /// The amount of total line segments.
        /// </summary>
        public int SegmentCount { get; }

        /// <summary>
        /// Whether or not the segmented line loops around.
        /// </summary>
        public bool Loop { get; set; }

        /// <summary>
        /// Get a point on the line at value <paramref name="t"/> (0f - 1f).
        /// </summary>
        /// <param name="t">The distance along the line at which to get a point.</param>
        /// <returns>A <see cref="Vector2"/> position (x,y).</returns>
        public Vector2 GetPoint(float t);

        /// <summary>
        /// Get a direction along the line at value <paramref name="t"/> (0f - 1f).
        /// </summary>
        /// <param name="t"></param>
        /// <returns>A <see cref="Vector2"/> direction (x,y).</returns>
        public Vector2 GetDirection(float t);

        /// <summary>
        /// Get a velocity value along the line at value <paramref name="t"/> (0f - 1f).
        /// </summary>
        /// <param name="t"></param>
        /// <returns>A <see cref="Vector2"/> velocity (x,y) value.</returns>
        public Vector2 GetVelocity(float t);

        /// <summary>
        /// Add a node (and thus a line segment) to the segmented line.
        /// </summary>
        public void AddNode();

        /// <summary>
        /// Remove a node (and thus a line segment) from the segmented line.
        /// </summary>
        public void RemoveNode();
    }
}
