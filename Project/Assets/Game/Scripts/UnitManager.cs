using DA.Singleton;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class UnitManager
    {
        private static UnitManager instance;
        public static UnitManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new UnitManager();
                return instance;
            }
        }
        private UnitManager() { }


        private List<Unit> allUnits;

        public void CreareUnit(List<Unit> allUnits)
        {
        }
    }
}