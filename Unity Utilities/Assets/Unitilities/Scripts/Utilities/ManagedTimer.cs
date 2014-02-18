using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Timer to keep track of time. It uses a manager to update itself and trigger its listeners
/// </summary>
public class ManagedTimer : Timer
{
    /// <summary>
    /// Delegate so other classes can set matching functions as listeners.
    /// Parameter 'timer' will be a reference to the timer that executed the trigger.
    /// </summary>
    public delegate void TimerEventListener(ManagedTimer timer);

    protected int numberOfTimesTriggered;

    protected List<TimerEventListener> onFinishListeners;
    protected List<TimerEventListener> onPauseListeners;
    protected List<TimerEventListener> onResumeListeners;


    #region Construtors/Destructors

    public ManagedTimer()
        : this(1)
    {
    }

    public ManagedTimer(float _targetTime)
        : this(0, _targetTime, false, false)
    {
    }

    public ManagedTimer(float _targetTime, bool _loop, bool _startPaused)
        : this(0, _targetTime, _loop, _startPaused)
    {
    }

    public ManagedTimer(float _initialTime, float _targetTime, bool _loop, bool _startPaused)
    {
        numberOfTimesTriggered = 0;

        onFinishListeners = new List<TimerEventListener>();
        onPauseListeners = new List<TimerEventListener>();
        onResumeListeners = new List<TimerEventListener>();

        currentTime = _initialTime;
        startingTime = _initialTime;
        targetTime = _targetTime;
        forward = _initialTime < targetTime;
        isPaused = _startPaused;
        finished = false;
        looped = _loop;

        TimerManager.Instance.AddTimer(this);
    }

    public void Destroy()
    {
        TimerManager.Instance.RemoveTimer(this);
    }

    #endregion

    #region Listeners

    protected void CallListeners(List<TimerEventListener> listeners)
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i](this);
        }
    }

    public virtual void RemoveOnFinishListener(TimerEventListener listener)
    {
        this.onFinishListeners.Remove(listener);
    }

    public virtual void RemoveOnPauseListener(TimerEventListener listener)
    {
        this.onPauseListeners.Remove(listener);
    }

    public virtual void RemoveOnResumeListener(TimerEventListener listener)
    {
        this.onResumeListeners.Remove(listener);
    }


    public virtual void AddOnFinishListener(TimerEventListener listener)
    {
        if (!this.onFinishListeners.Contains(listener))
            this.onFinishListeners.Add(listener);
    }

    public virtual void AddOnPauseListener(TimerEventListener listener)
    {
        if (!this.onPauseListeners.Contains(listener))
            this.onPauseListeners.Add(listener);
    }

    public virtual void AddOnResumeListener(TimerEventListener listener)
    {
        if (!this.onResumeListeners.Contains(listener))
            this.onResumeListeners.Add(listener);
    }

    #endregion

    #region Properties

    public int TriggeredTimes
    {
        get
        {
            return numberOfTimesTriggered;
        }
    }

    #endregion

    #region Functions


    protected void Trigger()
    {
        numberOfTimesTriggered++;

        CallListeners(this.onFinishListeners);
    }


    /// <summary>
    /// Updates the Timer with the current delta of time
    /// </summary>
    /// <returns>The state of the Timer after the update</returns>
    public void Update()
    {
        Update(Time.deltaTime);
    } 

    /// <summary>
    /// Updates the Timer with a custom amount of seconds
    /// </summary>
    /// <returns>The state of the Timer after the update</returns>
    public void Update(float seconds)
    {
        if (finished)
            return;

        if (isPaused)
            return;

        bool timeReached = false;

        if (forward)
        {
            currentTime += seconds;
            timeReached = currentTime >= targetTime;
        }
        else
        {
            currentTime -= seconds;
            timeReached = currentTime <= targetTime;
        }

        if (timeReached)
        {
            Trigger();

            if (looped)
            {
                float delta = Mathf.Abs(currentTime - targetTime);
                Reset();
                Update(delta);
            }
            else
            {
                currentTime = targetTime;
                finished = true;
            }
        }
    }

    #endregion
}