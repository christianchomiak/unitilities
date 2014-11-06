using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestScript : MonoBehaviour {

    public Color x, y;

	// Use this for initialization
	void Start ()
    {
        Color a = Color.yellow;
        Color b = Color.blue;
	}
	
	// Update is called once per frame
	void Update () 
    {
        y = x.ToHSV().ToRGB();
        Debug.Log("x: " + x.ToString());
        Debug.Log("y: " + x.ToHSV().ToString());
    }
}
