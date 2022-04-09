using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace StephanHooft.VariableRanges.EditorScripts
{
    /// <summary>
    /// A custom property drawer for <see cref="FloatRange"/>.
    /// </summary>
    [CustomPropertyDrawer(typeof(FloatRange))]
    public class FloatRangeDrawer : PropertyDrawer
    {
        #region Fields
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region PropertyDrawer Implementation

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => base.GetPropertyHeight(property, label);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Keyboard), label);
            var indent = EditorGUI.indentLevel;
            var labelWidth = EditorGUIUtility.labelWidth;
            EditorGUI.indentLevel = 0;
            EditorGUIUtility.labelWidth = 7;
            var rects = CreateRects(position, 7, 5);
            EditorGUI.BeginChangeCheck();
            var lowerProperty = property.FindPropertyRelative("lower");
            var upperProperty = property.FindPropertyRelative("upper");
            var lower = EditorGUI.FloatField(rects[0], lowerProperty.floatValue);
            EditorGUI.LabelField(rects[1], "-");
            var upper = EditorGUI.FloatField(rects[2], upperProperty.floatValue);
            if (lower > upper)
                upper = lower;
            if (EditorGUI.EndChangeCheck())
            {
                lowerProperty.floatValue = lower;
                upperProperty.floatValue = upper;
            }
            EditorGUIUtility.labelWidth = labelWidth;
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            var lowerField = new PropertyField(property.FindPropertyRelative("lower"));
            var upperField = new PropertyField(property.FindPropertyRelative("upper"));
            container.Add(lowerField);
            container.Add(upperField);
            return
                container;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        private Rect[] CreateRects(Rect controlRect, float labelWidth, float padding)
        {
            Rect[] rects = new Rect[3];
            var fieldWidth = (controlRect.width - ((padding * 2) + labelWidth)) / 2;
            rects[0] = new Rect(
                controlRect.x,
                controlRect.y,
                fieldWidth,
                controlRect.height
                );
            rects[1] = new Rect(
                controlRect.x + fieldWidth + padding,
                controlRect.y,
                labelWidth,
                controlRect.height
                );
            rects[2] = new Rect(
                controlRect.x + fieldWidth + (padding * 2) + labelWidth,
                controlRect.y,
                fieldWidth,
                controlRect.height
                );
            return
                rects;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
