/// <summary>
/// Singleton v1.2.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Base class to provide a singleton status to an object.
/// 
/// According to Wikipedia:
///     "The singleton pattern is a design pattern that restricts the instantiation of a class to one object. 
///      This is useful when exactly one object is needed to coordinate actions across the system."
///      
/// All singletons have also an option that can be marked in the Inspector to keep them alive whenever the current
/// scene changes in Unity. 
/// </summary>

using UnityEngine;

namespace Unitilities
{

    /// <summary>
    /// Remember to call base.Awake() in each new Singleton
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T instance;

        [Header("Singleton properties")]

        /// <summary>
        /// If true, the singleton won't be destroyed when the scene changes
        /// </summary>
        [Tooltip("If true, the singleton won't be destroyed when the scene changes")]
        [SerializeField]
        bool isPersistent = true;

        #region Unity

        protected virtual void Awake()
        {
            if (Exists)
            {
                if (!IsCurrentSingleton)
                {
                    Debug.LogWarning("Warning: More than one instance of singleton " + typeof(T) + " exists in the scene. The newest one will be destroyed.");
                    Destroy(gameObject);
                }
            }
            else
            {
                // Previously, it was `instance = gameObject.GetComponent<T>();` but this should be more efficient
                instance = this as T;

                if (isPersistent)
                    DontDestroyOnLoad(gameObject);
            }
        }

        //Only the true singleton can reset the global reference
        protected virtual void OnDestroy()
        {
            if (IsCurrentSingleton)
                instance = null;
        }

        #endregion

        #region Private & Protected

        /// <summary>
        /// Determines whether the current instance is the one true singleton
        /// </summary>
        /// <returns></returns>
        protected bool IsCurrentSingleton
        {
            get
            {
                return Exists && instance.gameObject.GetInstanceID() == gameObject.GetInstanceID();
            }
        }

        #endregion


        #region Public API

        /// <summary>
        /// Determines whether the specified Singleton exists or not
        /// </summary>
        public static bool Exists
        {
            get
            {
                #if UNITY_EDITOR
                    return (instance != null);
                #else
                    return ((object) instance != null);
                #endif
            }
        }

        /// <summary>
        /// Returns the instance of this singleton.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (!Exists)
                {
                    //We could first check inside the scene for an existing instance
                    //but because how this script is constructed, asking if it `Exists` should be enough.
                    //instance = (T) FindObjectOfType(typeof(T)); 
                    
                    Debug.Log("An instance of " + typeof(T).ToString() + " is needed in the scene, but there was none found.\nOne was generated automatically for you.");

                    GameObject obj = new GameObject("Singleton_" + typeof(T));
                    instance = obj.AddComponent(typeof(T)) as T;
                }

                return instance;
            }
        }
        
        #endregion

    }

}