using UnityEngine;
using System.Collections;

/// <summary>
/// Remember to call base.Awake() in each new Singleton
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    /// <summary>
    /// Returns the instance of this singleton.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    Debug.LogWarning("An instance of " + typeof(T) + " is needed in the scene, but there is none. Generated automatically.");

                    GameObject obj = new GameObject("Singleton_" + typeof(T));
                    instance = obj.AddComponent(typeof(T)) as T;
                }
            }

            return instance;
        }
    }

    public bool IsCurrentSingleton()
    {
        if (instance == null)
            return false;

        return instance.gameObject.GetInstanceID() == this.gameObject.GetInstanceID();
    }

    protected virtual void Awake()
    {
        if (instance != null && !IsCurrentSingleton())
        {
            Debug.LogWarning("Warning: More than one instance of singleton " + typeof(T) + " existing.");
            Destroy(this.gameObject);
        }
        else if (instance == null)
        {
            instance = gameObject.GetComponent<T>(); // AddComponent(typeof(T)) as T;
        }
    }

    public virtual void OnDestroy()
    {
        if (IsCurrentSingleton())
        {
            instance = null;
        }
    }

}