/// <summary>
/// LocalizationConfig v1.1.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// MonoBehaviour that makes use of the LocalizationManager and modify 
/// the sibling UI Text as needed.
/// 
/// Requires the SimpleJSON plugin: http://wiki.unity3d.com/index.php/SimpleJSON
/// To use a different method, please change LoadData() in the LocalizationData struct.
/// </summary>

using SimpleJSON;
using UnityEngine;
using System.Collections.Generic;

namespace Unitilities.Localization
{
    //[WARNING]
    //For you own sanity, do not change any enum after beginning a project as
    //you'll need to update all values in the inspector that make reference to them.

    #region Personalization - You can change these

    //These should be the first elements of the tuples defined in the DATA
    //section of the language files.
    /// <summary>
    /// The different types of text that can be used.
    /// </summary>
    public enum LocalizationKeyword
    {
        ERROR,           //Required, do not delete
        LANGUAGE_NAME,   //Not required but useful to store the name of the language in its native form
        /*
         * Your custom keywords here
         */
    }

    /// <summary>
    /// The different languages supported in the game.
    /// </summary>
    public enum LocalizationLanguage
    {
        //Language IDs can be anything, but it's recommended using a convention.
        //For example: https://msdn.microsoft.com/en-us/library/ee825488(v=cs.20).aspx
        EN_US,
        ES_MX,
        FR_FR,
        PT_BR,
        DE_DE,
        IT_IT,
        RU_RU,
        AR_SA,
        JA_JP,
        KO_KR,
        ZH_CHS
    }

    #endregion


    #region Core - You can't change these

    /// <summary>
    /// Type of localizations:
    ///    * Default: the "native" language of the game
    ///    * System: the language of the device
    ///    * Custom: a specific language set by the developer
    /// NOTE: Custom has higher precedence than Default and System
    /// </summary>
    public enum LocalizationPreference
    {
        DEFAULT,
        SYSTEM,
        CUSTOM
    }

    /// <summary>
    /// LTR: Left-to-Right
    /// RTL: Right-to-Left
    /// </summary>
    public enum WritingSystem
    {
        LTR,
        RTL
    }


    /// <summary>
    /// Container of the translations for each language
    /// </summary>
    [System.Serializable]
    public struct LocalizationData
    {
        #region Fields

        [SerializeField, HideInInspector]
        string name;

        [ReadOnly, SerializeField]
        LocalizationLanguage language;

        //File that stores the translated data
        //[IMPORTANT] THESE FILES SHOULD BE IN UTF-8 IF THE LANGUAGE USES ANY FOREIGN CHARACTER TO ENGLISH [/IMPORTANT]
        [SerializeField]
        TextAsset dataFile;

        [SerializeField]
        WritingSystem writingSystem;

        Dictionary<LocalizationKeyword, string> data;

        #endregion


        #region Accessors

        public LocalizationLanguage Language
        {
            get { return language; }
        }

        public TextAsset DataFile
        {
            get { return dataFile; }
        }

        Dictionary<LocalizationKeyword, string> Data
        {
            get
            {
                if (data == null)
                    data = new Dictionary<LocalizationKeyword, string>();

                return data;
            }
        }

        #endregion


        #region Constructors

        public LocalizationData(LocalizationLanguage languageID, string languageName, TextAsset newDataFile)
        {
            language = languageID;
            dataFile = newDataFile;
            data = new Dictionary<LocalizationKeyword, string>();
            name = languageName;
            writingSystem = WritingSystem.LTR;
        }

        #endregion


        #region Private Functions

        void AddData(LocalizationKeyword type, string value)
        {

            if (Data.ContainsKey(type))
            {
                Data[type] = value;
            }
            else
            {
                Data.Add(type, value);
            }
        }


        #endregion


        #region API / Public Functions

        /// <summary>
        /// Loads the translator with the data holded inside its DataFile.
        /// </summary>
        public void LoadData()
        {
            //languages = new Dictionary<LocalizationLanguage, LocalizationData>();

            TextAsset localizationData = dataFile;

            if ((object) localizationData == null) //System.IO.File.Exists(levelPath))
            {
                Debug.LogError(@"I/O ERROR: Couldn't  load the data file for """ + language.ToString() + @""". Is the field assigned?");
                //string level_text = System.IO.File.ReadAllText(levelPath);   
                return;
            }

            string localizationText = localizationData.text;
            Debug.Log(@"Loading the data file for """ + language.ToString() + @"""");

            var rawLanguageData = JSON.Parse(localizationText);

            if ((object) rawLanguageData == null)
            {
                Debug.LogWarning("JSON is empty");
                return;
            }

            //Not used in the code yet. Commented to avoid warnings.
            string name = rawLanguageData["NAME"].Value;
            string id = rawLanguageData["ID"].Value;

            //string scriptType = rawLanguageData["SCRIPT"].Value;
            //The writing system is defined on the Inspector, but it could
            //be read from the data file and determined using this code.
            /*if (scriptType == "RTL")
                writingSystem = WritingSystem.RTL;
            else
                writingSystem = WritingSystem.LTR;*/


            //TODO: Check if the id key exists
            //LocalizationLanguage newLanguageID = languagesIDTable[id.ToUpper()];
            //LocalizationData newData = new LocalizationData(newLanguageID, null, null); //id

            var data = rawLanguageData["DATA"];

            for (int i = 0; i < data.Count; i++)
            {
                var dataValue = data[i];
                string keyword = dataValue[0].Value;
                string translation = dataValue[1].Value;

                if (writingSystem == WritingSystem.RTL)
                    translation.Reverse(); 

                //TODO: Check if the type key exists
                AddData(LocalizationManager.Instance.GetDataType(keyword), translation);
            }

            //languages.Add(newLanguageID, newData);
        }

        /// <summary>
        /// Get the translation of a provided text
        /// </summary>
        /// <param name="request">Text to translate</param>
        /// <returns>The string representing the translation of the request</returns>
        public string Translate(LocalizationKeyword request)
        {
            string value = "";

            if (Data.TryGetValue(request, out value))
            {
                return value;
            }

            Debug.LogError("There's no record of a text of type \"" + request.ToString() + "\" in the language \"" + language.ToString() + "\".");

            return null;
        }

        #endregion
    }

    #endregion

}