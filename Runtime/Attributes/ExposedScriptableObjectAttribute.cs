using UnityEngine;

namespace StephanHooft.Attributes
{
    /// <summary>
    /// Attribute for displaying an embedded scriptable object editor in the editor.
    /// </summary>
    public class ExposedScriptableObjectAttribute : PropertyAttribute
    {
        #region Properties

        /// <summary>
        /// Whether to show or hide the <see cref="ScriptableObject"/> while the application is playing.
        /// </summary>
        public bool HideWhilePlaying { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructor

        /// <summary>
        /// Display an embedded scriptable object editor in the editor.
        /// </summary>
        /// <param name="hideWhilePlaying">
        /// Whether to show or hide the <see cref="ScriptableObject"/> while the application is playing.
        /// </param>
        public ExposedScriptableObjectAttribute(bool hideWhilePlaying = false)
        {
            HideWhilePlaying = hideWhilePlaying;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        /// <summary>
        /// Assigns a <see cref="ExposedScriptableObjectAttribute"/>.
        /// </summary>
        public ExposedScriptableObjectAttribute() { }
    }
}
