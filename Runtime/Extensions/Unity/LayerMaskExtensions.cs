using UnityEngine;

namespace StephanHooft.Extensions
{
    public static class LayerMaskExtensions
    {
        /// <summary>
        /// Checks whether a <see cref="GameObject"/>'s layer is included in the <see cref="LayerMask"/>.
        /// </summary>
        /// <param name="gameObject">The <see cref="GameObject"/> to check against.</param>
        /// <returns><see cref="true"/> if the <see cref="LayerMask"/> contains the <see cref="GameObject"/>'s
        /// layer.</returns>
        public static bool Contains(this LayerMask mask, GameObject gameObject)
        {
            var objLayerMask = 1 << gameObject.layer;
            return
                (mask.value & objLayerMask) > 0;
        }

        /// <summary>
        /// Checks whether a <see cref="Component"/>'s layer is included in the <see cref="LayerMask"/>.
        /// </summary>
        /// <param name="component">The <see cref="Component"/> to check against.</param>
        /// <returns><see cref="true"/> if the <see cref="LayerMask"/> contains the <see cref="Component"/>'s
        /// layer.</returns>
        public static bool Contains(this LayerMask mask, Component component)
        {
            var objLayerMask = 1 << component.gameObject.layer;
            return
                (mask.value & objLayerMask) > 0;
        }

        /// <summary>
        /// Checks whether an <see cref="int"/> layer is included in the <see cref="LayerMask"/>.
        /// </summary>
        /// <param name="layer">The layer <see cref="int"/> to check against.</param>
        /// <returns><see cref="true"/> if the <see cref="LayerMask"/> contains the <see cref="int"/>
        /// layer.</returns>
        public static bool Contains(this LayerMask mask, int layer)
        {
            var objLayerMask = 1 << layer;
            return
                (mask.value & objLayerMask) > 0;
        }

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="LayerMask"/> is empty.
        /// </summary>
        /// <returns><see cref="true"/> if the <see cref="LayerMask"/> has zero layers included.</returns>
        public static bool IsEmpty(this LayerMask mask)
        {
            return mask.value == 0;
        }
    }
}
