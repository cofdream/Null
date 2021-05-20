using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    public class UnitObject : MonoBehaviour
    {
        public Unit Unit;

        void Update()
        {
            Unit.FSM.OnUpdate();
        }
        void FixedUpdate()
        {
            Unit.FSM.OnFixedUpdate();
        }
    }

}