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

            var inSituRotationCondition = new InSituRotationCondition() { TargetState = InSituRotationState, movementVariable = movementVariable };
            var movementCondition = new MovementCondition() { TargetState = LocomotionState, movementVariable = movementVariable };

            IdleState.UpdateActions = new StateAction<FiniteStateMachinePlayer>[]
            {
                new CasualStateAction() { variable = casualVariable},
            };

            IdleState.ConditionActions = new Condition<FiniteStateMachinePlayer>[]
            {
                inSituRotationCondition,
                movementCondition
            };




            LocomotionState.FixUpdateActions = new StateAction<FiniteStateMachinePlayer>[]
            {
               new MovementStateAction(){ variable = movementVariable },
               //new RotationBaseOnCameraOrientation(){ variable = movementVariable },
            };



            InSituRotationState.UpdateActions = new StateAction<FiniteStateMachinePlayer>[]
            {
                new InSituRotationAction(){ },
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
