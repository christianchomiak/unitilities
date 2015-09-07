/// <summary>
/// LocalizationManager v1.1.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Manager in charge of translating the LocalizedTexts.
/// 
/// </summary>

using UnityEngine;
using System.Collections.Generic;
//using System.Text;


namespace Unitilities.Localization
{
    public class LocalizationManager : Singleton<LocalizationManager>
    {
        #region Public & Serialized Fields

        [Header("Localization Manager")]

        [Tooltip("The native language")]
        [SerializeField]
        LocalizationLanguage defaultLanguage = LocalizationLanguage.EN_US;

        [Tooltip("If true, the language of the device will be used instead of the default")]
        [SerializeField]
        bool preferSystemLanguage = false;
        
        /// <summary>
        /// Initial data from which the manager build upon.
        /// </summary>
        [SerializeField]
        LocalizationData[] startingLanguageData = new LocalizationData[]
        {
            new LocalizationData(LocalizationLanguage.EN_US, "English", null),
            new LocalizationData(LocalizationLanguage.ES_MX, "Spanish", null),
            new LocalizationData(LocalizationLanguage.FR_FR, "French", null),
            new LocalizationData(LocalizationLanguage.PT_BR, "Portuguese", null),
            new LocalizationData(LocalizationLanguage.DE_DE, "German", null),
            new LocalizationData(LocalizationLanguage.IT_IT, "Italian", null),
            new LocalizationData(LocalizationLanguage.RU_RU, "Russian", null),
            new LocalizationData(LocalizationLanguage.AR_SA, "Arabic", null),
            new LocalizationData(LocalizationLanguage.JA_JP, "Japanese", null),
            new LocalizationData(LocalizationLanguage.KO_KR, "Korean", null),
            new LocalizationData(LocalizationLanguage.ZH_CHS, "Chinese (Simplified)", null)
        }; 

        #endregion


        #region Private & Protected Fields

        //The "startingLanguageData" is converted to this dictionary at runtime.
        //It is possible to use the array instead and the LocalizationLanguage enum
        //as the index value, but it'll need that the order of the elements of the array 
        //be in the order of such enum values.
        Dictionary<LocalizationLanguage, LocalizationData> languages;

        /// <summary>
        /// The language of the device
        /// </summary>
        LocalizationLanguage systemLanguage;

        /// <summary>
        /// Transformation table between a string and the language ID that represents it
        /// </summary>
        Dictionary<string, LocalizationLanguage> languagesIDTable;

        /// <summary>
        /// Transformation table between a string and the text content that represents it
        /// </summary>
        Dictionary<string, LocalizationKeyword> localizationTypeTable;

        /// <summary>
        /// Has the LocalizationManager finished the boot process?
        /// </summary>
        protected bool alreadyInitialized = false;

        #endregion


        #region Accessors

        public LocalizationLanguage DefaultLanguage
        {
            get { return preferSystemLanguage ? systemLanguage : defaultLanguage; }
        }

        public LocalizationLanguage SysLanguage
        {
            get { return systemLanguage; }
        }

        #endregion
        

        #region Unity Functions

        protected override void Awake()
        {
            base.Awake();
        }

        #endregion
        

        #region Private & Protected Functions

        /// <summary>
        /// One function to boot them all.
        /// </summary>
        void InitializeSystems()
        {
            InitializeLanguagesIDDictionary();
            InitializeLocalizationKeywordDictionary();
            GetSystemLanguage();
            LoadLocalizationData();

            alreadyInitialized = true;
        }

        /// <summary>
        /// Creates the conversion table between string and language ID
        /// </summary>
        void InitializeLanguagesIDDictionary()
        {
            languagesIDTable = new Dictionary<string, LocalizationLanguage>();

            System.Array A = System.Enum.GetValues(typeof(LocalizationLanguage));
            if (A.Length == 0)
            {
                throw new System.ArgumentException("Enum " + typeof(LocalizationLanguage).ToString() + " is empty", "array");
            }

            for (int i = 0; i < A.Length; i++)
            {
                LocalizationLanguage locLan = (LocalizationLanguage) A.GetValue(i);
                languagesIDTable.Add(locLan.ToString(), locLan);
            }
        }

        /// <summary>
        /// Creates the conversion table between string and content type
        /// </summary>
        void InitializeLocalizationKeywordDictionary()
        {
            localizationTypeTable = new Dictionary<string, LocalizationKeyword>();
            
            System.Array A = System.Enum.GetValues(typeof(LocalizationKeyword));
            if (A.Length == 0)
            {
                throw new System.ArgumentException("Enum " + typeof(LocalizationKeyword).ToString() + " is empty", "array");
            }

            for (int i = 0; i < A.Length; i++)
            {
                LocalizationKeyword locType = (LocalizationKeyword) A.GetValue(i);
                localizationTypeTable.Add(locType.ToString(), locType);
            }
        }

        /// <summary>
        /// Determines the language of the device and stores it.
        /// </summary>
        void GetSystemLanguage()
        {
            //Debug.Log(@"Culture name: " + System.Globalization.CultureInfo.CurrentCulture.Name + @"""");
            Debug.Log(@"System Language: " + Application.systemLanguage.ToString() + @"""");
            
            switch (Application.systemLanguage)
            {
                case SystemLanguage.English:
                    systemLanguage = LocalizationLanguage.EN_US;
                    break;
                case SystemLanguage.Spanish:
                    systemLanguage = LocalizationLanguage.ES_MX;
                    break;
                case SystemLanguage.Italian:
                    systemLanguage = LocalizationLanguage.IT_IT;
                    break;
                case SystemLanguage.German:
                    systemLanguage = LocalizationLanguage.DE_DE;
                    break;
                case SystemLanguage.Portuguese:
                    systemLanguage = LocalizationLanguage.PT_BR;
                    break;
                case SystemLanguage.French:
                    systemLanguage = LocalizationLanguage.FR_FR;
                    break;
                case SystemLanguage.Japanese:
                    systemLanguage = LocalizationLanguage.JA_JP;
                    break;
                case SystemLanguage.Chinese:
                    systemLanguage = LocalizationLanguage.ZH_CHS;
                    break;
                case SystemLanguage.Arabic:
                    systemLanguage = LocalizationLanguage.AR_SA;
                    break;
                case SystemLanguage.Russian:
                    systemLanguage = LocalizationLanguage.RU_RU;
                    break;
                case SystemLanguage.Korean:
                    systemLanguage = LocalizationLanguage.KO_KR;
                    break;
                case SystemLanguage.Unknown:
                    Debug.LogWarning("The language of the device is UNKNOWN. The default language will be used instead.");
                    systemLanguage = defaultLanguage;
                    break;
                default:
                    Debug.LogWarning("The SysLanguage \"" + Application.systemLanguage.ToString() + "\" does not exist. The default language will be used instead.");
                    systemLanguage = defaultLanguage;
                    break;
            }
        }

        void LoadLocalizationData()
        {
            languages = new Dictionary<LocalizationLanguage, LocalizationData>();

            for (int i = 0; i < startingLanguageData.Length; i++)
            {
                //LoadLanguageLocalizationData(startingLanguageData[i]);
                startingLanguageData[i].LoadData();
                languages.Add(startingLanguageData[i].Language, startingLanguageData[i]);
            }
        }

        #endregion


        #region Public Functions

        /// <summary>
        /// Returns the Content type that matches the desired one
        /// </summary>
        /// <param name="contentString"></param>
        /// <returns></returns>
        public LocalizationKeyword GetDataType(string contentString)
        {
            LocalizationKeyword type = LocalizationKeyword.ERROR;

            if (localizationTypeTable.TryGetValue(contentString, out type))
            {
                return type;
            }

            Debug.LogError("The TextType \"" + contentString + "\" does not exist.");
            return LocalizationKeyword.ERROR;
        }

        /// <summary>
        /// Get the translation of a specified text on a certain language
        /// </summary>
        /// <param name="content">Text to translate</param>
        /// <param name="preference">How should the text be translated?</param>
        /// <returns></returns>
        public string Translate(LocalizationKeyword content, LocalizationPreference preference)
        {
            if (!alreadyInitialized)
                InitializeSystems();

            switch (preference)
            {
                case LocalizationPreference.DEFAULT:
                    if (preferSystemLanguage)
                        goto case LocalizationPreference.SYSTEM;
                    else
                        return CustomTranslate(content, defaultLanguage);
                /*if (Languages.TryGetValue(defaultLanguage, out startingLanguageData))
                    return startingLanguageData.CustomTranslate(request);
                break;*/
                case LocalizationPreference.SYSTEM:
                    return CustomTranslate(content, systemLanguage);
                /*if (Languages.TryGetValue(systemLanguage, out startingLanguageData))
                    return startingLanguageData.CustomTranslate(request);
                break;*/
                case LocalizationPreference.CUSTOM:
                    Debug.LogWarning("Translations using CUSTOM as a preference should use CustomTranslate instead. The default language will be used instead.");
                    goto case LocalizationPreference.DEFAULT;
                default:
                    Debug.LogWarning("Preference type \"" + preference.ToString() + "\" could not be found. The default language will be used instead.");
                    goto case LocalizationPreference.DEFAULT;
            }
        }

        /// <summary>
        /// Get the translation of a specified text on a certain language
        /// </summary>
        /// <param name="content">Text to translate</param>
        /// <param name="customLanguage">To what language will the text be translated to?</param>
        /// <returns></returns>
        public string CustomTranslate(LocalizationKeyword content, LocalizationLanguage customLanguage)
        {
            if (!alreadyInitialized)
                InitializeSystems();

            LocalizationData data;

            if (languages.TryGetValue(customLanguage, out data))
            {
                string result = data.Translate(content);

                //Check if the consulted language has a translation for such text
                if (result == null)
                {
                    //If the consulted language is the one set as default or fallback, return "ERROR".
                    if (customLanguage == defaultLanguage)
                    {
                        string contentString = content.ToString();
                        Debug.LogError("There's no match in the language \"" + customLanguage.ToString() + "\" for \"" + contentString + "\"");
                        return contentString;
                    }
                    else
                    {
                        return CustomTranslate(content, defaultLanguage);
                    }
                }

                return result;
            }

            if (customLanguage == defaultLanguage)
            {
                Debug.LogError("There's no record of the language \"" + customLanguage.ToString() + "\".");
                return "[ERROR]";
            }
            else
            {
                Debug.LogError("There's no record of the language \"" + customLanguage.ToString() + "\". The default language will be used.");
                return CustomTranslate(content, defaultLanguage);
            }
        }

        /// <summary>
        /// Changes the default language of the manager.
        /// </summary>
        /// <param name="newDefaultLanguage">New language to be used as the default</param>
        /// <param name="overrideUseSystem">Wether the system language will have greater precedence</param>
        public void SetDefaultLanguage(LocalizationLanguage newDefaultLanguage, bool overrideUseSystem = true)
        {
            defaultLanguage = newDefaultLanguage;
            if (overrideUseSystem)
                preferSystemLanguage = false;
        }

        #endregion

    }
}