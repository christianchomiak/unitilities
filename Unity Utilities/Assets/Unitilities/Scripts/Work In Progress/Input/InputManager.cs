using UnityEngine;
using System.Collections;


public class InputManager : Singleton<InputManager>
{

    public Vector3 previousMousePosition;


    protected override void Awake()
    {
        base.Awake();
        previousMousePosition = Vector3.zero;
    }
	
    /// <summary>
    /// Monobehaviour function that is called after all Update functions.
    /// Source: http://docs.unity3d.com/Documentation/Manual/ExecutionOrder.html
    /// </summary>
    void LateUpdate()
    {
        if (ApplicationHelper.PlatformIsDesktop)
            previousMousePosition = Input.mousePosition;
    }

    public ScreenInput TouchInput(int _buttonNumber)
    {
        int buttonNumber = 0;

        if (ApplicationHelper.PlatformIsMobile)
        {
            if (Input.touchCount > 0)
            {
                if (!_buttonNumber.Between(0, Input.touchCount - 1))
                {
                    Debug.LogWarning(Input.touchCount.ToString() + " touches registered, however the one asked for doesn't exist. First touch will be used instead.");
                    buttonNumber = 0;
                }

                return new ScreenInput(Input.GetTouch(buttonNumber));
            }
        }
        else if (ApplicationHelper.PlatformIsDesktop)
        {
            if (!_buttonNumber.Between(0, 2))
            {
                Debug.LogWarning("You're asking for mouse input (3 buttons), but you're specifying a button that doesn't exist. Left button will be used instead.");
                buttonNumber = 0;
            }

            TouchPhase tPhase = TouchPhase.Canceled;
            if (Input.GetMouseButtonDown(buttonNumber))
            {
                tPhase = TouchPhase.Began;
            }
            else if (Input.GetMouseButtonUp(buttonNumber))
            {
                tPhase = TouchPhase.Ended;
            }
            else if (Input.GetMouseButton(buttonNumber))
            {
                if (Input.mousePosition != previousMousePosition)
                {
                    tPhase = TouchPhase.Moved;
                }
                else
                {
                    tPhase = TouchPhase.Stationary;
                }
            }
            else
            {
                //tPhase = TouchPhase.Canceled;
                return null;
            }
            return new ScreenInput(Input.mousePosition, tPhase);
        }

        return null;
    }

}
