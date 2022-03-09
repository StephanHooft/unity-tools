using UnityEditor;
using UnityEngine;

namespace StephanHooft.Attributes.EditorScripts
{
    /// <summary>
    /// A <see cref="CustomPropertyDrawer"/> for the <see cref="InspectorLabel"/>.
    /// </summary>
    [CustomPropertyDrawer(typeof(InspectorLabel))]
    public class InspectorLabelDrawer : PropertyDrawer
    {
        #region PropertyDrawer Implementation

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            InspectorLabel[] overriddenLabel = (InspectorLabel[])fieldInfo.GetCustomAttributes(typeof(InspectorLabel), true);
            var labelText = overriddenLabel[0] != null ? new GUIContent(overriddenLabel[0].Label) : label;
            EditorGUI.PropertyField(position, property, labelText);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
