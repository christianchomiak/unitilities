/// <summary>
/// Timer v1.0.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Basic timer to keep track of time. To be used and consulted manually, without a manager.
/// </summary>

using UnityEngine;

namespace Unitilities.Timers
{

    public enum TimerState { PAUSED, FINISHED, ONGOING };

    /// <summary>
    /// Basic timer to keep track of time. To be used manually, without a manager.
    /// </summary>
    public class Timer
    {
        protected float startingTime, currentTime, targetTime;

        protected bool startsPaused;

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
        /// Creates a k-seconds timer that doesn't loop
        /// </summary>
        /// <param name="_targetTime">Number of seconds</param>
        /// <param name="_startPaused">True: The timer start paused</param>
        public Timer(float _targetTime, bool _startPaused)
            : this(0, _targetTime, false, _startPaused)
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
            _initialTime = Mathf.Abs(_initialTime);
            _targetTime = Mathf.Abs(_targetTime);

            if (_targetTime == 0f)
            {
                Debug.LogError("Timer cannot have a target time of 0. A default of 1 will be used.");
                _targetTime = 1f;
            }

            currentTime = _initialTime;
            startingTime = _initialTime;
            targetTime = _targetTime;
            forward = _initialTime < targetTime;

            startsPaused = _startPaused;
            isPaused = startsPaused;

            finished = false;
            looped = _loop;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The length (in seconds) of the Timer
        /// </summary>
        public float Length
        {
            get { return Mathf.Abs(targetTime - startingTime); }
        }

        /// <summary>
        /// Is the Timer paused?
        /// </summary>
        public bool IsPaused
        {
            get { return isPaused; }
        }

        /// <summary>
        /// Has the Timer ended?
        /// </summary>
        public bool HasEnded
        {
            get { return finished; }
        }

        /// <summary>
        /// Current state of the Timer: Finished, Paused or Ongoing
        /// </summary>
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

        /// <summary>
        /// Number of seconds until the timer ends
        /// </summary>
        public float RemainingTime
        {
            get { return Mathf.Abs(targetTime - currentTime); }
        }

        /// <summary>
        /// The current value of the timer
        /// </summary>
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

        /// <summary>
        /// Completion percentage of the timer. 100% == 1f
        /// </summary>
        public float PercentageRemaining
        {
            get { return RemainingTime / Length; }
        }

        #endregion

        #region Functions

        /*public bool HasFinished()
    {
        return finished;
    }*/

        /// <summary>
        /// Pauses an ongoing timer.
        /// </summary>
        public void Pause()
        {
            if (isPaused || finished)
                return;

            isPaused = true;
        }

        /// <summary>
        /// Unpauses a paused the timer
        /// </summary>
        public void Resume()
        {
            if (!isPaused || finished)
                return;

            isPaused = false;
        }

        /// <summary>
        /// Starts over a running timer (paused if created that way)
        /// </summary>
        public void Reset()
        {
            currentTime = startingTime;
            isPaused = startsPaused; //isPaused = false;
        }

        /// <summary>
        /// Starts over a finished timer
        /// </summary>
        public void Restart()
        {
            Reset();
            finished = false;
        }

        /// <summary>
        /// Updates the Timer with the current delta of time
        /// </summary>
        /// <returns>The state of the Timer after the update</returns>
        public TimerState Update()
        {
            return Update(Time.deltaTime);
        }

        /// <summary>
        /// Updates the Timer with a custom number of seconds
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns>The state of the Timer after the update</returns>
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
                if (seconds < 0f)
                {
                    Debug.LogError("Timer is set to move forward but the number of seconds is negative. Timer will not be updated.");
                    return TimerState.ONGOING;
                }

                currentTime += seconds;
                timeReached = currentTime >= targetTime;
            }
            else
            {
                if (seconds > 0f)
                {
                    Debug.LogError("Timer is set to move backwards but the number of seconds is positive. Timer will not be updated.");
                    return TimerState.ONGOING;
                }

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

}