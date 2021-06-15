using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class AnimatorHashes
    {
        public int Vertical = Animator.StringToHash("vertical");
        public int Stance = Animator.StringToHash("stance");
        public int LeftFootForward = Animator.StringToHash("leftFootForward");
    }
}