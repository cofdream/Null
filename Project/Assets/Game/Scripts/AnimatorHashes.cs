using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class AnimatorHashes
    {
        public readonly int MoveVertical = Animator.StringToHash("moveVertical");
        public readonly int Stance = Animator.StringToHash("stance");
        public readonly int LeftFootForward = Animator.StringToHash("leftFootForward");
    }
}