using UnityEngine;
using UnityEditor;
using System.Collections;

[CanEditMultipleObjects]
[CustomEditor(typeof(ObjectTextDrawer))] 
public class ObjectTextDrawerEditor : Editor 
{

    public override void OnInspectorGUI()
    {
        ObjectTextDrawer item = (ObjectTextDrawer)target;

        item.isEnabled = EditorGUILayout.BeginToggleGroup("Enable", item.isEnabled);

        if (item.isEnabled)
        {
            item.write = (ObjectTextDrawer.ObjectTextOptions)EditorGUILayout.EnumPopup("Type", item.write);

            switch (item.write)
            {
                case ObjectTextDrawer.ObjectTextOptions.CustomText:

                    item.customText = EditorGUILayout.TextField("Custom text", item.customText);

                    break;
                default:
                    break;
            }

            item.textSize = EditorGUILayout.IntSlider("Font size", item.textSize, 15, 50); //, item.textSize);

            item.boldText = EditorGUILayout.Toggle("Bold text", item.boldText);
            item.italicText = EditorGUILayout.Toggle("Italic text", item.italicText);

            item.textColor = EditorGUILayout.ColorField("Font color", item.textColor);
            item.backgroundColor = EditorGUILayout.ColorField("Background color", item.backgroundColor);
        }

        EditorGUILayout.EndToggleGroup();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(item);
        }
    }

}
