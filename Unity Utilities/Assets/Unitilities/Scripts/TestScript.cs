using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Dummy script to test various things
/// </summary>
public class TestScript : MonoBehaviour
{
    public GameObject p;

	// Use this for initialization
	void Start () 
    {
        //Pool<TestScript> p = new Pool<TestScript>(this);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //PoolManager.Instance.Spawn(p, this.transform);
            p.Spawn(this.transform);
            
        }
	}
}
