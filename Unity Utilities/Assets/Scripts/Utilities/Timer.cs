using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TimerState { PAUSED, FINISHED, ONGOING };

/// <summary>
/// Basic timer to keep track of time. To be used manually, without a manager.
/// </summary>
public class Timer
{
    protected float startingTime, currentTime, targetTime;

    protected bool forward;
    protected bool isPaused;
    protected bool finished;
    protected bool looped;


    #region Construtors

    /// <summary>
    /// Creates a 1-second timer that doesn't loop and starts unpaused.
    /// </summary>
    public Timer()
        : this(1)
    {
    }

    /// <summary>
    /// Creates a k-seconds timer that doesn't loop and starts unpaused.
    /// </summary>
    /// <param name="_targetTime">Number of seconds</param>
    public Timer(float _targetTime)
        : this(0, _targetTime, false, false)
    {
    }

    /// <summary>
    /// Creates a k-seconds timer.
    /// </summary>
    /// <param name="_targetTime">Number of seconds</param>
    /// <param name="_loop">True: The timer restarts after finishing</param>
    /// <param name="_startPaused">True: The timer start paused</param>
    public Timer(float _targetTime, bool _loop, bool _startPaused)
        : this(0, _targetTime, _loop, _startPaused)
    {
    }

    /// <summary>
    /// Creates a k-seconds timer.
    /// </summary>
    /// <param name="_initialTime">Starting time in seconds</param>
    /// <param name="_targetTime">Finish time in seconds</param>
    /// <param name="_loop">True: The timer restarts after finishing</param>
    /// <param name="_startPaused">True: The timer start paused</param>
    public Timer(float _initialTime, float _targetTime, bool _loop, bool _startPaused)
    {
        currentTime = _initialTime;
        startingTime = _initialTime;
        targetTime = _targetTime;
        forward = _initialTime < targetTime;
        isPaused = _startPaused;
        finished = false;
        looped = _loop;
    }

    #endregion

    #region Properties

    public float Length
    {
        get { return Mathf.Abs(targetTime - startingTime); }
    }

    public bool IsPaused
    {
        get { return isPaused; }
    }

    public TimerState CurrentState
    {
        get
        {
            if (finished)
                return TimerState.FINISHED;

            if (isPaused)
                return TimerState.PAUSED;

            return TimerState.ONGOING;
        }
    }

    public float RemainingTime
    {
        get { return Mathf.Abs(targetTime - currentTime); }
    }

    public float CurrentTime
    {
        protected set { currentTime = value; }
        get { return currentTime; }
    }

    public bool Looped
    {
        set { looped = value; }
        get { return looped; }
    }

    public float PercentageRemaining
    {
        get { return RemainingTime / Length; }
    }

    #endregion

    #region Functions

    public bool HasFinished()
    {
        return finished;
    }

    public void Pause()
    {
        if (isPaused || finished)
            return;

        isPaused = true;
    }

    public void Resume()
    {
        if (!isPaused || finished)
            return;

        isPaused = false;
    }

    protected void Reset()
    {
        currentTime = startingTime;
    }

    public void Restart()
    {
        Reset();
        finished = false;
        isPaused = false;
    }

    public TimerState Update()
    {
        return Update(Time.deltaTime);
    }

    public TimerState Update(float seconds)
    {
        finished = false;

        if (finished)
            return TimerState.FINISHED;

        if (isPaused)
            return TimerState.PAUSED;

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
            finished = true;
            return TimerState.FINISHED;
        }

        return TimerState.ONGOING;
    }

    #endregion
}