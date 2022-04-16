using UnityEditor;
using UnityEngine;

namespace StephanHooft.Attributes.EditorScripts
{
    /// <summary>
    /// A custom <see cref="PropertyDrawer"/> for the <see cref="InspectorLabelAttribute"/>.
    /// </summary>
    [CustomPropertyDrawer(typeof(InspectorLabelAttribute))]
    public class InspectorLabelDrawer : PropertyDrawer
    {
        #region PropertyDrawer Implementation

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            InspectorLabelAttribute[] overriddenLabel = (InspectorLabelAttribute[])fieldInfo.GetCustomAttributes(typeof(InspectorLabelAttribute), true);
            var labelText = overriddenLabel[0] != null ? new GUIContent(overriddenLabel[0].Label) : label;
            EditorGUI.PropertyField(position, property, labelText);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
