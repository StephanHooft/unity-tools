using UnityEditor;
using UnityEngine;

namespace StephanHooft.HybridUpdate.EditorScripts
{
#if UNITY_EDITOR
    /// <summary>
    /// Custom inspector for <see cref="HybridUpdater"/>.
    /// </summary>
    [CustomEditor(typeof(HybridUpdater))]
    public class HybridUpdaterInspector : Editor
    {
        #region Fields

        private SerializedProperty paused;
        private SerializedProperty timeSpeed;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Editor Implementation

        private void OnEnable()
        {
            paused = serializedObject.FindProperty("paused");
            timeSpeed = serializedObject.FindProperty("timeSpeed");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(paused);
            EditorGUILayout.PropertyField(timeSpeed);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();
            EditorGUILayout.Space(10f);
            if (Application.isPlaying)
            {
                if (AtLeastOneType())
                {
                    EditorGUILayout.LabelField(
                    string.Format("Registered {0} Types", typeof(HybridUpdater.Behaviour).Name),
                    EditorStyles.boldLabel);
                    EditorGUILayout.Space(5);
                    var labelWidth = EditorGUIUtility.labelWidth;
                    EditorGUIUtility.labelWidth = 40f;
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Rank");
                    EditorGUILayout.LabelField("Type Name");
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space(2);
                    foreach ((string name, int rank) pair in target as HybridUpdater)
                        EditorGUILayout.TextField(pair.rank.ToString(), pair.name);
                    EditorGUI.EndDisabledGroup();
                    EditorGUIUtility.labelWidth = labelWidth;
                }
            }
            else
                EditorGUILayout.HelpBox(string.Format(
                    "While the application is playing, all {0} types registered to this {1} will be listed here.",
                    typeof(HybridUpdater.Behaviour).Name, typeof(HybridUpdater).Name),
                    MessageType.Info);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        private bool AtLeastOneType()
        {
            foreach ((string, int) pair in target as HybridUpdater)
                return
                    true;
            return
                false;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
#endif
}
