using UnityEngine;

namespace StephanHooft.Lines
{
    /// <summary>
    /// An interface for segmented lines in twodimensional space.
    /// </summary>
    public interface ISegmentedLine2D
    {
        #region Interface Properties

        /// <summary>
        /// Whether or not the <see cref="ISegmentedLine2D"/> loops around.
        /// </summary>
        public bool Loop { get; set; }

        /// <summary>
        /// The amount of points on the <see cref="ISegmentedLine2D"/>.
        /// </summary>
        public int NodeCount { get; }

        /// <summary>
        /// The amount of total line segments.
        /// </summary>
        public int SegmentCount { get; }

        /// <summary>
        /// Gets/Sets one of the <see cref="ISegmentedLine2D"/>'s nodes.
        /// </summary>
        /// <param name="index">The <see cref="int "/> index of the node to set.</param>
        public Vector2 this[int index] { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Interface Methods

        /// <summary>
        /// Add a node (and thus a line segment) to the <see cref="ISegmentedLine2D"/>.
        /// </summary>
        public void AddNode();

        /// <summary>
        /// Get a point on the <see cref="ISegmentedLine2D"/> at value <paramref name="t"/> (0f - 1f).
        /// </summary>
        /// <param name="t">The distance along the line at which to get a point.</param>
        /// <returns>A <see cref="Vector2"/> position (x,y).</returns>
        public Vector2 GetPositionOnLine(float t);

        /// <summary>
        /// Get a direction along the <see cref="ISegmentedLine2D"/> at value <paramref name="t"/> (0f - 1f).
        /// </summary>
        /// <param name="t"></param>
        /// <returns>A <see cref="Vector2"/> direction (x,y).</returns>
        public Vector2 GetDirectionOnLine(float t);

        /// <summary>
        /// Get a velocity value along the <see cref="ISegmentedLine2D"/> at value <paramref name="t"/> (0f - 1f).
        /// </summary>
        /// <param name="t"></param>
        /// <returns>A <see cref="Vector2"/> velocity (x,y) value.</returns>
        public Vector2 GetVelocityOnLine(float t);

        /// <summary>
        /// Remove a node (and thus a line segment) from the <see cref="ISegmentedLine2D"/>.
        /// </summary>
        public void RemoveNode();

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
