using UnityEditor;
using UnityEngine;

namespace StephanHooft.Attributes.EditorScripts
{
    /// <summary>
    /// A custom <see cref="PropertyDrawer"/> for the <see cref="ExposedScriptableObjectAttribute"/>.
    /// </summary>
    [CustomPropertyDrawer(typeof(ExposedScriptableObjectAttribute))]
    public class ExposedScriptableObjectDrawer : PropertyDrawer
    {
        private Editor editor = null;

        #region PropertyDrawer Implementation

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);
            if (property.objectReferenceValue != null)
                property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);
            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;
                if (!editor)
                    Editor.CreateCachedEditor(property.objectReferenceValue, null, ref editor);
                editor.OnInspectorGUI();
                EditorGUI.indentLevel--;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
