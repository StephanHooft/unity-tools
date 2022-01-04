using UnityEngine;

namespace StephanHooft.Lines
{
    /// <summary>
    /// A <see cref="MonoBehaviour"/> that implements <see cref="ISegmentedLine2D"/>.
    /// </summary>
    public abstract class SegmentedLine2D : MonoBehaviour, ISegmentedLine2D
    {
        public abstract int PointCount { get; }
        public abstract int SegmentCount { get; }
        public abstract bool Loop { get; set; }
        public abstract void AddNode();
        public abstract Vector2 GetDirection(float t);
        public abstract Vector2 GetPoint(float t);
        public abstract Vector2 GetVelocity(float t);
        public abstract void RemoveNode();
    }
}
