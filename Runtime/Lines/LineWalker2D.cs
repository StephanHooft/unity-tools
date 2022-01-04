using StephanHooft.HybridUpdate;
using UnityEngine;

namespace StephanHooft.Lines
{
    /// <summary>
    /// Class that allows a <see cref="GameObject"/> to move along an <see cref="ISegmentedLine2D"/>.
    /// </summary>
    public class LineWalker2D : MonoBehaviour
    {
        public LineWalkerMode mode;
        public SegmentedLine2D line;
        public float duration;

        private float progress;
        private bool goingForward = true;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void OnEnable()
        {
            HybridUpdateManager.RegisterUpdateCallback(HybridUpdate, -1);
        }

        private void HybridUpdate(float deltaTime)
        {
            if (goingForward)
            {
                progress += Time.deltaTime / duration;
                if (progress > 1f)
                {
                    if (mode == LineWalkerMode.Once)
                        progress = 1f;
                    else if (mode == LineWalkerMode.Loop)
                        progress -= 1f;
                    else
                    {
                        progress = 2f - progress;
                        goingForward = false;
                    }
                }
            }
            else
            {
                progress -= Time.deltaTime / duration;
                if (progress < 0f)
                {
                    progress = -progress;
                    goingForward = true;
                }
            }
            Vector2 position = line.GetPoint(progress);
            transform.localPosition = position;
        }

        private void OnDisable()
        {
            HybridUpdateManager.UnregisterUpdateCallback(HybridUpdate);
        }
    }
}
