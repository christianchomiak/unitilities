using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class HelloState : FSMState 
{


	// Use this for initialization
	void Start ()
    {
	
	}
	


    protected override void DoBeforeLeaving()
    {
        Debug.Log("Leaving");
    }

    public override void DoBeforeEntering()
    {
        Debug.Log("Entering");
    }

    protected override void Act()
    {
        Debug.Log("Hello");
    }

}
