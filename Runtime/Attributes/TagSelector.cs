using UnityEngine;

namespace StephanHooft.Attributes
{
    /// <summary>
    /// Class to add a tag selector <see cref="PropertyAttribute"/> to a <see cref="string"/> field.
    /// <remarks><para>Borrowed from: https://github.com/WSWhitehouse/Unity-Tag-Selector .</para></remarks>
    /// </summary>
    public class TagSelectorAttribute : PropertyAttribute
    {
        #region Fields

        /// <summary>
        /// Allows for the default tag field drawer to be used.
        /// </summary>
        public bool UseDefaultTagFieldDrawer = false;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
