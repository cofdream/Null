using System;
using UnityEngine;

namespace Game
{
    public static class AnimatorHashes
    {
        public static int MoveVertical = Animator.StringToHash("moveVertical");
        public static int Stance = Animator.StringToHash("stance");
        public static int LeftFootForward = Animator.StringToHash("leftFootForward");
        public static int Jump = Animator.StringToHash("Jump");
    }
}