/// <summary>
/// GUI3DObjectEditor v1.0.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Custom editor for GUI3DObject
/// </summary>

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Unitilities.UI
{

    [CanEditMultipleObjects]
    [CustomEditor(typeof(GUI3DObject))]
    public class GUI3DObjectEditor : Editor
    {
        Dictionary<string, Vector2> aspectRatios = new Dictionary<string, Vector2>()
    {
        { "4:3", new Vector2(4, 3)      },
        { "16:9", new Vector2(16, 9)    },
        { "3:2", new Vector2(3, 2)      },
        { "16:10", new Vector2(16, 10)  },
        { "5:4", new Vector2(5, 4)      },
        { "1:1", new Vector2(1, 1)      },
        { "Custom", new Vector2(-1, -1) },
        { "Camera", Vector2.zero        }
    };

        public override void OnInspectorGUI()
        {
            GUI3DObject item = (GUI3DObject) target;

            /*item.useScaleFeedback = EditorGUILayout.BeginToggleGroup("Use Scale Feedback", item.useScaleFeedback);
            item.feedbackScale = EditorGUILayout.FloatField("Feedback Scale", item.feedbackScale);
            EditorGUILayout.EndToggleGroup();*/

            item.useRelativeData = EditorGUILayout.BeginToggleGroup("Use Relative Data", item.useRelativeData);
            item.positionAnchor = (TextAnchor) EditorGUILayout.EnumPopup("Position Anchor", item.positionAnchor);
            item.anchorBounds = (GUI3DObject.AnchorBounds) EditorGUILayout.EnumPopup("Anchor Bounds", item.anchorBounds);
            item.relativePosition = EditorGUILayout.Vector2Field("Relative Position", item.relativePosition);

            item.relativeTo = (GUI3DObject.SizeRelation) EditorGUILayout.EnumPopup("Size Option", item.relativeTo);

            switch (item.relativeTo)
            {
                case GUI3DObject.SizeRelation.OneSideOfScreen:
                    item.selectedAxis = (GUI3DObject.ScreenAxis) EditorGUILayout.EnumPopup("Main Axis", item.selectedAxis);
                    item.relativeSingleSize = EditorGUILayout.FloatField("Relative Size", item.relativeSingleSize);

                    string[] aspectRatiosKeys = new string[aspectRatios.Count];
                    aspectRatios.Keys.CopyTo(aspectRatiosKeys, 0);

                    item._choiceIndex = EditorGUILayout.Popup("Ratio", item._choiceIndex, aspectRatiosKeys);

                    Vector2 customRatio = aspectRatios[aspectRatiosKeys[item._choiceIndex]];

                    if (customRatio == aspectRatios["Custom"])
                    {
                        item.customRatio = EditorGUILayout.Vector2Field("Custom Ratio", item.customRatio);
                    }
                    else
                    {
                        item.customRatio = customRatio;
                    }

                    break;
                case GUI3DObject.SizeRelation.BothSidesOfScreen:
                    item.relativeSize = EditorGUILayout.Vector2Field("Relative Size", item.relativeSize);
                    break;
                default:
                    break;
            }
            EditorGUILayout.EndToggleGroup();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(item);
            }
        }

    }

}