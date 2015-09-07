using UnityEngine;

namespace Unitilities.Debugging
{
    public enum PlayerPrefsType { Int, Float, String };

    public class PlayerPrefsDataType
    {
        public int intValue;
        public float floatValue;
        public string stringValue;

        public PlayerPrefsType valueType;

        //public bool toBeDeleted;
        public bool isSelected;
        public string keyName;

        public int prevKeyNameLength;

        public void CopyFrom(PlayerPrefsDataType p)
        {
            keyName = p.keyName;
            valueType = p.valueType;

            intValue = p.intValue;
            floatValue = p.floatValue;
            stringValue = p.stringValue;
        }

        public bool IsDifferent(PlayerPrefsDataType p)
        {
            if (keyName.CompareTo(p.keyName) != 0)
                return true;

            bool isDifferent = false;

            switch (valueType)
            {
                case PlayerPrefsType.Int:
                    isDifferent = intValue != p.intValue;
                    break;
                case PlayerPrefsType.Float:
                    isDifferent = floatValue != p.floatValue;
                    break;
                case PlayerPrefsType.String:
                    isDifferent = stringValue.CompareTo(p.stringValue) != 0;
                    break;
                default:
                    isDifferent = true;
                    break;
            }

            return isDifferent;
        }

        public PlayerPrefsDataType(string _keyName, PlayerPrefsType _type, int _value)
            : this(_keyName, _type, _value, 0f, "")
        {
        }

        public PlayerPrefsDataType(string _keyName, PlayerPrefsType _type, float _value)
            : this(_keyName, _type, -1, _value, "")
        {
        }

        public PlayerPrefsDataType(string _keyName, PlayerPrefsType _type, string _value)
            : this(_keyName, _type, -1, 0f, _value)
        {
        }

        protected PlayerPrefsDataType(string _keyName, PlayerPrefsType _type, int _intValue, float _floatValue, string _stringValue)
        {
            keyName = _keyName;
            valueType = _type;

            intValue = _intValue;
            floatValue = _floatValue;
            stringValue = _stringValue;

            isSelected = false;
        }

        public void SaveToRealPrefs()
        {
            switch (valueType)
            {
                case PlayerPrefsType.Int:
                    PlayerPrefs.SetInt(keyName, intValue);
                    break;
                case PlayerPrefsType.Float:
                    PlayerPrefs.SetFloat(keyName, floatValue);
                    break;
                case PlayerPrefsType.String:
                    PlayerPrefs.SetString(keyName, stringValue);
                    break;
                default:
                    PlayerPrefs.SetInt(keyName, intValue);
                    break;
            }
        }


    }

}