using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Temp2
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] FiniteStateMachinePlayer fsm;

        [SerializeField] MovementVariable movementVariable;
        void Start()
        {
            var IdleState = new State<FiniteStateMachinePlayer>();
            var LocomotionState = new State<FiniteStateMachinePlayer>();
            var RoatationState = new State<FiniteStateMachinePlayer>();
            var InSituRotationState = new State<FiniteStateMachinePlayer>();

            var casualVariable = new CasualVariable();
            movementVariable = new MovementVariable();


            IdleState.UpdateActions = new StateAction<FiniteStateMachinePlayer>[]
            {
                new Casual() { variable = casualVariable},
            };

            IdleState.ConditionActions = new Condition<FiniteStateMachinePlayer>[]
            {
                new InSituRotationCondition(){ TargetState = LocomotionState, movementVariable = movementVariable },
                new MovementCondition(){ TargetState = LocomotionState, movementVariable = movementVariable },
            };


            LocomotionState.FixUpdateActions = new StateAction<FiniteStateMachinePlayer>[]
            {
               new MovementStateAction(),
               new RotationBaseOnCameraOrientation(),
            };


            fsm.CurrentState = IdleState;
        }


        void Update()
        {
            movementVariable.UpdateVariable();

            fsm.deltaTime = Time.deltaTime;
            fsm.CurrentState.Update(fsm);
        }
        private void FixedUpdate()
        {
            fsm.CurrentState.FixUpdate(fsm);
        }
    }
}
