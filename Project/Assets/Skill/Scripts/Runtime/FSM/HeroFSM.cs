using DA.Core.FSM;
using System;
using UnityEngine;

namespace Skill
{
    [UnityEngine.CreateAssetMenu(menuName = "FSM/Create HeroFSM")]
    public class HeroFSM : FSM
    {
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }

        public void Init(GameObject gameObject)
        {
            Rigidbody = gameObject.GetComponent<Rigidbody>();
            Animator = gameObject.GetComponent<Animator>();
        }
    }
}