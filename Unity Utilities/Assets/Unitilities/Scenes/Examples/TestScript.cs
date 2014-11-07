using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TestScript : MonoBehaviour {

    Dictionary<HSVColor, int> h;

	// Use this for initialization
	void Start ()
    {
        h = new Dictionary<HSVColor, int>();

        HSVColor c = new HSVColor(Color.green);

        Debug.Log("Contains: " + c.RGB.ToString());

        //c += c; // new HSVColor(Color.yellow);

        Debug.Log("Contains: " + c.RGB.ToString());
    }
	
	// Update is called once per frame
	void Update () 
    {
    }

}
