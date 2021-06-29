using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class AnimatorData
    {
        public Transform leftFoot;
        public Transform rightFoot;
        public AnimatorData(Animator animator)
        {
            leftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
            rightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot);
        }
    }
}