/// <summary>
/// ReadOnlyDrawer v1.0.0
/// 
/// Taken from: http://answers.unity3d.com/questions/489942/how-to-make-a-readonly-property-in-inspector.html
/// 
/// Use the [ReadOnly] attribute to make deny any changes to a variables through the inspector.
/// 
/// </summary>


using UnityEditor;
using UnityEngine;

namespace Unitilities
{

    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property,
                                                GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position,
                                   SerializedProperty property,
                                   GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }

}