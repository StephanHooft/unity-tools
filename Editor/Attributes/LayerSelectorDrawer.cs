using UnityEngine;
using UnityEditor;

namespace StephanHooft.Attributes.EditorScripts
{
    /// <summary>
    /// A custom <see cref="PropertyDrawer"/> for the <see cref="LayerSelectorAttribute"/>.
    /// </summary>
    [CustomPropertyDrawer(typeof(LayerSelectorAttribute))]
    public class LayerSelectorDrawer : PropertyDrawer
    {
        #region PropertyDrawer Implementation

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            if (property.propertyType == SerializedPropertyType.Integer)
                property.intValue = EditorGUI.LayerField(position, label, property.intValue);
            else
                EditorGUI.PropertyField(position, property, label);
            EditorGUI.EndProperty();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
