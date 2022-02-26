using UnityEngine;
using UnityEditor;

namespace StephanHooft.Attributes.EditorScripts
{
    /// <summary>
    /// A <see cref="CustomPropertyDrawer"/> for the <see cref="MinMaxAttribute"/>.
    /// <remarks><para>Heavily based on code from: https://github.com/GucioDevs/SimpleMinMaxSlider .</para></remarks>
    /// </summary>
    [CustomPropertyDrawer(typeof(MinMaxAttribute))]
    public class MinMaxDrawer : PropertyDrawer
    {
        #region PropertyDrawer Implementation

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var minMaxAttribute = (MinMaxAttribute)attribute;
            var propertyType = property.propertyType;
            if (propertyType == SerializedPropertyType.Vector2)
                label.tooltip = string.Format("Range between {0} and {1}.", minMaxAttribute.min.ToString("F2"), minMaxAttribute.max.ToString("F2"));
            else if (propertyType == SerializedPropertyType.Vector2Int)
                label.tooltip = string.Format("Range between {0} and {1}.", minMaxAttribute.min, minMaxAttribute.max);
            var controlRect = EditorGUI.PrefixLabel(position, label);
            var priorIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var splittedRect = CreateMinMaxSliderRects(controlRect, 50, 5);
            if (propertyType == SerializedPropertyType.Vector2)
            {
                EditorGUI.BeginChangeCheck();
                var vector = property.vector2Value;
                var minVal = vector.x;
                var maxVal = vector.y;
                minVal = EditorGUI.FloatField(splittedRect[0], float.Parse(minVal.ToString("F2"))); // Draw a float field in the area of splittedRect[0]
                maxVal = EditorGUI.FloatField(splittedRect[2], float.Parse(maxVal.ToString("F2"))); // Draw a float field in the area of splittedRect[2]
                EditorGUI.MinMaxSlider(splittedRect[1], ref minVal, ref maxVal, minMaxAttribute.min, minMaxAttribute.max);
                if (minVal < minMaxAttribute.min)
                    minVal = minMaxAttribute.min;
                if (maxVal > minMaxAttribute.max)
                    maxVal = minMaxAttribute.max;
                vector = new Vector2(minVal > maxVal ? maxVal : minVal, maxVal);
                if (EditorGUI.EndChangeCheck())
                    property.vector2Value = vector;
            }
            else if (propertyType == SerializedPropertyType.Vector2Int)
            {
                EditorGUI.BeginChangeCheck();
                var vector = property.vector2IntValue;
                var minVal = (float)vector.x;
                var maxVal = (float)vector.y;
                minVal = EditorGUI.FloatField(splittedRect[0], minVal);
                maxVal = EditorGUI.FloatField(splittedRect[2], maxVal);
                EditorGUI.MinMaxSlider(splittedRect[1], ref minVal, ref maxVal, minMaxAttribute.min, minMaxAttribute.max);
                if (minVal < minMaxAttribute.min)
                    maxVal = minMaxAttribute.min;
                if (minVal > minMaxAttribute.max)
                    maxVal = minMaxAttribute.max;
                vector = new Vector2Int(Mathf.FloorToInt(minVal > maxVal ? maxVal : minVal), Mathf.FloorToInt(maxVal));
                if (EditorGUI.EndChangeCheck())
                    property.vector2IntValue = vector;
            }
            else
                throw
                    new System.Exception("MinMaxAttribute can only be assigned to Vector2 and Vector2Int.");
            EditorGUI.indentLevel = priorIndentLevel;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// Split a <see cref="Rect"/> <paramref name="controlRect"/> into 3 <see cref="Rect"/>s.
        /// </summary>
        /// <param name="controlRect">The <see cref="Rect"/> on which to base three new <see cref="Rect"/>s.</param>
        /// <param name="floatFieldWidth"></param>
        /// <param name="padding"></param>
        /// <returns>An array of <see cref="Rect"/>s of size 3.</returns>
        private Rect[] CreateMinMaxSliderRects(Rect controlRect, int floatFieldWidth, int padding)
        {
            Rect[] rects = new Rect[3];
            rects[0] = new Rect(
                controlRect.x,
                controlRect.y,
                floatFieldWidth,
                controlRect.height
                );
            rects[1] = new Rect(
                controlRect.x + floatFieldWidth + padding,
                controlRect.y,
                controlRect.width - 2 * (floatFieldWidth + padding),
                controlRect.height
                );
            rects[2] = new Rect(
                controlRect.x + (controlRect.width - floatFieldWidth),
                controlRect.y,
                floatFieldWidth,
                controlRect.height
                );
            return
                rects;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
