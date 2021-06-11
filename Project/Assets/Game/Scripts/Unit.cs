using DA.AssetLoad;
using Game.FSM;
using Game.Skill;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class Unit : ScriptableObjectClone
    {
        public string Name;

        public UnitAttribute UnitAttribute;

        public Skill.Skill[] Skills;

        public string PrefabPath;
        public GameObject GameObject { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }

        public ControllerHangPoint ControllerHangPoint;

        public FiniteStateMachine FSM;
        public AnimatorHashes AnimatorHashes;
        public AnimatorData AnimatorData;
        public MovementVariables MovementVariables;

        public Dictionary<int, CloneData> AllDependencies;

        public Dictionary<int, CloneData> GetDependencies()
        {
            var Dependencies = AssetDatabase.GetDependencies(AssetDatabase.GetAssetPath(this));
            List<CloneData> cloneDatas = new List<CloneData>();
            foreach (var dependencie in Dependencies)
            {
                var daSO = AssetDatabase.LoadAssetAtPath<ScriptableObjectClone>(dependencie);
                if (daSO != null)
                {
                    cloneDatas.Add(new CloneData() { Instance = daSO });
                }
            }

            AllDependencies = new Dictionary<int, CloneData>(cloneDatas.Count);
            foreach (var cloneData in cloneDatas)
            {
                AllDependencies.Add(cloneData.Instance.GetInstanceID(), cloneData);
            }

            return AllDependencies;
        }


        public void Init(Vector3 bornPosition, Quaternion rotation)
        {
            foreach (var cloneData in AllDependencies.Values)
            {
                cloneData.CloneInstance = Instantiate(cloneData.Instance);
            }

            CloneDependencies(AllDependencies);

            foreach (var skill in Skills)
            {
                skill.Init(this);
            }

            LoadGameObjec(bornPosition, rotation);
        }

        private void LoadGameObjec(Vector3 bornPosition, Quaternion rotation)
        {
            var loader = AssetLoader.GetAssetLoader();
            var gameObject = loader.LoadAsset<GameObject>(PrefabPath);
            GameObject = GameObject.Instantiate<GameObject>(gameObject, bornPosition, rotation);
            GameObject.name = Name;
            FSMManager.AddFSM(FSM);

            Rigidbody = GameObject.GetComponent<Rigidbody>();
            Animator = GameObject.GetComponent<Animator>();
            ControllerHangPoint = GameObject.GetComponent<ControllerHangPoint>();

            AnimatorHashes = new AnimatorHashes();

            AnimatorData = new AnimatorData(Animator);

            MovementVariables = new MovementVariables();

            loader.Unload(PrefabPath);
        }

        public void OnUpdate()
        {
            foreach (var sKill in Skills)
            {
                sKill.UpdateSkill(Time.deltaTime);
            }
        }

        protected override void CloneDependencies(Dictionary<int, CloneData> AllDependencies)
        {
            for (int i = 0; i < Skills.Length; i++)
            {
                Skills[i] = GetCloneInstance(AllDependencies, Skills[i]);
            }
        }
    }
}