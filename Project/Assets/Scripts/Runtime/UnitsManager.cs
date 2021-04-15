using System.Collections.Generic;
using UnityEngine;

namespace NullNamespace
{
    public class UnitsManager : MonoBehaviour
    {
        public static UnitsManager Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
        }



        public List<Unit> units = new List<Unit>();

        public void Start()
        {
            for (int i = 0; i < 3; i++)
            {
                var unit = GameObject.CreatePrimitive(PrimitiveType.Capsule).gameObject.AddComponent<Unit>();
                unit.transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));

                unit.Initialize();
                units.Add(unit);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                units[0].castSpell.CastSpell(units[0]);
            }
        }

        public Unit GetRandomUnit(Unit filter)
        {
            int index = Random.Range(0, units.Count);
            var unit = units[index];
            if (unit == filter)
            {
                if (index == 0)
                {
                    index = 1;
                }
                else if (index == units.Count - 1)
                {
                    index = units.Count - 1;
                }
                else
                {
                    index++;
                }
                return units[index];
            }
            else
                return unit;
        }
    }
}