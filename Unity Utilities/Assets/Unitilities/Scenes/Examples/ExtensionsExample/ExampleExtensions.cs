using UnityEngine;
using System.Collections;
using Unitilities;

public class ExampleExtensions : MonoBehaviour 
{
    public GameObject prefabToClone;
    public Transform parentOfNewClone;
    

	// Use this for initialization
	void Start () 
    {

        Vector2 p = Vector2.one;

        Debug.Log("p: " + p.ToString());


        if (prefabToClone == null)
        {
            Debug.Log("Cannot clone null prefab");
            Destroy(gameObject);
            return;
        }

        if (parentOfNewClone == null)
        {
            Debug.Log("No customParent was specified, the clone will be at the root of the scene.");
        }

	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (Input.GetKeyDown(KeyCode.Space))
        {
            prefabToClone.Clone("Clone of " + prefabToClone.name, parentOfNewClone);
        }
	}
}
