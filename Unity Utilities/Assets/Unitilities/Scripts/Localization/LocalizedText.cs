/// <summary>
/// LocalizedText v1.1.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// MonoBehaviour that makes use of the LocalizationManager and modify 
/// the sibling UI Text as needed.
/// 
/// For Unity 4.6+ only.
/// </summary>

using UnityEngine;
using UnityEngine.UI;

namespace Unitilities.Localization
{
    /// <summary>
    /// MonoBehaviour that makes use of the LocalizationManager
    /// and modify the sibling UI Text as needed.
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class LocalizedText : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        Text textField;

        [SerializeField]
        LocalizationKeyword content;

        [SerializeField]
        LocalizationPreference localizationPreference = LocalizationPreference.DEFAULT;

        [SerializeField]
        LocalizationLanguage customLanguage = LocalizationLanguage.EN_US;

        #endregion



        #region Accessors

        public LocalizationLanguage CustomLanguage
        {
            get { return customLanguage; }
        }

        #endregion



        #region Unity Functions

        void Awake()
        {
            if ((object)textField == null)
            {
                textField = GetComponent<Text>();
            }
        }

        void OnEnable()
        {
            GetTextTranslated();
        }

        #endregion



        #region Private& Protected Functions

        void GetTextTranslated()
        {
            string newValue = "";

            switch (localizationPreference)
            {
                case LocalizationPreference.DEFAULT:
                    goto case LocalizationPreference.SYSTEM;
                case LocalizationPreference.SYSTEM:
                    newValue = LocalizationManager.Instance.Translate(content, localizationPreference);
                    break;
                case LocalizationPreference.CUSTOM:
                    newValue = LocalizationManager.Instance.CustomTranslate(content, customLanguage);
                    break;
                default:
                    Debug.LogWarning("Preference type \"" + localizationPreference.ToString() + "\" could not be found. The default language will be used instead.");
                    newValue = LocalizationManager.Instance.Translate(content, LocalizationPreference.DEFAULT);
                    break;
            }

            textField.text = newValue;
        }

        #endregion


        #region Public Functions

        public void SetTextColor(Color newColor)
        {
            textField.color = newColor;
        }

        public void SetTextContent(LocalizationKeyword newType)
        {
            content = newType;
            GetTextTranslated();
        }

        public void SetCustomLanguage(LocalizationLanguage newLanguage)
        {
            customLanguage = newLanguage;
            if (localizationPreference == LocalizationPreference.CUSTOM)
                GetTextTranslated();
        }

        #endregion
    }

}