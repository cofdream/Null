using Game.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        public Animator Animator;
        public Rigidbody Rigidbody;
        public ControllerHangPoint ControllerHangPoint;

        public PlayFiniteStateMachine PlayFiniteStateMachine;

        public bool IsJump;
        public Movement Movement;
        public Transform ModeTransform;

        public void Start()
        {
            PlayFiniteStateMachine.CurrentState = PlayFiniteStateMachine.AllStates[0];
            PlayFiniteStateMachine.Start(this);
        }

        public void Update()
        {
            PlayFiniteStateMachine.Update(this);
        }
        public void FixedUpdate()
        {
            PlayFiniteStateMachine.FixedUpdate(this);
        }
    }
}