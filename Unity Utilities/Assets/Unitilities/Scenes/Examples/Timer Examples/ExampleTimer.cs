using UnityEngine;
using System.Collections;

public class ExampleTimer : MonoBehaviour 
{
    public float targetTime = 1f;
    public bool repeatTimer = false;
    public bool startsPaused = false;

    Timer timer;

	// Use this for initialization
	void Start () 
    {
        timer = new Timer(targetTime, repeatTimer, startsPaused);
	}
	
	// Update is called once per frame
	void Update () 
    {
        TimerState ts = timer.Update();

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (timer.IsPaused)
                timer.Resume();
            else
                timer.Pause();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (timer.HasEnded)
                timer.Restart();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (timer.CurrentState == TimerState.ONGOING)
                timer.Reset();
        }

        if (ts == TimerState.PAUSED)
        {
            Debug.Log("Timer PAUSED. Press P to resume.");
        }
        else if (ts == TimerState.ONGOING)
        {
            Debug.Log("Timer is RUNNING. Current time: " + timer.CurrentTime + ". Remains: " + timer.RemainingTime + ". Remains (percentage): " + (timer.PercentageRemaining * 100) + "%");
        }
        else if (ts == TimerState.FINISHED)
        {
            Debug.Log("Timer FINISHED. Press S to restart.");
        }
	}
}
