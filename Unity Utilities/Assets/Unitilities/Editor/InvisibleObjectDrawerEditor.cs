using UnityEngine;
using UnityEditor;
using System.Collections;

[CanEditMultipleObjects]
[CustomEditor(typeof(InvisibleObjectDrawer))] 
public class InvisibleObjectDrawerEditor : Editor 
{

    public override void OnInspectorGUI()
    {
        InvisibleObjectDrawer item = (InvisibleObjectDrawer)target;

        item.isEnabled = EditorGUILayout.BeginToggleGroup("Enable", item.isEnabled);

        if (item.isEnabled)
        {
            item.radius = EditorGUILayout.FloatField("Radius", item.radius);
            item.color = EditorGUILayout.ColorField("Color", item.color);
        }

        EditorGUILayout.EndToggleGroup();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(item);
        }
    }

}
