using UnityEngine;

public enum TransitionCondition { NULL_ID };

[System.Serializable]
public class FSMTransition
{
    [SerializeField]
    string name;

    [SerializeField]
    FSMState toState;

    [SerializeField]
    TransitionCondition condition;

    public FSMTransition(FSMState _toState, TransitionCondition _condition)
    {
        toState = _toState;
        condition = _condition;
    }

    public FSMState ToState
    {
        get { return toState; }
    }

    public TransitionCondition Condition
    {
        get { return condition; }
    }

}
