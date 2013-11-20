// C# example:
using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

using Microsoft.Win32;


//using System;
//using System.Diagnostics;
//using System.IO;
//using System.Xml;

public enum SortingType { Ascending, Descending };

public class PlayerPrefsEditor2 : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    
    float myFloat = 1.23f;

    List<PlayerPrefsValue> playerPrefs;
    Dictionary<PlayerPrefsValue, PlayerPrefsValue> originalPrefs;

    private Vector2 scrollPosition;

    private bool somethingWasDeleted;
    private string searchString = "";
    private SortingType sortingType = SortingType.Ascending;
    private SortingType prevSortingType;

    private Texture2D undoTexture, saveTexture, deleteTexture;

    private List<PlayerPrefsValue> toBeDeleted;

    private int updateFactor = 100;
    private int currentFrame = 0;

    private int currentNumberOfUpdates = 0;
    private int maxUpdatesPerSecond = 1;

    private bool autoRefresh = false;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/PlayerPrefs Editor (2)")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        PlayerPrefsEditor2 window = (PlayerPrefsEditor2) EditorWindow.GetWindow(typeof(PlayerPrefsEditor2));

        //window.somethingWasDeleted = false;
    }

    void OnEnable()
    {
    }

    void OnDisable()
    {
        //EditorPrefs.SetBool("myBool", myBool);
        //Debug.Log("Salvando: " + EditorPrefs.GetBool("myBool"));
    }

    void OnDestroy()
    {
    }

    void SavePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        foreach (PlayerPrefsValue pref in playerPrefs)
            pref.SaveToRealPrefs();
        
        RefreshPlayerPrefs();
    }

    void OnInspectorUpdate()
    {
        if (autoRefresh)
        {
            if (currentFrame == 0)
            {
                RefreshPlayerPrefs();
                Debug.Log("Updated");
            }
            Debug.Log("Current: " + currentFrame);

            currentFrame++;
            currentFrame %= 10;

            /*if (currentFrame >= updateFactor)
            {
                RefreshPlayerPrefs();
                Debug.Log("Update :D " + currentFrame);
                currentFrame = 0;
            }*/
        }
    }

    void Update()
    {
        /*currentFrame++;

        if (currentFrame >= updateFactor)
        {
            RefreshPlayerPrefs();
            Debug.Log("Update :D " + currentFrame);
            currentFrame = 0;
        }*/
    }

    void OnGUI()
    {
        if (playerPrefs == null)
            RefreshPlayerPrefs();

        if (toBeDeleted == null)
            toBeDeleted = new List<PlayerPrefsValue>();
        else
            toBeDeleted.Clear();


        GUILayout.BeginHorizontal(GUI.skin.FindStyle("Toolbar"));

        //GUILayout.FlexibleSpace();

        if (GUILayout.Button(new GUIContent("Delete All"), EditorStyles.toolbarButton, GUILayout.Width(64)))
        {
            PlayerPrefs.DeleteAll();
            RefreshPlayerPrefs();
            return;
        }

        sortingType = (SortingType)EditorGUILayout.EnumPopup(sortingType, EditorStyles.toolbarDropDown, GUILayout.MaxWidth(80));
        //sortingType = (SortingType)EditorGUILayout.EnumPopup(sortingType, EditorStyles.toolbarPopup, GUILayout.MaxWidth(80));
        //EditorGUILayout.Popup((int)sortingType, Enum.GetNames(typeof(SortingType)));
        GUILayout.FlexibleSpace();
        searchString = GUILayout.TextField(searchString, GUI.skin.FindStyle("ToolbarSeachTextField"), GUILayout.MaxWidth(100));
        if (GUILayout.Button("", GUI.skin.FindStyle("ToolbarSeachCancelButton")))
        {
            // Remove focus if cleared
            searchString = "";
            GUI.FocusControl(null);
        }
        
        autoRefresh = GUILayout.Toggle(autoRefresh, "Auto-refresh");

        if (GUILayout.Button(new GUIContent("Force Refresh"), EditorStyles.toolbarButton, GUILayout.Width(80)))
        {
            RefreshPlayerPrefs();
            return;
        }

        /*if (GUILayout.Button(new GUIContent("Force Refresh"), EditorStyles.toolbarButton, GUILayout.Width(64)))
        {
            RefreshPlayerPrefs();
            return;
        }*/
        GUILayout.EndHorizontal();

        if (undoTexture == null)
        {
            undoTexture = AssetDatabase.LoadAssetAtPath("Assets/Editor/EditorIcons/undo.png", typeof(Texture2D)) as Texture2D;
        }
        if (deleteTexture == null)
        {
            deleteTexture = AssetDatabase.LoadAssetAtPath("Assets/Editor/EditorIcons/delete.png", typeof(Texture2D)) as Texture2D;
        }
        if (saveTexture == null)
        {
            saveTexture = AssetDatabase.LoadAssetAtPath("Assets/Editor/EditorIcons/save.png", typeof(Texture2D)) as Texture2D;
        }


        GUILayout.Label("Player Preferences", EditorStyles.boldLabel);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        if (playerPrefs.Count == 0)
        {
            //GUILayout.Label("Currently there are no PlayerPrefs", EditorStyles.miniLabel);
            EditorGUILayout.HelpBox("No Player Preferences found", MessageType.Info);
        }
        else
        {

            if (sortingType != prevSortingType)
            {
                if (sortingType == SortingType.Ascending)
                {
                    playerPrefs.Sort(delegate(PlayerPrefsValue a, PlayerPrefsValue b)
                        {
                            return a.keyName.CompareTo(b.keyName);    
                        }
                    );
                }
                else
                {
                    playerPrefs.Sort(delegate(PlayerPrefsValue a, PlayerPrefsValue b)
                    {
                        return a.keyName.CompareTo(b.keyName);
                    }
                    );
                    playerPrefs.Reverse();
                }
            }
            prevSortingType = sortingType;

            foreach(PlayerPrefsValue pref in playerPrefs)
            {
                if (!pref.keyName.ToLowerInvariant().Contains(searchString))
                    continue;

                GUILayout.BeginHorizontal();
                
                pref.isSelected = EditorGUILayout.Toggle(pref.isSelected, GUILayout.MaxWidth(16));

                //GUILayout.Label(pref.keyName, GUILayout.MinWidth(75), GUILayout.MaxWidth(100));

                if (pref.IsDifferent(originalPrefs[pref]))
                    GUI.skin.font = EditorStyles.boldFont;
                else
                    GUI.skin.font = EditorStyles.standardFont;

                pref.keyName = GUILayout.TextField(pref.keyName, GUILayout.MinWidth(75), GUILayout.MaxWidth(100));

                switch (pref.valueType)
                {
                    case PlayerPrefsTypes.Int:
                        pref.intValue = EditorGUILayout.IntField(pref.intValue, EditorStyles.textField, GUILayout.MaxWidth(150));
                        break;
                    case PlayerPrefsTypes.Float:
                        pref.floatValue = EditorGUILayout.FloatField(pref.floatValue, EditorStyles.textField, GUILayout.MaxWidth(150));
                        break;
                    case PlayerPrefsTypes.String:
                        pref.stringValue = EditorGUILayout.TextField(pref.stringValue, EditorStyles.textField, GUILayout.MaxWidth(150));
                        break;
                    default:
                        pref.stringValue = EditorGUILayout.TextField(pref.stringValue, EditorStyles.textField, GUILayout.MaxWidth(150));
                        break;
                }
                
                if (GUILayout.Button(new GUIContent(saveTexture, "Save this preference"), GUILayout.MaxHeight(24), GUILayout.Width(24)))
                {
                    originalPrefs[pref].CopyFrom(pref);
                    GUI.FocusControl(null);
                    Debug.Log("Saved!!");
                }
                else if (GUILayout.Button(new GUIContent(undoTexture, "Revert this preference"), GUILayout.MaxHeight(24), GUILayout.Width(24)))
                {
                    //revertThisPref = pref;
                    pref.CopyFrom(originalPrefs[pref]);
                    GUI.FocusControl(null);
                    Debug.Log("Restored!!");
                }
                else if (GUILayout.Button(new GUIContent(deleteTexture, "Delete this preference"), GUILayout.MaxHeight(24), GUILayout.Width(24)))
                {
                    pref.toBeDeleted = true;
                    toBeDeleted.Add(pref);
                    somethingWasDeleted = true;
                }

                GUILayout.EndHorizontal();
                GUI.skin.font = EditorStyles.standardFont;
            }

            foreach(PlayerPrefsValue pref in toBeDeleted)
            {
                if (pref.toBeDeleted)
                {
                    playerPrefs.Remove(pref);
                    originalPrefs.Remove(pref);
                    PlayerPrefs.DeleteKey(pref.keyName);
                }
            }
            /*if (toBeDeleted.Count != 0)
            {
                SavePlayerPrefs();
            }*/


            /*if (GUI.changed)
            {
                EditorUtility.SetDirty(this);
            }*/
        }

        /*GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();*/

        GUILayout.EndScrollView();
    }

    void RefreshPlayerPrefs()
    {
        somethingWasDeleted = false;

        if (playerPrefs == null)
            playerPrefs = new List<PlayerPrefsValue>();
        else
            playerPrefs.Clear();

        if (originalPrefs == null)
            originalPrefs = new Dictionary<PlayerPrefsValue, PlayerPrefsValue>();
        else
            originalPrefs.Clear();


        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            GetPlayerPrefsWindows();
        }
        else if (Application.platform == RuntimePlatform.OSXEditor)
        {
            Debug.Log("Coming soon");
        }
    }

    void GetPlayerPrefsWindows()
    {
        //In Windows, Unity stores the PlayerPrefs in the Registry, in a key identifiable by the Company and Product name of the game.
        string regKey = @"Software\" + PlayerSettings.companyName + @"\" + PlayerSettings.productName;

        RegistryKey key = Registry.CurrentUser.OpenSubKey(regKey);

        if (key == null)
        {
            Debug.Log("No keys found in registry.");
            return;
        }

        foreach (string subkeyName in key.GetValueNames())
        {
            string keyName = subkeyName.Substring(0, subkeyName.LastIndexOf("_"));
            string val = key.GetValue(subkeyName).ToString();
            
            int testInt = -1;
            bool couldBeInt = int.TryParse(val, out testInt);

            if (!float.IsNaN(PlayerPrefs.GetFloat(keyName, float.NaN)))
            {
                PlayerPrefsValue newPref = new PlayerPrefsValue(keyName, PlayerPrefsTypes.Float, PlayerPrefs.GetFloat(keyName));
                playerPrefs.Add(newPref);
                originalPrefs.Add(newPref, new PlayerPrefsValue(keyName, PlayerPrefsTypes.Float, PlayerPrefs.GetFloat(keyName))); 
            }
            else if (couldBeInt && (PlayerPrefs.GetInt(keyName, testInt - 10) == testInt))
            {
                PlayerPrefsValue newPref = new PlayerPrefsValue(keyName, PlayerPrefsTypes.Int, PlayerPrefs.GetInt(keyName));
                playerPrefs.Add(newPref);
                originalPrefs.Add(newPref, new PlayerPrefsValue(keyName, PlayerPrefsTypes.Int, PlayerPrefs.GetInt(keyName)));
            }
            else
            {
                PlayerPrefsValue newPref = new PlayerPrefsValue(keyName, PlayerPrefsTypes.String, PlayerPrefs.GetString(keyName));
                playerPrefs.Add(newPref);
                originalPrefs.Add(newPref, new PlayerPrefsValue(keyName, PlayerPrefsTypes.String, PlayerPrefs.GetString(keyName)));
            }
        }
    }

}