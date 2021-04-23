using System.Collections;
using Test;
using UnityEngine;

namespace Skill
{
    public class AddHPSkill : SKillBase
    {
        public Unit unit;
        public override void DoSomething()
        {
            unit.HP += 10;
            Debug.Log("add Hp");
        }
    }
}