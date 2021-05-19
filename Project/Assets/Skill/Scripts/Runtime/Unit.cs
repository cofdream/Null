using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    //[CreateAssetMenu(menuName = "Temp/Unit")]
    [System.Serializable]
    public class Unit /*: ScriptableObject*/
    {
        public UnitAttribute unitAttribute;

        public SKillBase[] sKills;

        public string Name;
    }
}