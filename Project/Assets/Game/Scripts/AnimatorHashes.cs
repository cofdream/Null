using System;
using UnityEngine;

namespace Game
{
    public static class AnimatorHashes
    {
        public static int MoveVerticalParameter = Animator.StringToHash("moveVertical");
        public static int StanceParameter = Animator.StringToHash("stance");
        public static int LeftFootForwardParameter = Animator.StringToHash("leftFootForward");
        public static int IsRun2Parameter = Animator.StringToHash("isRun2");
        public static int IsMove = Animator.StringToHash("isMove");
        public static int QuickTurnLeft = Animator.StringToHash("quickTurnLeft");

        public static int JumpIdleState = Animator.StringToHash("JumpIdle");
        public static int JumpRunState = Animator.StringToHash("JumpRun");
        public static int QuickTurnLeftState = Animator.StringToHash("QuickTurnLeft");
        public static int LocomotionState = Animator.StringToHash("Locomotion");
    }
}