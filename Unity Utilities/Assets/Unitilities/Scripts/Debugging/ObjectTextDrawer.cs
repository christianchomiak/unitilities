/// <summary>
/// ObjectTextDrawer v1.0.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Script to show a text, above a GameObject, ingame.
/// 
/// Only to be used during development in the Editor.
/// </summary>

using UnityEngine;

namespace Unitilities.Debugging
{
    //using Helpers;

    //TO-DO: customize the GUI Style of the displayed text
    /// <summary>
    /// Script that shows info on screen above a GameObject (only works in the Editor)
    /// </summary>
    public class ObjectTextDrawer : MonoBehaviour
    {
        public enum ObjectTextOptions { Name, Position, CustomText };

        public bool isEnabled = true;

        public ObjectTextOptions write;

        public string customText;

        //[Range(15, 50)]
        public int textSize;

        public bool boldText;

        public bool italicText;

        public Color textColor = Color.black;

        public Color backgroundColor;

        /*[SerializeField]
        Vector3 textPosition;*/

        void Awake()
        {
            if (!ApplicationHelper.PlatformIsEditor)
                Destroy(this);

        }

#if UNITY_EDITOR

        /// <summary>
        /// Draw a sphere to signal the object's position on editor view
        /// </summary>
        void OnDrawGizmos()
        {
            if (!isEnabled)
                return;

            if (textColor.a < 0.05f)
                Debug.LogWarning("Text hsvColor of text in " + gameObject.name + " may not be totally visible. Check its customAlpha channel in the hsvColor picker.");

            /*if (backgroundColor.a < 0.05f)
                Debug.LogWarning("Background hsvColor of text in " + gameObject.name + " may not be totally visible. Check its customAlpha channel in the hsvColor picker.");*/

            GUIStyle gs = new GUIStyle();
            gs.normal.textColor = textColor;
            gs.fontSize = textSize;

            if (boldText && italicText)
                gs.fontStyle = FontStyle.BoldAndItalic;
            else if (boldText)
                gs.fontStyle = FontStyle.Bold;
            else if (italicText)
                gs.fontStyle = FontStyle.Italic;

            //Texture2D t = Utilities.CreateBackgroundTexture(backgroundColor);
            Texture2D t = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            t.SetPixel(0, 0, backgroundColor);
            t.Apply();
            t.hideFlags = HideFlags.DontSave;

            gs.normal.background = t;

            string text = "";
            switch (write)
            {
                case ObjectTextOptions.Name:
                    text = gameObject.name;
                    break;
                case ObjectTextOptions.Position:
                    text = transform.position.ToString();
                    break;
                case ObjectTextOptions.CustomText:
                    text = customText;
                    break;
                default:
                    text = gameObject.name;
                    break;
            }

            UnityEditor.Handles.Label(transform.position, text, gs);

            DestroyImmediate(t);
        }

#endif
    }

}