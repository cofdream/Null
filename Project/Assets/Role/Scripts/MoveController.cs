using DevTool;
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
            var IdleState = new State<FiniteStateMachinePlayer>();
            var LocomotionState = new State<FiniteStateMachinePlayer>();
            var RoatationState = new State<FiniteStateMachinePlayer>();
            var InSituRotationState = new State<FiniteStateMachinePlayer>();

            fsm.MovementVariable = new MovementVariable();

            var inSituRotationCondition = new InSituRotationCondition() { TargetState = InSituRotationState };
            var movementCondition = new MovementCondition() { TargetState = LocomotionState };



            IdleState.UpdateActions = new StateAction<FiniteStateMachinePlayer>[]
            {
                new CasualStateAction(),
            };
            IdleState.ConditionActions = new Condition<FiniteStateMachinePlayer>[]
            {
                inSituRotationCondition,
                movementCondition
            };



            LocomotionState.FixUpdateActions = new StateAction<FiniteStateMachinePlayer>[]
            {
               new MovementStateAction(),
               new RotationBaseOnCameraOrientation(),
            };



            InSituRotationState.UpdateActions = new StateAction<FiniteStateMachinePlayer>[]
            {
                new InSituRotationAction(){ },
            };

            fsm.Init(IdleState);
        }


        void Update()
        {
            fsm.MovementVariable.UpdateVariable();

            fsm.deltaTime = Time.deltaTime;
            fsm.CurrentState.Update(fsm);
        }
        private void FixedUpdate()
        {
            fsm.CurrentState.FixUpdate(fsm);
        }
    }
}
