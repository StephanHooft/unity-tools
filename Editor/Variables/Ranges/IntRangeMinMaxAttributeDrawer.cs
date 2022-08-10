using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace StephanHooft.Variables.Ranges.EditorScripts
{
    /// <summary>
    /// A custom property drawer for <see cref="IntRangeMinMaxAttribute"/>.
    /// </summary>
    [CustomPropertyDrawer(typeof(IntRangeMinMaxAttribute))]
    public class IntRangeMinMaxAttributeDrawer : PropertyDrawer
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
            var attribute = (IntRangeMinMaxAttribute)base.attribute;
            var propertyType = property.type;
            if (propertyType == typeof(IntRange).Name)
                label.tooltip = string.Format("Range between {0} and {1}.",
                    attribute.min.ToString("F2"), attribute.max.ToString("F2"));
            position = EditorGUI.PrefixLabel(position, label);
            var priorIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var splittedRect = CreateMinMaxSliderRects(position, 50, 5);
            if (propertyType == typeof(IntRange).Name)
            {
                EditorGUI.BeginChangeCheck();
                var lowerProperty = property.FindPropertyRelative("lower");
                var upperProperty = property.FindPropertyRelative("upper");
                var lower = (float)EditorGUI.IntField(splittedRect[0], lowerProperty.intValue);
                var upper = (float)EditorGUI.IntField(splittedRect[2], upperProperty.intValue);
                EditorGUI.MinMaxSlider(splittedRect[1], ref lower, ref upper, attribute.min, attribute.max);
                if (lower < attribute.min)
                    lower = attribute.min;
                if (upper > attribute.max)
                    upper = attribute.max;
                if (EditorGUI.EndChangeCheck())
                {
                    lowerProperty.intValue = Mathf.FloorToInt(lower);
                    upperProperty.intValue = Mathf.FloorToInt(upper);
                }
            }
            else
                throw
                    new System.Exception(
                        string.Format("{0} can only be assigned to {1}.",
                        typeof(IntRangeMinMaxAttribute).Name, typeof(IntRange).Name));
            EditorGUI.indentLevel = priorIndentLevel;
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

        private Rect[] CreateMinMaxSliderRects(Rect controlRect, int intFieldWidth, int padding)
        {
            Rect[] rects = new Rect[3];
            rects[0] = new Rect(
                controlRect.x,
                controlRect.y,
                intFieldWidth,
                controlRect.height
                );
            rects[1] = new Rect(
                controlRect.x + intFieldWidth + padding,
                controlRect.y,
                controlRect.width - 2 * (intFieldWidth + padding),
                controlRect.height
                );
            rects[2] = new Rect(
                controlRect.x + (controlRect.width - intFieldWidth),
                controlRect.y,
                intFieldWidth,
                controlRect.height
                );
            return
                rects;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
