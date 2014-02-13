using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class FSMState : MonoBehaviour
{
    #region Variables

    public bool isInitialState = false;

    [SerializeField]
    List<FSMTransition> transitions;

    List<TransitionCondition> conditions;

    #endregion


    #region UnityFunctions

    protected virtual void Awake()
    {
        CheckForReflectiveTransitions();

        CheckInitialState();
    }

    // Use this for initialization
    void Start()
    {
        conditions = new List<TransitionCondition>();

        foreach (FSMTransition t in transitions)
        {
            conditions.Add(t.Condition);
        }
    }

    void FixedUpdate()
    {
        Reason();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Reason();
        Act();
    }

    #endregion


    #region AbstractFunctions

    public abstract void DoBeforeEntering();

    protected abstract void DoBeforeLeaving();

    protected abstract void Act();

    #endregion


    #region Functions

    void CheckForReflectiveTransitions()
    {
        List<FSMTransition> toDelete = new List<FSMTransition>();

        foreach (FSMTransition t in transitions)
        {
            if (t.ToState == this || t.ToState.GetType() == this.GetType())
            {
                toDelete.Add(t);
            }
        }
        if (toDelete.Count != 0)
            Debug.Log("Found " + toDelete.Count + " reflective transition" + (toDelete.Count == 1 ? "." : "s."));

        transitions.RemoveAll(x => toDelete.Contains(x));
    }

    void CheckInitialState()
    {
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
    
    #endregion
}
