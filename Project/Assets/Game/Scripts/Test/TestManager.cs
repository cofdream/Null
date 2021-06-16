using System.Collections.Generic;
using UnityEngine;
using Game.FSM;
using DA.AssetLoad;
using Game.Variable;

namespace Game.Test
{
    public class TestManager : MonoBehaviour
    {
        public static TestManager Instance { get; private set; }

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

        private void Update()
        {
            foreach (var item in AllUnit)
            {
                item.Update();
            }
        }
        private void FixedUpdate()
        {
            foreach (var item in AllUnit)
            {
                item.FixedUpdate();
            }
        }

        private void CreateHeroUnit()
        {
            Unit unit = new Unit();

            unit.Name = "Hero";
            unit.PrefabPath = "Assets/Game/Art/Prefabs/HeroModel.prefab";

            unit.UnitAttribute = new UnitAttribute(100, 100, 10, 10, 8);

            unit.FSM = new FiniteStateMachine();

            //Skill
            {
                Skill AtkRandomTarget = new Skill();

                AplyDamageSkillAction aplyDamageSkillAction = new AplyDamageSkillAction();
                ClculateDamageSkillAction clculateDamageSkillAction = new ClculateDamageSkillAction();
                GetRandomTargetSkillAction getRandomTargetSkillAction = new GetRandomTargetSkillAction();

                UnitVariable executor = new UnitVariable();
                UnitVariable target = new UnitVariable();
                IntVariable intVariable = new IntVariable();

                aplyDamageSkillAction.Executor = executor;
                aplyDamageSkillAction.Target = target;
                aplyDamageSkillAction.DamageVariable = intVariable;

                clculateDamageSkillAction.Executor = executor;
                clculateDamageSkillAction.Target = target;
                clculateDamageSkillAction.DamageVariable = intVariable;
                clculateDamageSkillAction.BaseDamage = 10;

                getRandomTargetSkillAction.Executor = executor;
                getRandomTargetSkillAction.Target = target;

                AtkRandomTarget.SkillActions = new SkillAction[] { getRandomTargetSkillAction, clculateDamageSkillAction, aplyDamageSkillAction };

                executor.Value = unit;

                //Skill fly = new Skill();


                unit.Magic.Skills = new Skill[] { AtkRandomTarget };
            }

            unit.Init(Vector3.zero, Quaternion.identity);


            unitHeroClone = unit;


            string path = "Assets/Game/Art/Prefabs/ThirdPersonCamera.prefab";
            var loader = AssetLoader.GetAssetLoader();
            CameraHangPoint cameraHangPoint = loader.LoadAsset<CameraHangPoint>(path);
            loader.Unload(path);

            cameraHangPoint = GameObject.Instantiate(cameraHangPoint);
            Transform followTransform = unit.ControllerHangPoint.FollowTarget;
            cameraHangPoint.CM_VCamera.Follow = followTransform;

            TestUIManager.Instance.HeroCamera = cameraHangPoint.Camera;


            State idleState = new State();
            State LocomotionState = new State();

            var InputStateAction = new InputStateAction();
            var RotateStateAction = new RotateStateAction();
            var MovementForwardStateAction = new MovementForwardStateAction();
            var MovementForward_AniStateAction = new MovementForward_AniStateAction();
            var RotationBaseOnCameraOrientationStateAction = new RotationBaseOnCameraOrientationStateAction();

            var ToLocamotionState = new Transition();
            var MoveCondition = new MoveCondition();

            var ToIdleState = new Transition();
            var StopCondition = new IdleCondition();

            var MovementVariables = new MovementVariables();

            unit.FSM.AllStates = new State[] { idleState, LocomotionState };
            unit.FSM.CurrentState = idleState;

            idleState.StateAction = new StateAction[] { InputStateAction, RotateStateAction };
            idleState.Transitions = new Transition[] { ToLocamotionState };

            LocomotionState.StateAction = new StateAction[] { InputStateAction, RotateStateAction, MovementForwardStateAction, RotationBaseOnCameraOrientationStateAction, MovementForward_AniStateAction };
            LocomotionState.Transitions = new Transition[] { ToIdleState };

            InputStateAction.MovementVariables = MovementVariables;
            InputStateAction.UnitVariable = new UnitVariable() { Value = unit };

            RotateStateAction.target = unit.GameObject.transform;

            MovementForwardStateAction.MovementVariables = MovementVariables;
            MovementForwardStateAction.transform = unit.GameObject.transform;
            MovementForwardStateAction.rigidbody = unit.Rigidbody;

            MovementForward_AniStateAction.Animator = unit.Animator;
            MovementForward_AniStateAction.MovementVariables = MovementVariables;
            MovementForward_AniStateAction.Transform = unit.GameObject.transform;

            RotationBaseOnCameraOrientationStateAction.Transform = unit.GameObject.transform;
            RotationBaseOnCameraOrientationStateAction.MovementVariables = MovementVariables;
            RotationBaseOnCameraOrientationStateAction.CameraTransform = cameraHangPoint.Camera.transform;

            MoveCondition.MovementVariable = MovementVariables;
            StopCondition.MovementVarible = MovementVariables;

            ToLocamotionState.Condition = MoveCondition;
            ToLocamotionState.TargetState = LocomotionState;

            ToIdleState.Condition = StopCondition;
            ToIdleState.TargetState = idleState;
        }

        private void CreateEnemys()
        {
            unitEnemys = new List<Unit>();

            for (int i = 0; i < 1; i++)
            {
                Unit unit = new Unit();

                unitEnemys.Add(unit);

                unit.Name = "Enemy";
                unit.PrefabPath = "Assets/Game/Art/Prefabs/HeroModel.prefab";

                unit.UnitAttribute = new UnitAttribute(100, 100, 10, 10, 8);

                unit.Magic.Skills = new Skill[] { };

                unit.FSM = new FiniteStateMachine();

                unit.Init(new Vector3(1, 0, 1), Quaternion.identity);


                unitHeroClone = unit;


                #region MyRegion
                //State idleState = new State();
                //State LocomotionState = new State();

                ////var InputStateAction = new InputStateAction();
                //var RotateStateAction = new RotateStateAction();
                //var MovementForwardStateAction = new MovementForwardStateAction();
                //var MovementForward_AniStateAction = new MovementForward_AniStateAction();

                //var ToLocamotionState = new Transition();
                //var MoveCondition = new MoveCondition();

                //var ToIdleState = new Transition();
                //var StopCondition = new IdleCondition();

                //var MovementVariables = new MovementVariables();

                //unit.FSM.AllStates = new State[] { idleState, LocomotionState };
                //unit.FSM.CurrentState = idleState;

                //idleState.StateAction = new StateAction[] {  RotateStateAction };
                //idleState.Transitions = new Transition[] { ToLocamotionState };

                //LocomotionState.StateAction = new StateAction[] {  RotateStateAction, MovementForwardStateAction, MovementForward_AniStateAction };
                //LocomotionState.Transitions = new Transition[] { ToIdleState };


                //RotateStateAction.target = unit.GameObject.transform;

                //MovementForwardStateAction.MovementVariables = MovementVariables;
                //MovementForwardStateAction.transform = unit.GameObject.transform;
                //MovementForwardStateAction.rigidbody = unit.Rigidbody;

                //MovementForward_AniStateAction.Animator = unit.Animator;
                //MovementForward_AniStateAction.MovementVariables = MovementVariables;
                //MovementForward_AniStateAction.Transform = unit.GameObject.transform;

                //MoveCondition.MovementVariable = MovementVariables;
                //StopCondition.MovementVarible = MovementVariables;

                //ToLocamotionState.Condition = MoveCondition;
                //ToLocamotionState.TargetState = LocomotionState;

                //ToIdleState.Condition = StopCondition;
                //ToIdleState.TargetState = idleState; 
                #endregion
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