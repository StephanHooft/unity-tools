using UnityEditor;

namespace StephanHooft.CustomEditorScripts
{
    public static class InspectorTools
    {
        /// <summary>
        /// Draws a toggle based on a <see cref="bool"/> <paramref name="field"/> value and returns whether its value
        /// was changed. The <paramref name="modified"/> out <see cref="bool"/> can be used to set the result to the
        /// original field.
        /// </summary>
        /// <param name="label">The label of the toggle to make.</param>
        /// <param name="field">The field value to set to the toggle.</param>
        /// <param name="modified">The modified toggle field value.</param>
        /// <returns><see cref="true"/> if the toggle value was modified.</returns>
        public static bool DrawBoolFieldToggle(string label, bool field, out bool modified)
        {
            modified = field;
            if (EditorGUILayout.Toggle(label, field) != field)
                modified = !field;
            return field != modified;
        }
    }
}
