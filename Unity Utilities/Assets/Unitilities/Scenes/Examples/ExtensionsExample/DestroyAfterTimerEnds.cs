using UnityEngine;
using System.Collections;

public class DestroyAfterTimerEnds : MonoBehaviour 
{
    Timer autodestructTimer;

    public float targetTime = 1f;

	// Use this for initialization
	void Start () 
    {
        autodestructTimer = new Timer(targetTime);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (autodestructTimer.Update() == TimerState.FINISHED)
            Destroy(gameObject);
	}
}
