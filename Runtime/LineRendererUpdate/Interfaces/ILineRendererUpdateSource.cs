using UnityEngine;

namespace StephanHooft.LineRendererUpdate
{
    /// <summary>
    /// Interface for classes that might provide a <see cref="LineRendererUpdater"/> with data.
    /// </summary>
    public interface ILineRendererUpdateSource
    {
        /// <summary>
        /// The intended <see cref="int"/> number of positions for the <see cref="LineRenderer"/>.
        /// </summary>
        public int PositionCount { get; }

        /// <summary>
        /// Retrieve the <see cref="Vector3"/> positions to set to the <see cref="LineRenderer"/>.
        /// </summary>
        public Vector3[] GetPositions();
    }
}
