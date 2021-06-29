using DA.AssetLoad;
using Game.FSM;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class Unit
    {
        public string Name;

        public UnitAttribute UnitAttribute;

        public Magic Magic = new Magic();

        public string PrefabPath;
        [SerializeReference] public GameObject GameObject;
        [SerializeReference] public Rigidbody Rigidbody;
        [SerializeReference] public Animator Animator;

        public ControllerHangPoint ControllerHangPoint;


        [SerializeReference] public PlayFiniteStateMachine FSM;


        public void Init(Vector3 bornPosition, Quaternion rotation)
        {
            CreatGameObject(bornPosition, rotation);
        }

        private void CreatGameObject(Vector3 bornPosition, Quaternion rotation)
        {
            var loader = AssetLoader.GetAssetLoader();
            var prefab = loader.LoadAsset<GameObject>(PrefabPath);
            loader.Unload(PrefabPath);

            GameObject = GameObject.Instantiate<GameObject>(prefab, bornPosition, rotation);
            GameObject.name = Name;


            Rigidbody = GameObject.GetComponent<Rigidbody>();
            Animator = GameObject.GetComponent<Animator>();
            ControllerHangPoint = GameObject.GetComponent<ControllerHangPoint>();
        }

        public void Update()
        {
            FSM.Update();

            Magic.Update();
        }

        public void FixedUpdate()
        {
            FSM.FixedUpdate();
        }
    }
}