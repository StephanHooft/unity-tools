#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace StephanHooft.Lines.EditorScripts
{
#if UNITY_EDITOR
    /// <summary>
    /// Custom inspector for <see cref="BezierSpline2D"/>.
    /// </summary>
    [CustomEditor(typeof(BezierSpline2D))]
    public class BezierSpline2DInspector : Editor
    {
        private BezierSpline2D spline;
        private Transform handleTransform;
        private Quaternion handleRotation;

        private int selectedIndex = -1;
        private int selectedPointIndex = -1;

        private const int stepsPerCurve = 10;
        private const float directionScale = 0.5f;

        private const float handleSize = 0.04f;
        private const float pickSize = 0.06f;

        private static Color[] modeColors = {
            new Color(1f, 0f, 0f, 1f),
            new Color(1f, 0.6f, 0f, 1f),
            new Color(1f, 1f, 0f, 1f)
        };

        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector();
            spline = target as BezierSpline2D;
            if (spline.PointCount < 1)
                spline.Reset();

            EditorGUI.BeginChangeCheck();
            bool loop = EditorGUILayout.Toggle("Loop", spline.Loop);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(spline, "Toggle Loop");
                EditorUtility.SetDirty(spline);
                SetLoop(loop);
            }
            if (GUILayout.Button("Add Node"))
            {
                spline.AddNode();
                EditorUtility.SetDirty(spline);
            }
            if (spline.PointCount > 2 && GUILayout.Button("Remove Node"))
            {
                spline.RemoveNode();
                EditorUtility.SetDirty(spline);
            }
            if (selectedIndex >= 0 && selectedIndex < spline.PointCount)
                DrawSelectedNodeInspector();
        }

        private void OnSceneGUI()
        {
            spline = target as BezierSpline2D;
            handleTransform = spline.transform;
            handleRotation = Tools.pivotRotation == PivotRotation.Local ?
                handleTransform.rotation : Quaternion.identity;

            DrawBezier();
            //ShowDirections();
            DrawAllNodes();
        }

        private void SetLoop(bool loop)
        {
            if (!loop)
            {
                if(selectedIndex == 0 && selectedPointIndex == 1 || selectedIndex == spline.PointCount - 1 && selectedPointIndex == 2)
                {
                    selectedIndex = -1;
                    selectedPointIndex = -1;
                }
            }
            spline.Loop = loop;
        }

        private void DrawBezier()
        {
            for (int i = 1; i < spline.PointCount; i++)
            {
                Handles.DrawBezier(
                    handleTransform.TransformPoint(spline.GetControlPoint(i - 1, 0)),
                    handleTransform.TransformPoint(spline.GetControlPoint(i, 0)),
                    handleTransform.TransformPoint(spline.GetControlPoint(i - 1, 2)),
                    handleTransform.TransformPoint(spline.GetControlPoint(i, 1)), 
                    Color.white, null, 2f);
            }
            if (spline.Loop)
                Handles.DrawBezier(
                    handleTransform.TransformPoint(spline.GetControlPoint(spline.PointCount - 1, 0)),
                    handleTransform.TransformPoint(spline.GetControlPoint(0, 0)),
                    handleTransform.TransformPoint(spline.GetControlPoint(spline.PointCount - 1, 2)),
                    handleTransform.TransformPoint(spline.GetControlPoint(0, 1)),
                    Color.white, null, 2f);
        }

        private void ShowDirections()
        {
            Handles.color = Color.green;
            Vector2 point = spline.GetPoint(0f);
            Handles.DrawLine(point, point + spline.GetDirection(0f) * directionScale);
            int steps = stepsPerCurve * spline.SegmentCount;
            for (int i = 1; i <= steps; i++)
            {
                point = spline.GetPoint(i / (float)steps);
                Handles.DrawLine(point, point + spline.GetDirection(i / (float)steps) * directionScale);
            }
        }

        private void DrawAllNodes()
        {
            for (int i = 0; i < spline.PointCount; i++)
                DrawNode(i);
        }

        private Vector2 DrawNode(int index)
        {
            Vector2 point = handleTransform.TransformPoint(spline.GetControlPoint(index, 0));
            Vector2 controlPointPre = handleTransform.TransformPoint(spline.GetControlPoint(index, 1));
            Vector2 controlPointPost = handleTransform.TransformPoint(spline.GetControlPoint(index, 2));

            Handles.color = Color.gray;
            if(spline.Loop || index > 0)
                Handles.DrawLine(point, controlPointPre);
            if(spline.Loop || index < spline.PointCount - 1)
                Handles.DrawLine(point, controlPointPost);

            for (int i = 0; i <= 2; i++)
            {
                DrawNodeButton(index, i);
                DrawNodeHandle(index, i);
            }
            return point;
        }

        private void DrawNodeButton(int nodeIndex, int pointIndex)
        {
            if (!spline.Loop && (nodeIndex == 0 && pointIndex == 1 || nodeIndex == spline.PointCount - 1 && pointIndex == 2))
                return;
            Vector2 position = handleTransform.TransformPoint(spline.GetControlPoint(nodeIndex, pointIndex));
            float size = HandleUtility.GetHandleSize(position);
            if (pointIndex == 0)
                size *= nodeIndex == 0 ? 6f : 3f;
            Handles.color = modeColors[(int)spline.GetControlPointMode(nodeIndex)];
            Handles.CapFunction function;
            if (pointIndex == 0) function = Handles.SphereHandleCap; else function = Handles.DotHandleCap;
            if (Handles.Button(position, handleRotation, size * handleSize, size * pickSize, function))
            {
                selectedIndex = nodeIndex;
                selectedPointIndex = pointIndex;
                Repaint();
            }
        }

        private void DrawNodeHandle(int nodeIndex, int pointIndex)
        {
            if (!spline.Loop && (nodeIndex == 0 && pointIndex == 1 || nodeIndex == spline.PointCount - 1 && pointIndex == 2))
                return;
            Vector2 position = handleTransform.TransformPoint(spline.GetControlPoint(nodeIndex, pointIndex));
            if (selectedIndex == nodeIndex && selectedPointIndex == pointIndex)
            {
                EditorGUI.BeginChangeCheck();
                position = Handles.DoPositionHandle(position, handleRotation);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(spline, pointIndex == 0 ? "Move Node" : "Move Control Point");
                    EditorUtility.SetDirty(spline);
                    spline.SetControlPoint(nodeIndex, pointIndex, handleTransform.InverseTransformPoint(position));
                }
            }
        }

        private void DrawSelectedNodeInspector()
        {
            EditorGUILayout.Separator();
            GUILayout.Label("Selected Node: " + selectedIndex);
            EditorGUI.indentLevel++;
            VectorField("Node Position", 0, "Move Node");
            EditorGUI.BeginChangeCheck();
            BezierControlPointMode mode = (BezierControlPointMode)EditorGUILayout.EnumPopup("Control Point Mode", spline.GetControlPointMode(selectedIndex));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(spline, "Change Control Point Mode");
                spline.SetControlPointMode(selectedIndex, mode);
                EditorUtility.SetDirty(spline);
            }
            VectorField("Control Point 1", 1, "Move Control Point");
            VectorField("Control Point 2", 2, "Move Control Point");
            EditorGUI.indentLevel--;

            void VectorField(string fieldName, int index, string undoName)
            {
                EditorGUI.BeginChangeCheck();
                Vector2 point = EditorGUILayout.Vector2Field(fieldName, spline.GetControlPoint(selectedIndex, index));
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(spline, undoName);
                    EditorUtility.SetDirty(spline);
                    spline.SetControlPoint(selectedIndex, index, point);
                }
            }
        }
    }
#endif
}
