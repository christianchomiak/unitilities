// MyScriptEditor.cs
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TimeManager))]
public class TimeManagerEditor : Editor
{
    public void OnEnable()
    {
        //TimeManager myTarget = (TimeManager)target;     

        //if (myTarget.)
    }

    public override void OnInspectorGUI()
    {
        TimeManager myTarget = (TimeManager) target;

        int prevNumber = myTarget.numberOfLayers;

        myTarget.numberOfLayers = EditorGUILayout.IntField("Number of layers", myTarget.numberOfLayers);
        myTarget.numberOfLayers = (myTarget.numberOfLayers < 0 ? 0 : myTarget.numberOfLayers);

        if (myTarget.numberOfLayers == 0)
        {
            myTarget.layers.Clear();
            myTarget.scales.Clear();
            return;
        }

        int diff = myTarget.numberOfLayers - prevNumber;


        if (diff < 0) 
        {
            diff *= -1;

            if (diff > myTarget.layers.Count)
            {
                myTarget.layers.Clear();
                myTarget.scales.Clear();
            }
            else
            {
                myTarget.layers.RemoveRange(myTarget.layers.Count - 1 - diff, diff);
                myTarget.scales.RemoveRange(myTarget.scales.Count - 1 - diff, diff);
            }

        }
        else if (diff > 0)
        {
            for (int i = 0; i < diff; i++)
            {
                myTarget.layers.Add(TimeManager.TimeLayer.REALTIME);
                myTarget.scales.Add(1f);
            }
        }

        for (int i = 0; i < myTarget.numberOfLayers; i++)
        {
            GUILayout.Space(20);
            myTarget.layers[i] = (TimeManager.TimeLayer) EditorGUILayout.EnumPopup("Layer", myTarget.layers[i]);
            myTarget.scales[i] = EditorGUILayout.FloatField("Time Scale", myTarget.scales[i]);
            myTarget.scales[i] = (myTarget.scales[i] < 0f ? 0f : myTarget.scales[i]);
        }

                //myTarget.MyValue = EditorGUILayout.IntSlider("Val-you", myTarget.MyValue, 1, 10);
    }
}