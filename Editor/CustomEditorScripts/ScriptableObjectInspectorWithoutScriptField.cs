using StephanHooft.Extensions;
using UnityEditor;
using UnityEngine;

namespace StephanHooft.CustomEditorScripts
{
    /// <summary>
    /// Custom <see cref="Editor"/> for <see cref="ScriptableObject"/>s.
    /// </summary>
    [CustomEditor(typeof(ScriptableObject), true)]
    [CanEditMultipleObjects]
    public class ScriptableObjectInspectorWithoutScriptField : Editor
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
