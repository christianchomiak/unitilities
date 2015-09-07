/// <summary>
/// UnitilitiesMenu v1.0.1 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Script to show quick Unitilities-related shortcuts in the Unity Editor.
/// </summary>

using UnityEngine;
using UnityEditor;

namespace Unitilities
{
    public class UnitilitiesMenu : ScriptableObject
    {
        // Add a menu item named "Do Something with a Shortcut Key" to MyMenu in the menu bar
        // and give it a shortcut (ctrl-g on Windows, cmd-g on OS X).
        [MenuItem("Unitilities/GameObject/Enable-Disable %h")]
        static void ChangeEnableStatus()
        {
            foreach (GameObject go in Selection.gameObjects)
            {
                //EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
                go.SetActive(!go.activeSelf);
            }
        }

        [MenuItem("Unitilities/GameObject/Enable-Disable %h", true)]
        static bool ValidateSelection()
        {
            return Selection.transforms.Length != 0;
        }
    }
}