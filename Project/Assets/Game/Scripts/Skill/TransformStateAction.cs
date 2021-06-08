using DA.Core.FSM;
using UnityEngine;

namespace Game.Skill
{
    public class TransformStateAction : StateAction
    {
        public Unit Traget;


        public override void OnEnter()
        {
            Traget.GameObject.transform.position += Traget.GameObject.transform.forward * 3f;
        }
    }
}