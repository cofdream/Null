//using UnityEngine;
//using UnityEngine.Events;

//namespace RPG
//{
//    public interface State
//    {
//        void Enter();
//        void Update();
//        void Exit();
//    }

//    public class WalkState : State
//    {
//        public Animator animator;
//        public void Enter()
//        {
//            Debug.Log("Walk State.");
//            animator.CrossFade("WalkFront_Shoot_AR", 0.2f);
//        }
//        public void Update()
//        {

//        }
//        public void Exit()
//        {

//        }
//    }
//    public class RotationState : State
//    {
//        public void Enter()
//        {
//            throw new System.NotImplementedException();
//        }

//        public void Exit()
//        {
//            throw new System.NotImplementedException();
//        }

//        public void Update()
//        {
//            throw new System.NotImplementedException();
//        }
//    }

//    public class IdleState : State
//    {
//        public Animator animator;
//        public void Enter()
//        {
//            Debug.Log("Idle State.");
//            animator.CrossFade("Idle_Guard_AR", 0.2f);
//        }
//        public void Update()
//        {

//        }
//        public void Exit()
//        {

//        }
//    }
//}