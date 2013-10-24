using UnityEngine;
using System.Collections;

//TO-DO: customize the GUI Style of the displayed text
public class ObjectTextDrawer : MonoBehaviour
{
    enum ObjectTextOptions { Name, Position, CustomText };

    [SerializeField]
    ObjectTextOptions write;

    [SerializeField]
    string customText;
    
    void Awake()
    {
        if (!Utilities.PlatformIsEditor())
            Destroy(this);
    }
	
    #if UNITY_EDITOR

        /// <summary>
        /// Draw a sphere to signal the object's position on editor view
        /// </summary>
        void OnDrawGizmos()
        {
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

            UnityEditor.Handles.Label(transform.position, text);
        }

    #endif
}
