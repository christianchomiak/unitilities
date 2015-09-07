/// <summary>
/// Singleton v1.0.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Custom drawer for the Tuple class included in Unitilities
/// </summary>

using UnityEngine;
using UnityEditor;

namespace Unitilities.Tuples
{
    [CustomPropertyDrawer(typeof(TupleI))]
    public class TupleIDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            SerializedProperty first = prop.FindPropertyRelative("first");
            SerializedProperty second = prop.FindPropertyRelative("second");

            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(pos, label, prop);
            
            // Draw label
            pos = EditorGUI.PrefixLabel(pos, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            var amountRect = new Rect(pos.x, pos.y, 75, pos.height);
            var unitRect = new Rect(pos.x + 80, pos.y, 75, pos.height);

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(amountRect, first, GUIContent.none);
            //EditorGUI.PropertyField(amountRect, prop.FindPropertyRelative("first"), GUIContent.none);
            EditorGUI.PropertyField(unitRect, second, GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}