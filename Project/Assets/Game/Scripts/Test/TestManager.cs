using System.Collections.Generic;
using UnityEngine;
using DA.Core.FSM;
using DA.AssetLoad;

namespace Game.Test
{
    public class TestManager : MonoBehaviour
    {
        public static TestManager Instance { get; private set; }

        [SerializeField] private Unit unitHero;
        [SerializeField] private Unit unitEnemy;

        public Unit unitHeroClone;
        public CameraHangPoint CameraHangPoint;

        public List<Unit> AllUnit;

        public List<Unit> unitEnemys;
        public List<Unit> unitFriends;
        public List<Unit> unitOthers;

        public bool IsLog;
        public bool IsCreateEnemys;
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            AllUnit = new List<Unit>();

            var time = System.DateTime.Now;
            CreateHeroUnit();
            if (IsLog) Debug.Log("Create Hero time:" + (System.DateTime.Now - time).TotalSeconds);

            time = System.DateTime.Now;
            CreateFriends();
            if (IsLog) Debug.Log("Create Friends time:" + (System.DateTime.Now - time).TotalSeconds);


            time = System.DateTime.Now;
            if (IsCreateEnemys) CreateEnemys();
            if (IsLog) Debug.Log("Create Enemys time:" + (System.DateTime.Now - time).TotalSeconds);

            time = System.DateTime.Now;
            CreateOthers();
            if (IsLog) Debug.Log("Create Others time:" + (System.DateTime.Now - time).TotalSeconds);

            AllUnit.AddRange(unitFriends);
            AllUnit.AddRange(unitEnemys);
            AllUnit.AddRange(unitOthers);

            TestUIManager.Instance.ReLoadUnits(AllUnit);
        }

        private void CreateHeroUnit()
        {
            var unit = unitHeroClone = Instantiate(unitHero);
            unit.AllDependencies = unitHero.GetDependencies();

            unit.UnitAttribute = new UnitAttribute(100, 100, 10, 10, 300);
            unit.Name = "Hero";
            unit.FSM = new FSM();

            unit.Init(Vector3.zero, Quaternion.identity);

            string path = "Assets/Game/Art/Prefabs/ThirdPersonCamera.prefab";
            var loader = AssetLoader.GetAssetLoader();

            CameraHangPoint cameraHangPoint = loader.LoadAsset<CameraHangPoint>(path);
            cameraHangPoint = GameObject.Instantiate(cameraHangPoint);

            loader.Unload(path);

            Transform followTransform = unit.ControllerHangPoint.FollowTarget;
            cameraHangPoint.CM_VCamera.Follow = followTransform;

            TestUIManager.Instance.HeroCamera = cameraHangPoint.Camera;


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

            unit.FSM.CurrentState = idleState;
        }

        private void CreateEnemys()
        {
            unitEnemys = new List<Unit>();

            for (int i = 1; i <= 5; i++)
            {
                var unit = Instantiate<Unit>(unitEnemy);
                unit.AllDependencies = unitEnemy.GetDependencies();

                unit.UnitAttribute = new UnitAttribute(100, 100, 10, 10, 300);
                unit.Name = "Enemy " + i;
                unit.Init(new Vector3(Random.Range(-5, 10), 0, Random.Range(-5, 5)), Quaternion.identity);

                unitEnemys.Add(unit);
            }

        }
        private void CreateFriends()
        {
            unitFriends = new List<Unit>() { unitHeroClone };
        }
        private void CreateOthers()
        {
            unitOthers = new List<Unit>();
        }
    }
}