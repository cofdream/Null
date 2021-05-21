using DA.Core.FSM;
using DA.Core.FSM.Variables;
using UnityEngine;

namespace Skill
{
    public class MovementForward_Ani : StateAction
    {
        public int AniHash;
        public Animator Animator;
        public MovementVarible MovementVarible;
        public FloatVariables DeltaTimeVariables;
        public override void Execute()
        {
            Animator.SetFloat(AniHash, MovementVarible.MoveAmount, 0.2f, DeltaTimeVariables.Value);
        }
    }
}