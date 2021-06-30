using DA.AssetLoad;
using Game.FSM;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class Unit
    {
        public string Name;

        public UnitAttribute UnitAttribute;

        public Magic Magic = new Magic();
    }
}