using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesEnum : MonoBehaviour
{
        enum State { Enter, Waiting, Receiving, TimeExpiring, Exit };
    State state;
        void Start()
        {
        }

        State ChangeState(int stateNum)
        {
            switch (stateNum)
            {
                case 0:
                    state=State.Enter;//getOrder and set up the timer
                    break;
                case 1:
                    state = State.Waiting;//wait to be handed items
                    break;
                case 2:
                    state = State.Receiving;//check the item you were handed
                    break;
                case 3:
                    state = State.TimeExpiring;//called by an animation event
                    break;
                case 4:
                    state = State.Exit;
                    break;
                default:
                    break;
            }

        return state;
        }
    
}
