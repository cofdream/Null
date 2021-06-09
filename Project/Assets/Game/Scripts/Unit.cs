using DA.AssetLoad;
using DA.Core.FSM;
using Game.Skill;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class Unit : ScriptableObject
    {
        public string Name;

        public UnitAttribute UnitAttribute;

        public SKill[] SKills;

        public string PrefabPath;
        public GameObject GameObject { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }

        public ControllerHangPoint ControllerHangPoint;

        [Sirenix.OdinInspector.ShowInInspector]
        public FSM FSM;
        public AnimatorHashes AnimatorHashes;
        public AnimatorData AnimatorData;
        public MovementVariables MovementVariables;

        [Sirenix.OdinInspector.ShowInInspector]
        public Dictionary<ScriptableObject, ScriptableObject> AllDependencies;
        [Sirenix.OdinInspector.Button]
        public void GetDependencies()
        {
            AllDependencies = new Dictionary<ScriptableObject, ScriptableObject>();

            var Dependencies = AssetDatabase.GetDependencies(AssetDatabase.GetAssetPath(this));

            foreach (var dependencie in Dependencies)
            {
                ScriptableObject scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(dependencie);
                if (scriptableObject != null)
                {
                    AllDependencies.Add(scriptableObject, null);
                }
            }
        }

        public void Init()
        {
            Dictionary<ScriptableObject, ScriptableObject> temp = new Dictionary<ScriptableObject, ScriptableObject>(AllDependencies.Count);
            foreach (var item in AllDependencies)
            {
                temp.Add(item.Key, Instantiate(item.Key));
            }
            foreach (var item in temp)
            {
                AllDependencies[item.Key] = item.Value;
            }

            

            LoadGameObjec();

            for (int i = 0; i < SKills.Length; i++)
            {
                if (AllDependencies.TryGetValue(SKills[i], out ScriptableObject scriptableObject))
                {
                    SKills[i] = scriptableObject as SKill;
                    SKills[i].InstantiateDependencies(AllDependencies);
                }
                else
                {
                    Debug.LogError("------------------------");
                }
            }
            
        }

        private void LoadGameObjec()
        {
            var loader = AssetLoader.GetAssetLoader();
            var gameObject = loader.LoadAsset<GameObject>(PrefabPath);
            GameObject = GameObject.Instantiate<GameObject>(gameObject);
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
            foreach (var sKill in SKills)
            {
                sKill.UpdateSkill(Time.deltaTime);
            }
        }
    }
}