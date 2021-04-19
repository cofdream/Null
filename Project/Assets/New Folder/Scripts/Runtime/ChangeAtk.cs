using UnityEngine;

namespace NullNamespace
{
    public class ChangeAtk : MonoBehaviour
    {
        public int Value;
        public bool Add;
        private void Update()
        {
            if (Add == false)
            {
                return;
            }
            var unit = gameObject.GetComponent<Unit>();
            if (unit != null)
            {
                Buff_Timer buff = gameObject.AddComponent<Buff_Timer>();

                buff.targetUnit = unit;
                if (Value > 0)
                {
                    buff.buffName_temp = "Add Atk " + Value;
                }
                else
                {
                    buff.buffName_temp = "Reduce Atk " + Value;
                }


                buff.Update = () =>
                {
                    int value = Value;
                    unit.AddAtK(value);
                };
            }

            Destroy(this);
        }
    }
}