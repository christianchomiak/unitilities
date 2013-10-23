using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TransitionCondition { NULL_ID };

[System.Serializable]
public class Transition
{
    [SerializeField]
    string name;

    [SerializeField]
    FSMState toState;

    [SerializeField]
    TransitionCondition condition;

    public Transition(FSMState _toState, TransitionCondition _condition)
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

public abstract class FSMState : MonoBehaviour
{
    public bool isInitialState = false;

    [SerializeField]
    List<Transition> transitions;

    List<TransitionCondition> conditions;

    protected virtual void Awake()
    {
        List<Transition> toDelete = new List<Transition>();

        foreach (Transition t in transitions)
        {
            if (t.ToState == this || t.ToState.GetType() == this.GetType())
            {
                toDelete.Add(t);
            }
        }
        if (toDelete.Count != 0)
            Debug.Log("Found " + toDelete.Count + " reflective transition" + (toDelete.Count == 1 ? "." : "s."));

        transitions.RemoveAll(x => toDelete.Contains(x));

        if (!isInitialState)
            this.enabled = false;
        else
        {
            if (!this.enabled)
            {
                isInitialState = false;
            }
            else
            {
                FSMState[] allFSMStates = GetComponents<FSMState>();
                for (int i = 0; i < allFSMStates.Length; i++)
                {
                    if (allFSMStates[i].enabled && allFSMStates[i] != this)
                    {
                        allFSMStates[i].isInitialState = false;
                        //allFSMStates[i].enabled = false;
                    }
                }
            }
        }
    }

	// Use this for initialization
	void Start ()
    {
        conditions = new List<TransitionCondition>();

        foreach (Transition t in transitions)
        {
            conditions.Add(t.Condition);
        }
	}

    void FixedUpdate()
    {
        Reason();
    }

	// Update is called once per frame
	protected virtual void Update ()
    {
        //Reason();
        Act();
	}

    public abstract void DoBeforeEntering();

    protected abstract void DoBeforeLeaving();

    void ExecuteTransition(FSMState toState)
    {
        DoBeforeLeaving();

        toState.enabled = true;

        toState.DoBeforeEntering();

        this.enabled = false;
    }



    protected virtual void Reason()
    {
        if (conditions != null)
        {
            foreach (TransitionCondition tc in conditions)
            {
                switch (tc)
                {
                    case TransitionCondition.NULL_ID:
                        break;
                    default:
                        break;
                }
            }
        }
        //Debug.Log("REASONING");
    }

    protected abstract void Act();

}
