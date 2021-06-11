using Game.FSM;
using Game.Variable;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class RotateStateAction : StateAction
    {
        public GameobjectVariable TargetGOVariable;
        private Transform target;
        private float x;
        private float y;


        public override void OnEnter()
        {
            var euler = target.localEulerAngles;
            x = euler.y;
            y = -euler.x;

            target = TargetGOVariable.Value.transform;
        }

        public override void OnUpdate()
        {
            // 左右旋转有些问题，暂时关闭
            //x += Input.GetAxis("Mouse X");
            //y += Input.GetAxis("Mouse Y");

            //x = Mathf.Clamp(x, -35f, 35f);
            //y = Mathf.Clamp(y, -35f, 35f);

            //target.localRotation = Quaternion.Euler(-y, x, 0);
        }

        protected override void CloneDependencies(Dictionary<int, CloneData> allDependencies)
        {
            TargetGOVariable = GetCloneInstance(allDependencies, TargetGOVariable);
        }
    }
}