using UnityEngine;
using UnityEditor;

public class UnitilitiesMenu : ScriptableObject
{
    // Add a menu item named "Do Something with a Shortcut Key" to MyMenu in the menu bar
    // and give it a shortcut (ctrl-g on Windows, cmd-g on OS X).
    [MenuItem("Unitilities/GameObject/Enable-Disable %h")]
    static void DoIt()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            //EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
            bool activeStatus = go.activeSelf;

            go.SetActive(!activeStatus);
        }
    }

    [MenuItem("Unitilities/GameObject/Enable-Disable %h", true)]
    static bool ValidateSelection()
    {
        return Selection.transforms.Length != 0;
    }
}