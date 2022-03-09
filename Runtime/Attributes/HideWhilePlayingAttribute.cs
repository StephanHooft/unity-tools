using UnityEngine;

namespace StephanHooft.Attributes
{
    /// <summary>
    /// Makes a field not show up in the inspector while the application is playing.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class HideWhilePlayingAttribute : PropertyAttribute
    {
        /// <summary>
        /// Assigns a <see cref="HideWhilePlayingAttribute"/>.
        /// </summary>
        public HideWhilePlayingAttribute() { }
    }
}
