using UnityEngine;

namespace StephanHooft.Lines
{
    /// <summary>
    /// A <see cref="MonoBehaviour"/> that implements <see cref="ISegmentedLine2D"/>.
    /// </summary>
    public abstract class SegmentedLine2D : MonoBehaviour, ISegmentedLine2D
    {
        public abstract bool Loop { get; set; }
        public abstract int NodeCount { get; }
        public abstract int SegmentCount { get; }
        public abstract Vector2 this[int index] { get; set; }
        public abstract void AddNode();
        public abstract Vector2 GetDirectionOnLine(float t);
        public abstract Vector2 GetPositionOnLine(float t);
        public abstract Vector2 GetVelocityOnLine(float t);
        public abstract void RemoveNode();
    }
}
