using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace StephanHooft.CustomEditorScripts
{
    /// <summary>
    /// A collection of helper methods for use in custom inspectors.
    /// </summary>
    public static class InspectorTools
    {
        #region Static Methods

        /// <summary>
        /// Draws a toggle based on a <see cref="bool"/> <paramref name="field"/> value in a custom inspector and
        /// returns whether its value was changed. The <paramref name="modified"/> out <see cref="bool"/> can be used
        /// to set the result to the original field.
        /// </summary>
        /// <param name="label">The label of the toggle to make.</param>
        /// <param name="field">The field value to set to the toggle.</param>
        /// <param name="modified">The modified toggle field value.</param>
        /// <returns><see cref="true"/> if the toggle value was modified.</returns>
        public static bool DrawBoolFieldToggle(string label, bool field, out bool modified)
        {
            modified = field;
            if (EditorGUILayout.Toggle(label, field) != field)
                modified = !field;
            return field != modified;
        }

        /// <summary>
        /// Draws a <see cref="LayerMask"/> field in a custom inspector.
        /// https://answers.unity.com/questions/42996/how-to-create-layermask-field-in-a-custom-editorwi.html
        /// </summary>
        /// <param name="label">The label of the <see cref="LayerMask"/> field.</param>
        /// <param name="layerMask">The existing <see cref="LayerMask"/> value to set to the field.</param>
        /// <returns>The resulting <see cref="LayerMask"/>.</returns>
        public static LayerMask DrawLayerMaskField(string label, LayerMask layerMask)
        {
            List<string> layers = new List<string>();
            List<int> layerNumbers = new List<int>();

            for (int i = 0; i < 32; i++)
            {
                string layerName = LayerMask.LayerToName(i);
                if (layerName != "")
                {
                    layers.Add(layerName);
                    layerNumbers.Add(i);
                }
            }
            int maskWithoutEmpty = 0;
            for (int i = 0; i < layerNumbers.Count; i++)
            {
                if (((1 << layerNumbers[i]) & layerMask.value) > 0)
                    maskWithoutEmpty |= (1 << i);
            }
            maskWithoutEmpty = EditorGUILayout.MaskField(label, maskWithoutEmpty, layers.ToArray());
            int mask = 0;
            for (int i = 0; i < layerNumbers.Count; i++)
            {
                if ((maskWithoutEmpty & (1 << i)) > 0)
                    mask |= (1 << layerNumbers[i]);
            }
            layerMask.value = mask;
            return layerMask;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
