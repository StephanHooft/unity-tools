#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace StephanHooft.Lines.EditorScripts
{
#if UNITY_EDITOR
    /// <summary>
    /// Custom inspector for <see cref="MultiLine2D"/>.
    /// </summary>
    [CustomEditor(typeof(MultiLine2D))]
    public class MultiLine2DInspector : Editor
    {
        private MultiLine2D line;
        private Transform handleTransform;
        private Quaternion handleRotation;

        private int selectedIndex = -1;

        private const float handleSize = 0.04f;
        private const float pickSize = 0.06f;

        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector();
            line = target as MultiLine2D;
            if (line.PointCount < 1)
                line.Reset();

            EditorGUI.BeginChangeCheck();
            bool loop = EditorGUILayout.Toggle("Loop", line.Loop);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(line, "Toggle Loop");
                EditorUtility.SetDirty(line);
                SetLoop(loop);
            }
            if (GUILayout.Button("Add Node"))
            {
                line.AddNode();
                EditorUtility.SetDirty(line);
            }
            if (line.PointCount > 2 && GUILayout.Button("Remove Node"))
            {
                line.RemoveNode();
                EditorUtility.SetDirty(line);
            }
            if (selectedIndex >= 0 && selectedIndex < line.PointCount)
                DrawSelectedNodeInspector();
        }

        private void OnSceneGUI()
        {
            line = target as MultiLine2D;
            handleTransform = line.transform;
            handleRotation = Tools.pivotRotation == PivotRotation.Local ?
                handleTransform.rotation : Quaternion.identity;

            DrawLine();
            DrawAllNodes();
        }

        private void SetLoop(bool loop)
        {
            line.Loop = loop;
        }

        private void DrawLine()
        {
            Handles.color = Color.white;
            for (int i = 1; i < line.PointCount; i++)
            {
                Handles.DrawLine(
                    handleTransform.TransformPoint(line.GetControlPoint(i-1)),
                    handleTransform.TransformPoint(line.GetControlPoint(i)),
                    2f);
            }
            if (line.Loop)
                Handles.DrawLine(
                    handleTransform.TransformPoint(line.GetControlPoint(line.PointCount - 1)),
                    handleTransform.TransformPoint(line.GetControlPoint(0)),
                    2f);
        }

        private void DrawAllNodes()
        {
            for (int i = 0; i < line.PointCount; i++)
                DrawNode(i);
        }

        private Vector2 DrawNode(int index)
        {
            Vector2 point = handleTransform.TransformPoint(line.GetControlPoint(index));
            DrawNodeButton(index);
            DrawNodeHandle(index);
            return point;
        }

        private void DrawNodeButton(int nodeIndex)
        {
            Vector2 position = handleTransform.TransformPoint(line.GetControlPoint(nodeIndex));
            float size = HandleUtility.GetHandleSize(position);
            size *= nodeIndex == 0 ? 6f : 3f;
            Handles.color = new Color(1f, 0.6f, 0f, 1f);
            if (Handles.Button(position, handleRotation, size * handleSize, size * pickSize, Handles.SphereHandleCap))
            {
                selectedIndex = nodeIndex;
                Repaint();
            }
        }

        private void DrawNodeHandle(int nodeIndex)
        {
            Vector2 position = handleTransform.TransformPoint(line.GetControlPoint(nodeIndex));
            if (selectedIndex == nodeIndex)
            {
                EditorGUI.BeginChangeCheck();
                position = Handles.DoPositionHandle(position, handleRotation);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(line, "Move Node");
                    EditorUtility.SetDirty(line);
                    line.SetControlPoint(nodeIndex, handleTransform.InverseTransformPoint(position));
                }
            }
        }

        private void DrawSelectedNodeInspector()
        {
            EditorGUILayout.Separator();
            GUILayout.Label("Selected Node: " + selectedIndex);
            EditorGUI.indentLevel++;
            VectorField("Node Position", selectedIndex, "Move Node");
            EditorGUI.indentLevel--;

            void VectorField(string fieldName, int index, string undoName)
            {
                EditorGUI.BeginChangeCheck();
                Vector2 point = EditorGUILayout.Vector2Field(fieldName, line.GetControlPoint(index));
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(line, undoName);
                    EditorUtility.SetDirty(line);
                    line.SetControlPoint(index, point);
                }
            }
        }
    }
#endif
}
