/// <summary>
/// TransformInspector v1.0.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Script to provide shortcuts in the Transform Inspector to reset the Position,
/// Scale and Rotation of a GameObject.
/// 
/// Based on: http://wiki.unity3d.com/index.php/TransformInspector
/// </summary>
using UnityEngine;
using UnityEditor;

namespace Unitilities
{

    [CustomEditor(typeof(Transform))]
    public class TransformInspector : Editor
    {
        public override void OnInspectorGUI()
        {

            Transform t = (Transform) target;

            // Replicate the standard transform inspector gui
            //EditorGUIUtility.LookLikeControls();
            EditorGUIUtility.labelWidth = 0;
            EditorGUIUtility.fieldWidth = 0;

            EditorGUI.indentLevel = 0;

            float buttonWidth = 20, labelWidth = 50, fieldWidth = 176;


            GUILayout.BeginHorizontal();
            Vector3 position = t.localPosition;
            if (GUILayout.Button("P", GUILayout.Width(buttonWidth)))
            {
                position = Vector3.zero;
            }
            EditorGUILayout.LabelField("Position", GUILayout.Width(labelWidth));
            position = EditorGUILayout.Vector3Field(GUIContent.none, position, GUILayout.MinWidth(fieldWidth));
            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();
            Vector3 eulerAngles = t.localEulerAngles;
            if (GUILayout.Button("R", GUILayout.Width(buttonWidth)))
            {
                eulerAngles = Vector3.zero;
            }
            EditorGUILayout.LabelField("Rotation", GUILayout.Width(labelWidth));
            eulerAngles = EditorGUILayout.Vector3Field(GUIContent.none, eulerAngles, GUILayout.MinWidth(fieldWidth));
            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();
            Vector3 scale = t.localScale;
            if (GUILayout.Button("S", GUILayout.Width(buttonWidth)))
            {
                scale = Vector3.one;
            }
            EditorGUILayout.LabelField("Scale", GUILayout.Width(labelWidth));
            scale = EditorGUILayout.Vector3Field(GUIContent.none, scale, GUILayout.MinWidth(fieldWidth));
            GUILayout.EndHorizontal();

            //position = EditorGUILayout.Vector3Field("Position", t.localPosition);
            //eulerAngles = EditorGUILayout.Vector3Field("Rotation", t.localEulerAngles);
            //scale = EditorGUILayout.Vector3Field("Scale", t.localScale);

            //EditorGUIUtility.LookLikeInspector();
            EditorGUIUtility.labelWidth = 0;
            EditorGUIUtility.fieldWidth = 0;


            //EditorGUIUtility.LookLikeControls

            if (GUI.changed)
            {
                Undo.RecordObject(t, "Transform Change");
                //Undo.RegisterUndo(t, "Transform Change");

                t.localPosition = FixIfNaN(position);
                t.localEulerAngles = FixIfNaN(eulerAngles);
                t.localScale = FixIfNaN(scale);
            }
        }

        private Vector3 FixIfNaN(Vector3 v)
        {
            if (float.IsNaN(v.x))
            {
                v.x = 0;
            }
            if (float.IsNaN(v.y))
            {
                v.y = 0;
            }
            if (float.IsNaN(v.z))
            {
                v.z = 0;
            }
            return v;
        }

    }

}