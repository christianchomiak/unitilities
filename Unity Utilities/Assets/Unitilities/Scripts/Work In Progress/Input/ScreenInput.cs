using UnityEngine;
using System.Collections;

/// <summary>
/// Created with the purpose of merge mouse input and device touch input 
/// on a single input type
/// </summary>
public class ScreenInput
{

    /// <summary>
    /// Screen Position
    /// </summary>
    protected Vector2 position;

    /// <summary>
    /// Touch phase
    /// </summary>
    protected TouchPhase phase;

    /// <summary>
    /// Finger id. It will be -1 on mouse input
    /// </summary>
    protected int fingerId;

    public int FingerID
    {
        get { return fingerId; }
    }

    public Vector2 Position
    {
        get { return position; }
    }

    public TouchPhase Phase
    {
        get { return phase; }
    }

    public ScreenInput(Vector2 pos, TouchPhase inputPhase)
    {
        Init(pos, inputPhase, -1);
    }

    public ScreenInput(Vector2 pos, TouchPhase inputPhase, int fId)
    {
        Init(pos, inputPhase, fId);
    }

    public ScreenInput(Touch touch)
    {
        Init(touch.position, touch.phase, touch.fingerId);
    }

    private void Init(Vector2 pos, TouchPhase inputPhase, int fId)
    {
        this.position = pos;
        this.phase = inputPhase;
        this.fingerId = fId;
    }

    public override string ToString()
    {
        return ("Input in id \"" + fingerId + "\". Position: " + position.ToString() + ". Phase: " +  phase.ToString());
    }
}
