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
        #region Fields

        private Editor editor = null;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region PropertyDrawer Implementation
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var attribute = (ExposedScriptableObjectAttribute[])
                fieldInfo.GetCustomAttributes(typeof(ExposedScriptableObjectAttribute), true);
            if (!Application.isPlaying || !attribute[0].HideWhilePlaying)
                return
                    base.GetPropertyHeight(property, label);
            else
                return
                    -EditorGUIUtility.standardVerticalSpacing;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attribute = (ExposedScriptableObjectAttribute[])
                fieldInfo.GetCustomAttributes(typeof(ExposedScriptableObjectAttribute), true);
            if (!Application.isPlaying || !attribute[0].HideWhilePlaying)
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
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
