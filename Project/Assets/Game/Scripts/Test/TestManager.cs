using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Game.Skill;
using DA.Core.FSM;

namespace Game.Test
{
    public class TestManager : MonoBehaviour
    {
        private List<Unit> AllUnit;

        public TestUIManager TestUIManager;

        private void Start()
        {
            AllUnit = new List<Unit>();

            CreateHeroUnit();
        }

        private void CreateHeroUnit()
        {
            MovementVarible movementVarible = new MovementVarible();

            Unit unit = new Unit()
            {
                Name = "Hero",
                UnitAttribute = new UnitAttribute(100, 100, 10, 10, 300),
                PrefabPath = "Game/Art/Prefabs/HeroModel.prefab",
                FSM = new FSM(),
            };

            unit.Init();

            State idleState = new State()
            {
                StateAction = new StateAction[]
                {
                    new InputStateAction()
                    {
                        Active = true,
                        MovementVarible = movementVarible,
                        Unit = unit,
                    },
                    //new CasualStateAction_Ani()
                    //{
                    //    Active = true,
                    //    WaitTime = 1f,
                    //    DeltaTimeVariables = FSMManager.GlobalVariabeles.DeltaTimeVariables,
                    //    Animator = unit.Animator,
                    //    AnimatorHashes = unit.AnimatorHashes,
                    //},
                },
            };

            State LocomotionState = new State()
            {
                StateAction = new StateAction[]
                {
                    new InputStateAction()
                    {
                        Active = true,
                        MovementVarible = movementVarible,
                        Unit = unit,
                    },
                    new MovementForwardStateAction()
                    {
                        Active = true,
                        MovementVarible = movementVarible,
                        Rigidbody = unit.Rigidbody,
                        Transform = unit.GameObject.transform,
                    },
                    new MovementForward_Ani()
                    {
                        Active = true,
                        MovementVarible = movementVarible,
                        AnimatorHashes = unit.AnimatorHashes,
                        Transform = unit.GameObject.transform,
                        Animator = unit.Animator,
                        AnimatorData = unit.AnimatorData,
                        DeltaTimeVariables = FSMManager.GlobalVariabeles.DeltaTimeVariables,
                    },
                },
                Transitions = new Transition[]
                {
                    new Transition()
                    {
                        Active = true,
                        id = 0,
                        Condition = new IdleCondition()
                        {
                            Description = "To idle state",
                            MovementVarible = movementVarible,
                        },
                        TargetState = idleState,
                    },
                },
            };

            idleState.Transitions = new Transition[]
            {
                new Transition()
                {
                    Active = true,
                    id = 0,
                    Condition = new MoveCondition()
                    {
                        Description = "To locomotion state",
                        MovementVarible = movementVarible,
                    },
                    TargetState = LocomotionState,
                },
            };



            unit.FSM.CurrentState = idleState;

            AllUnit.Add(unit);
        }
    }
}