/// <summary>
/// HSVColorDrawer v1.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Custom property drawer for the HSVColor struct.
/// </summary>

using UnityEngine;
using UnityEditor;
using Unitilities.Colors;

namespace Unitilities.Tuples
{
    [CustomPropertyDrawer(typeof(HSVColor))]
    public class HSVColorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            
            SerializedProperty hue = prop.FindPropertyRelative("hue");
            SerializedProperty saturation = prop.FindPropertyRelative("saturation");
            SerializedProperty value = prop.FindPropertyRelative("value");
            SerializedProperty alpha = prop.FindPropertyRelative("alpha");

            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(pos, label, prop);

            // Draw label
            pos = EditorGUI.PrefixLabel(pos, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            EditorGUI.BeginChangeCheck();
            Color newColor = EditorGUI.ColorField(pos, HSVColor.ToRGB(hue.floatValue, saturation.floatValue, value.floatValue, alpha.floatValue));
            // Only assign the value back if it was actually changed by the user.
            // Otherwise a single value will be assigned to all objects when multi-object editing,
            // even when the user didn't touch the control.
            if (EditorGUI.EndChangeCheck())
            {
                HSVColor newHSVColor = HSVColor.FromRGB(newColor);

                prop.FindPropertyRelative("hue").floatValue = newHSVColor.Hue;
                prop.FindPropertyRelative("saturation").floatValue = newHSVColor.Saturation;
                prop.FindPropertyRelative("value").floatValue = newHSVColor.Value;
                prop.FindPropertyRelative("alpha").floatValue = newHSVColor.Alpha;
            }
                

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}