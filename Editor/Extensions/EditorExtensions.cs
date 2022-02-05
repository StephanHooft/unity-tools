using UnityEditor;

namespace StephanHooft.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Editor"/>.
    /// </summary>
    public static class EditorExtensions
    {
        #region Static Methods

        /// <summary>
        /// Draws an Inspector without the "Script" field.
        /// </summary>
        /// <param name="Inspector">The Inspector to draw.</param>
        /// <returns><see cref="true"/> if a control was changed.</returns>
        public static bool DrawDefaultInspectorWithoutScriptField(this Editor Inspector)
        {
            EditorGUI.BeginChangeCheck();
            Inspector.serializedObject.Update();
            SerializedProperty Iterator = Inspector.serializedObject.GetIterator();
            Iterator.NextVisible(true);
            while (Iterator.NextVisible(false))
                EditorGUILayout.PropertyField(Iterator, true);
            Inspector.serializedObject.ApplyModifiedProperties();
            return
                EditorGUI.EndChangeCheck();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}