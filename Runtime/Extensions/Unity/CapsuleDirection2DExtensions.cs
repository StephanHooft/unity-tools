using UnityEngine;

namespace StephanHooft.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="CapsuleDirection2D"/>.
    /// </summary>
    public static class CapsuleDirection2DExtensions
    {
        #region Static Methods

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="CapsuleDirection2D"/> equals 
        /// <see cref="CapsuleDirection2D.Vertical"/>.
        /// </summary>
        public static bool IsVertical(this CapsuleDirection2D capsuleDirection)
            => capsuleDirection == CapsuleDirection2D.Vertical;

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="CapsuleDirection2D"/> equals 
        /// <see cref="CapsuleDirection2D.Horizontal"/>.
        /// </summary>
        public static bool IsHorizontal(this CapsuleDirection2D capsuleDirection)
            => capsuleDirection == CapsuleDirection2D.Horizontal;
    
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
