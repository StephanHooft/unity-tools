using UnityEngine;

namespace StephanHooft.Attributes
{
    /// <summary>
    /// Allows for an int to be set through a layer selection dropdown in edtitor.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class LayerSelectorAttribute : PropertyAttribute
    {
        /// <summary>
        /// Assigns a <see cref="LayerSelectorAttribute"/>.
        /// </summary>
        public LayerSelectorAttribute() { }
    }
}
