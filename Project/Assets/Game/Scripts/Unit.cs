using DA.AssetLoad;
using DA.Core.FSM;
using Game.Skill;
using UnityEngine;

namespace Game
{
    public class Unit 
    {
        public string Name;

        public UnitAttribute UnitAttribute;

        public SKillBase[] SKills;

        public string PrefabPath;
        public GameObject GameObject { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }

        public FSM FSM;
        public AnimatorHashes AnimatorHashes = new AnimatorHashes();
        public AnimatorData AnimatorData;

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

            AnimatorData = new AnimatorData(Animator);


            loader.Unload(PrefabPath);
        }
    }
}