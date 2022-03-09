using UnityEditor;
using UnityEngine;

namespace StephanHooft.Attributes.EditorScripts
{
    /// <summary>
    /// A custom property drawer for <see cref="HideWhilePlayingAttribute"/>.
    /// </summary>
    [CustomPropertyDrawer(typeof(HideWhilePlayingAttribute))]
    public class HideWhilePlayingAttributeDrawer : PropertyDrawer
    {
        #region PropertyDrawer Implementation

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (Application.isPlaying)
                return 
                    -EditorGUIUtility.standardVerticalSpacing;
            else
                return
                    base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!Application.isPlaying)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
