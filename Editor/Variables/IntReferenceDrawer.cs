using UnityEditor;
using UnityEngine;

namespace StephanHooft.Variables.EditorScripts
{
    /// <summary>
    /// A custom <see cref="PropertyDrawer"/> for the <see cref="IntReference"/>.
    /// </summary>
    [CustomPropertyDrawer(typeof(IntReference))]
    public class IntReferenceDrawer : PropertyDrawer
    {
        private readonly string[] popupOptions =
            { "Use Local Value", "Use IntVariable" };

        private GUIStyle popupStyle;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (popupStyle == null)
            {
                popupStyle = new(GUI.skin.GetStyle("PaneOptions"));
                popupStyle.imagePosition = ImagePosition.ImageOnly;
            }
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            EditorGUI.BeginChangeCheck();

            var useLocalValue = property.FindPropertyRelative("useLocalValue");
            var localValue = property.FindPropertyRelative("localValue");
            var variable = property.FindPropertyRelative("variable");

            var buttonRect = new Rect(position);
            buttonRect.yMin += popupStyle.margin.top;
            buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
            position.xMin = buttonRect.xMax;

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var index = useLocalValue.boolValue
                ? 0
                : 1;
            var result = EditorGUI.Popup(buttonRect, index, popupOptions, popupStyle);
            useLocalValue.boolValue = result == 0;
            var propertyToShow = useLocalValue.boolValue
                ? localValue
                : variable;
            EditorGUI.PropertyField(position, propertyToShow, GUIContent.none);
            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}