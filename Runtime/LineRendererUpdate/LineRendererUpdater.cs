using UnityEngine;

namespace StephanHooft.LineRendererUpdate
{
    /// <summary>
    /// A helper class to force a <see cref="LineRenderer"/>'s positions to update on every frame.
    /// </summary>
    public class LineRendererUpdater : MonoBehaviour
    {
        private ILineRendererUpdateSource source;
        private LineRenderer lineRenderer;
        private bool Set => source != null;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void OnEnable()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if (Set)
            {
                if(lineRenderer.positionCount != source.PositionCount)
                    lineRenderer.positionCount = source.PositionCount;
                lineRenderer.SetPositions(source.GetPositions());
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Set the <see cref="ILineRendererUpdateSource"/> to consult for positions every frame.
        /// </summary>
        /// <param name="source">The <see cref="ILineRendererUpdateSource"/> to set.</param>
        public void SetSource(ILineRendererUpdateSource source)
        {
            this.source = source;
            lineRenderer.SetPositions(source.GetPositions());
        }
    }
}
