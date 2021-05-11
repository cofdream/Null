using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    //[CreateAssetMenu(menuName = "Temp/Unit")]
    public class Unit /*: ScriptableObject*/
    {
        public UnitBaseAttribute unitBaseAttribute;

        public SKillBase[] sKills;

        public string Name;
    }
}