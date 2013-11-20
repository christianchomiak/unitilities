using UnityEngine;
using System.Collections;

public enum PlayerPrefsTypes { Int, Float, String };

public class PlayerPrefsValue
{
    public int intValue;
    public float floatValue;
    public string stringValue;

    public PlayerPrefsTypes valueType;

    public bool toBeDeleted;
    public bool isSelected;
    public string keyName;

    public void CopyFrom(PlayerPrefsValue p)
    {
        keyName = p.keyName;
        valueType = p.valueType;

        intValue = p.intValue;
        floatValue = p.floatValue;
        stringValue = p.stringValue;
    }

    public bool IsDifferent(PlayerPrefsValue p)
    {
        if (keyName.CompareTo(p.keyName) != 0)
            return true;

        bool isDifferent = false;

        switch (valueType)
        {
            case PlayerPrefsTypes.Int:
                isDifferent = intValue != p.intValue;
                break;
            case PlayerPrefsTypes.Float:
                isDifferent = floatValue != p.floatValue;
                break;
            case PlayerPrefsTypes.String:
                isDifferent = stringValue.CompareTo(p.stringValue) != 0;
                break;
            default:
                isDifferent = true;
                break;
        }

        return isDifferent;
    }

    public PlayerPrefsValue(string _keyName, PlayerPrefsTypes _type, int _value)
        : this(_keyName, _type, _value, 0f, "")
    {
    }

    public PlayerPrefsValue(string _keyName, PlayerPrefsTypes _type, float _value)
        : this(_keyName, _type, -1, _value, "")
    {
    }

    public PlayerPrefsValue(string _keyName, PlayerPrefsTypes _type, string _value)
        : this(_keyName, _type, -1, 0f, _value)
    {
    }

    protected PlayerPrefsValue(string _keyName, PlayerPrefsTypes _type, int _intValue, float _floatValue, string _stringValue)
    {
        keyName = _keyName;
        valueType = _type;

        intValue = _intValue;
        floatValue = _floatValue;
        stringValue = _stringValue;

        toBeDeleted = false;
        isSelected = false;
    }
    	
    public void SaveToRealPrefs()
    {
        switch (valueType)
        {
            case PlayerPrefsTypes.Int:
                PlayerPrefs.SetInt(keyName, intValue);
                break;
            case PlayerPrefsTypes.Float:
                PlayerPrefs.SetFloat(keyName, floatValue);
                break;
            case PlayerPrefsTypes.String:
                PlayerPrefs.SetString(keyName, stringValue);
                break;
            default:
                PlayerPrefs.SetInt(keyName, intValue);
                break;
        }
    }


}
