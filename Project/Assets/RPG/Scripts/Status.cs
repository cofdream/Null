using UnityEngine;
using UnityEngine.Events;

namespace RPG
{
    public interface State
    {
        void Enter();
        void Update();
        void Exit();
    }

    public class WalkState : State
    {
        public Animator animator;
        public void Enter()
        {
            Debug.Log("Walk State.");
            animator.SetInteger("Move", 1);
        }
        public void Update()
        {

        }
        public void Exit()
        {

        }
    }

    public class IdleState : State
    {
        public Animator animator;
        public void Enter()
        {
            Debug.Log("Idle State.");
            animator.SetInteger("Move", 0);
        }
        public void Update()
        {

        }
        public void Exit()
        {

        }
    }
}