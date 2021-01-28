using UnityEngine;

namespace Game
{
    public class Unit
    {
        private UnitBind unitBind;


        public void Initialize(MonoBehaviour unitBind)
        {
            this.unitBind = unitBind as UnitBind;
        }

        public void PlayMoveAnimation()
        {
            Debug.Log("PlayMoveAnimation.");
        }
    }
}