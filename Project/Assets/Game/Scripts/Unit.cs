using DA.AssetLoad;
using DA.Core.FSM;
using Game.Skill;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class Unit
    {
        public string Name;

        public UnitAttribute UnitAttribute;

        public SKillBase[] SKills;



        public string PrefabPath;
        public GameObject GameObject { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }

        public ControllerHangPoint ControllerHangPoint;

        [Sirenix.OdinInspector.ShowInInspector]
        public FSM FSM;
        public AnimatorHashes AnimatorHashes = new AnimatorHashes();
        public AnimatorData AnimatorData;
        public MovementVariables MovementVariables;

        public void Init()
        {
            LoadGameObjec();
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

            AnimatorData = new AnimatorData(Animator);

            MovementVariables = new MovementVariables();

            loader.Unload(PrefabPath);
        }
    }


}