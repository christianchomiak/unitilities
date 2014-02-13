using UnityEngine;
using System.Collections;

public class ExampleExtensions : MonoBehaviour 
{
    public GameObject prefabToClone;
    public Transform parentOfNewClone;

	// Use this for initialization
	void Start () 
    {
        if (prefabToClone == null)
        {
            Debug.Log("Cannot clone null prefab");
            Destroy(gameObject);
            return;
        }

        if (parentOfNewClone == null)
        {
            Debug.Log("No parent was specified, the clone will be at the root of the scene.");
        }

	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (Input.GetKeyDown(KeyCode.Space))
        {
            prefabToClone.Clone(parentOfNewClone, "Clone of " + prefabToClone.name);
        }
	}
}
