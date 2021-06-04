using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Game.Skill;
using DA.Core.FSM;
using DA.AssetLoad;

namespace Game.Test
{
    public class TestManager : MonoBehaviour
    {
        private List<Unit> AllUnit;

        public TestUIManager TestUIManager;

        [SerializeField] private Unit unit;

        private void Start()
        {
            AllUnit = new List<Unit>();

            // CreateHeroUnit();

            CreateHeroUnit_Skill();
        }

        private void CreateHeroUnit()
        {
            unit = new Unit()
            {
                Name = "Hero",
                UnitAttribute = new UnitAttribute(100, 100, 10, 10, 300),
                PrefabPath = "Assets/Game/Art/Prefabs/HeroModel.prefab",
                FSM = new FSM(),
            };

            unit.Init();

            string path = "Assets/Game/Art/Prefabs/ThirdPersonCamera.prefab";
            var loader = AssetLoader.GetAssetLoader();
            CameraHangPoint cameraHangPoint = loader.LoadAsset<CameraHangPoint>(path);
            cameraHangPoint = GameObject.Instantiate(cameraHangPoint);

            loader.Unload(path);

            Transform followTransform = unit.ControllerHangPoint.FollowTarget;
            cameraHangPoint.CM_VCamera.Follow = followTransform;


            State idleState = new State()
            {
                StateAction = new StateAction[]
                {
                    new InputStateAction()
                    {
                        Active = true,
                        MovementVariables = unit.MovementVariables,
                        Unit = unit,
                    },
                    new CasualStateAction_AniStateAction()
                    {
                        Active = false,
                        WaitTime = 1f,
                        DeltaTimeVariables = FSMManager.GlobalVariabeles.DeltaTimeVariables,
                        Animator = unit.Animator,
                        AnimatorHashes = unit.AnimatorHashes,
                    },
                    new RotateStateAction()
                    {
                        Active = true,
                        target = followTransform,
                    },
                },
            };


            State LocomotionState = new State()
            {
                StateAction = new StateAction[]
                {
                    new InputStateAction()
                    {
                        Active = true,
                        MovementVariables = unit.MovementVariables,
                        Unit = unit,
                    },
                    new MovementForwardStateAction()
                    {
                        Active = true,
                        MovementVariables = unit.MovementVariables,
                        Rigidbody = unit.Rigidbody,
                        Transform = unit.GameObject.transform,
                    },
                    new MovementForward_AniStateAction()
                    {
                        Active = true,
                        MovementVariables = unit.MovementVariables,
                        AnimatorHashes = unit.AnimatorHashes,
                        Transform = unit.GameObject.transform,
                        Animator = unit.Animator,
                        AnimatorData = unit.AnimatorData,
                        DeltaTimeVariables = FSMManager.GlobalVariabeles.DeltaTimeVariables,
                    },
                    new RotationBaseOnCameraOrientationStateAction()
                    {
                        Active = true,
                        Transform = unit.GameObject.transform,
                        CameraTransform = cameraHangPoint.transform,
                        MovementVariables = unit.MovementVariables,
                        DeltaVariables = FSMManager.GlobalVariabeles.DeltaTimeVariables,
                    },
                    new RotateStateAction()
                    {
                        Active = true,
                        target = followTransform,
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
                            MovementVarible = unit.MovementVariables,
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
                        MovementVariables = unit.MovementVariables,
                    },
                    TargetState = LocomotionState,
                },
            };


            State CastState = new State()
            {

            };



            unit.FSM.CurrentState = idleState;

            AllUnit.Add(unit);
        }

        private void CreateHeroUnit_Skill()
        {
            Unit unit = new Unit();
        }
    }
}