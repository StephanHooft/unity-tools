﻿using UnityEngine;
using UnityEditor;

namespace StephanHooft.CustomAttributes.EditorScripts
{
    /// <summary>
    /// A <see cref="CustomPropertyDrawer"/> for <see cref="CustomAttributes"/>
    /// <remarks><para>Heavily based on code from: https://github.com/GucioDevs/SimpleMinMaxSlider .</para></remarks>
    /// </summary>
    [CustomPropertyDrawer(typeof(MinMaxAttribute))]
    public class MinMaxSliderDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var minMaxAttribute = (MinMaxAttribute)attribute;
            var propertyType = property.propertyType;
            if (propertyType == SerializedPropertyType.Vector2)
                label.tooltip = "Range between " + minMaxAttribute.min.ToString("F2") + " and " + minMaxAttribute.max.ToString("F2") + ".";
            else if (propertyType == SerializedPropertyType.Vector2Int)
                label.tooltip = "Range between " + minMaxAttribute.min.ToString() + " and " + minMaxAttribute.max.ToString() + ".";
            Rect controlRect = EditorGUI.PrefixLabel(position, label);
            int priorIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            Rect[] splittedRect = CreateMinMaxSliderRects(controlRect, 50, 5);

            // Vector2 support
            if (propertyType == SerializedPropertyType.Vector2)
            {
                EditorGUI.BeginChangeCheck();
                Vector2 vector = property.vector2Value;
                float minVal = vector.x;
                float maxVal = vector.y;
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
            // Vector2Int support
            else if (propertyType == SerializedPropertyType.Vector2Int)
            {
                EditorGUI.BeginChangeCheck();
                Vector2Int vector = property.vector2IntValue;
                float minVal = vector.x;
                float maxVal = vector.y;
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
                throw new System.Exception("MinMaxAttribute can only be assigned to Vector2 and Vector2Int.");
            EditorGUI.indentLevel = priorIndentLevel;
        }

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
            return rects;
        }
    }
}