using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace StephanHooft.VariableRanges.EditorScripts
{
    /// <summary>
    /// A custom property drawer for <see cref="DoubleRangeMinMaxAttribute"/>.
    /// </summary>
    [CustomPropertyDrawer(typeof(DoubleRangeMinMaxAttribute))]
    public class DoubleRangeMinMaxAttributeDrawer : PropertyDrawer
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
            var attribute = (DoubleRangeMinMaxAttribute)base.attribute;
            var propertyType = property.type;
            if (propertyType == typeof(DoubleRange).Name)
                label.tooltip = string.Format("Range between {0} and {1}.",
                    attribute.min.ToString("F2"), attribute.max.ToString("F2"));
            position = EditorGUI.PrefixLabel(position, label);
            var priorIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var splittedRect = CreateMinMaxSliderRects(position, 50, 5);
            if (propertyType == typeof(DoubleRange).Name)
            {
                EditorGUI.BeginChangeCheck();
                var lowerProperty = property.FindPropertyRelative("lower");
                var upperProperty = property.FindPropertyRelative("upper");
                var lower = (float)EditorGUI.DoubleField(splittedRect[0], double.Parse(lowerProperty.doubleValue.ToString("F2")));
                var upper = (float)EditorGUI.DoubleField(splittedRect[2], double.Parse(upperProperty.doubleValue.ToString("F2")));
                EditorGUI.MinMaxSlider(splittedRect[1], ref lower, ref upper, (float)attribute.min, (float)attribute.max);
                if (lower < attribute.min)
                    lower = (float)attribute.min;
                if (upper > attribute.max)
                    upper = (float)attribute.max;
                if (EditorGUI.EndChangeCheck())
                {
                    lowerProperty.doubleValue = lower;
                    upperProperty.doubleValue = upper;
                }
            }
            else
                throw
                    new System.Exception(
                        string.Format("{0} can only be assigned to {1}.",
                        typeof(DoubleRangeMinMaxAttribute).Name, typeof(DoubleRange).Name));
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

        private Rect[] CreateMinMaxSliderRects(Rect controlRect, int doubleFieldWidth, int padding)
        {
            Rect[] rects = new Rect[3];
            rects[0] = new Rect(
                controlRect.x,
                controlRect.y,
                doubleFieldWidth,
                controlRect.height
                );
            rects[1] = new Rect(
                controlRect.x + doubleFieldWidth + padding,
                controlRect.y,
                controlRect.width - 2 * (doubleFieldWidth + padding),
                controlRect.height
                );
            rects[2] = new Rect(
                controlRect.x + (controlRect.width - doubleFieldWidth),
                controlRect.y,
                doubleFieldWidth,
                controlRect.height
                );
            return
                rects;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
