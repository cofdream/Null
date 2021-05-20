using DA.AssetLoad;
using DA.Core.FSM.Variables;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using Temp2;
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
        public GameObject GameObject;

        public HeroFSM FSM;

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
            GameObject.AddComponent<UnitObject>().Unit = this;

            FSM.Init(modelGameObject);


            loader.Unload(PrefabPath);
        }
    }
}