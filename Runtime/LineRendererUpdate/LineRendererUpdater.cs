using UnityEngine;

namespace StephanHooft.LineRendererUpdate
{
    /// <summary>
    /// A helper class to force a <see cref="LineRenderer"/>'s positions to update on every frame.
    /// </summary>
	[RequireComponent(typeof(LineRenderer))]
    public class LineRendererUpdater : MonoBehaviour
    {
        #region Properties

        private bool Set => source != null;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        private ILineRendererUpdateSource source;
        private LineRenderer lineRenderer;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region MonoBehaviour Implementation

        private void OnEnable()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if (Set)
            {
                if (lineRenderer.positionCount != source.PositionCount)
                    lineRenderer.positionCount = source.PositionCount;
                lineRenderer.SetPositions(source.GetPositions());
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// Set the <see cref="ILineRendererUpdateSource"/> to consult for positions every frame.
        /// </summary>
        /// <param name="source">The <see cref="ILineRendererUpdateSource"/> to set.</param>
        public void SetSource(ILineRendererUpdateSource source)
        {
            this.source = source;
            Update();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
