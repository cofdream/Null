using DA.AssetLoad;
using DA.Core.FSM;
using UnityEngine;

namespace Skill
{
    [CreateAssetMenu(menuName = "Units/Create Unit")]
    public class Unit : ScriptableObject
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

        public void Init()
        {
            LoadGameObjec();
        }

        private void LoadGameObjec()
        {
            GameObject = new GameObject(Name);

            var loader = AssetLoader.GetAssetLoader();
            var gameObject = loader.LoadAsset<GameObject>(PrefabPath);
            var modelGameObject = GameObject.Instantiate<GameObject>(gameObject, GameObject.transform);


            FSMManager.AddFSM(FSM);

            Rigidbody = modelGameObject.GetComponent<Rigidbody>();
            Animator = modelGameObject.GetComponent<Animator>();


            loader.Unload(PrefabPath);
        }
    }
}