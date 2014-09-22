using UnityEngine;
using System.Collections;

public class ManagedTimerExample : MonoBehaviour 
{
    ManagedTimer mt;

	// Use this for initialization
	void Start () 
    {
        mt = new ManagedTimer(0.3f, true, false);
        mt.AddOnFinishListener(DebugHola);
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    public void DebugHola(ManagedTimer _mt)
    {
        Debug.Log("Hello");
    }

}
