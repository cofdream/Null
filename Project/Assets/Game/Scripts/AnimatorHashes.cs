using System;
using UnityEngine;

namespace Game
{
    public static class AnimatorHashes
    {
        public static int MoveVerticalParameter = Animator.StringToHash("moveVertical");
        public static int StanceParameter = Animator.StringToHash("stance");
        public static int LeftFootForwardParameter = Animator.StringToHash("leftFootForward");
        public static int stop = Animator.StringToHash("stop");

        public static int JumpIdleState = Animator.StringToHash("JumpIdle");
        public static int JumpRunState = Animator.StringToHash("JumpRun");
        public static int QuickTurnLeftState = Animator.StringToHash("QuickTurnLeft");
    }
}