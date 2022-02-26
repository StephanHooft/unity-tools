using UnityEditor;
using UnityEngine;

namespace StephanHooft.Attributes.EditorScripts
{
    /// <summary>
    /// A <see cref="CustomPropertyDrawer"/> for the <see cref="LabelOverride"/>.
    /// <remarks><para>Borrowed from: https://answers.unity.com/questions/1005277/can-i-change-variable-name-on-inspector.html .</para></remarks>
    /// </summary>
    [CustomPropertyDrawer(typeof(LabelOverride))]
    public class LabelOverrideDrawer : PropertyDrawer
    {
        #region PropertyDrawer Implementation

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            try
            {
                var propertyAttribute = attribute as LabelOverride;
                if (DetectArray(property) == false)
                    label.text = propertyAttribute.label;
                else
                    Debug.LogWarningFormat("{0}(\"{1}\") doesn't support arrays ", typeof(LabelOverride).Name, propertyAttribute.label);
                EditorGUI.PropertyField(position, property, label);
            }
            catch (System.Exception ex) { Debug.LogException(ex); }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion

        #region Methods

        private bool DetectArray(SerializedProperty property)
        {
            string path = property.propertyPath;
            int idot = path.IndexOf('.');
            if (idot == -1) return false;
            string propName = path.Substring(0, idot);
            SerializedProperty p = property.serializedObject.FindProperty(propName);
            return p.isArray;
            //CREDITS: https://answers.unity.com/questions/603882/serializedproperty-isnt-being-detected-as-an-array.html
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
