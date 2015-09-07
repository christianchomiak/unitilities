/// <summary>
/// InvisibleObjectDrawerEditor v1.0.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Custom editor for InvisibleObjectDrawer.
/// </summary>

using UnityEngine;
using UnityEditor;

namespace Unitilities.Debugging
{

    [CanEditMultipleObjects]
    [CustomEditor(typeof(InvisibleObjectDrawer))]
    public class InvisibleObjectDrawerEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            InvisibleObjectDrawer item = (InvisibleObjectDrawer) target;

            item.isEnabled = EditorGUILayout.BeginToggleGroup("Enable", item.isEnabled);

            if (item.isEnabled)
            {
                item.shape = (InvisibleObjectDrawer.Shape) EditorGUILayout.EnumPopup("Shape", item.shape);

                if (item.shape == InvisibleObjectDrawer.Shape.SPHERE)
                    item.radius = EditorGUILayout.FloatField("Radius", item.radius);
                else
                {
                    item.position = EditorGUILayout.Vector3Field("Position offset", item.position);
                    item.scale = EditorGUILayout.Vector3Field("Scale", item.scale);
                }

                item.color = EditorGUILayout.ColorField("Color", item.color);
            }

            EditorGUILayout.EndToggleGroup();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(item);
            }
        }

    }

}