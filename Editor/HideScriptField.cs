using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Based on code found at: https://forum.unity.com/threads/hiding-script-field-in-default-inspector.269149/

namespace StephanHooft.CustomEditorScripts
{
    /// <summary>
    /// Custom editor for MonoBehaviours.
    /// </summary>
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class DefaultInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            this.DrawDefaultInspectorWithoutScriptField();
        }
    }

    /// <summary>
    /// Custom editor for ScriptableObjects.
    /// </summary>
    [CustomEditor(typeof(ScriptableObject), true)]
    [CanEditMultipleObjects]
    public class DefaultScriptableObjectInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            this.DrawDefaultInspectorWithoutScriptField();
        }
    }

    /// <summary>
    /// Editor extension class that allows drawing of Inspectors without the "Script" field.
    /// </summary>
    public static class DefaultInspector_EditorExtension
    {
        /// <summary>
        /// Draws an Inspector without the "Script" field.
        /// </summary>
        /// <param name="Inspector">The Inspector to draw</param>
        /// <returns>True if a control was changed.</returns>
        public static bool DrawDefaultInspectorWithoutScriptField(this Editor Inspector)
        {
            EditorGUI.BeginChangeCheck();
            Inspector.serializedObject.Update();
            SerializedProperty Iterator = Inspector.serializedObject.GetIterator();
            Iterator.NextVisible(true);
            while (Iterator.NextVisible(false))
                EditorGUILayout.PropertyField(Iterator, true);
            Inspector.serializedObject.ApplyModifiedProperties();
            return (EditorGUI.EndChangeCheck());
        }
    }
}
