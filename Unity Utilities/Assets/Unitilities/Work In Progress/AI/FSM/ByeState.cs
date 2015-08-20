using UnityEngine;

namespace Unitilities.AI
{

    public class ByeState : FSMState
    {

        // Use this for initialization
        void Start()
        {

        }



        protected override void DoBeforeLeaving()
        {
            Debug.Log("Leaving");
        }

        public override void DoBeforeEntering()
        {
            Debug.Log("Entering");
        }

        protected override void Act()
        {
            Debug.Log("Bye");
        }

    }

}