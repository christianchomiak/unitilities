using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimerManager : Singleton<TimerManager>
{
    private List<ManagedTimer> timers;

    protected override void Awake()
    {
        base.Awake();

        timers = new List<ManagedTimer>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (ManagedTimer t in timers)
        {
            t.Update();
        }
    }

    public void AddTimer(ManagedTimer t)
    {
        timers.Add(t);
    }

    public void RemoveTimer(ManagedTimer t)
    {
        timers.Remove(t);
    }


}