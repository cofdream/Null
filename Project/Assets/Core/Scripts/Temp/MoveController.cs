using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Temp2
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] FiniteStateMachinePlayer fsm;

        void Start()
        {
            var state = new State<FiniteStateMachinePlayer>();
            fsm.CurrentState = state;

            state.UpdateAction = new StateAction<FiniteStateMachinePlayer>[]
            {
               new MovementState(),
            };
        }


        void Update()
        {
            fsm.CurrentState.Update(fsm);
        }
    }
}
