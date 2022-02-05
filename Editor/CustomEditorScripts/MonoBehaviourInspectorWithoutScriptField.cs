using StephanHooft.Extensions;
using UnityEditor;
using UnityEngine;

namespace StephanHooft.CustomEditorScripts
{
    /// <summary>
    /// Custom <see cref="Editor"/> for <see cref="MonoBehaviour"/>s.
    /// <para>Based on code found at:
    /// https://forum.unity.com/threads/hiding-script-field-in-default-inspector.269149/</para>
    /// </summary>
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class MonoBehaviourInspectorWithoutScriptField : Editor
    {
        #region Editor Implementation

        public override void OnInspectorGUI()
        {
            this.DrawDefaultInspectorWithoutScriptField();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
