using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditorInternal;

namespace StephanHooft.Attributes.EditorScripts
{
    /// <summary>
    /// A custom <see cref="PropertyDrawer"/> for the <see cref="TagSelectorAttribute"/>.
    /// <remarks><para>Borrowed from: https://github.com/WSWhitehouse/Unity-Tag-Selector .</para></remarks>
    /// </summary>
    [CustomPropertyDrawer(typeof(TagSelectorAttribute))]
    public class TagSelectorPropertyDrawer : PropertyDrawer
    {
        #region PropertyDrawer Implementation

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                EditorGUI.BeginProperty(position, label, property);
                TagSelectorAttribute selectorAttribute = attribute as TagSelectorAttribute;
                if (selectorAttribute.UseDefaultTagFieldDrawer)
                    property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
                else
                {
                    List<string> tagList = new() { "No Tag" };
                    tagList.AddRange(InternalEditorUtility.tags);
                    string propertyString = property.stringValue;
                    int index = -1;
                    if (propertyString == "")
                        index = 0;
                    else
                    {
                        for (int i = 1; i < tagList.Count; i++)
                        {
                            if (tagList[i] != propertyString)
                                continue;
                            index = i;
                            break;
                        }
                    }
                    index = EditorGUI.Popup(position, label.text, index, tagList.ToArray());
                    if (index == 0)
                        property.stringValue = "";
                    else if (index >= 1)
                        property.stringValue = tagList[index];
                    else
                        property.stringValue = "";
                }
                EditorGUI.EndProperty();
            }
            else
                EditorGUI.PropertyField(position, property, label);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion        
    }
}