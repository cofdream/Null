using UnityEngine;
using UnityEngine.UI;

namespace Game.Test
{
    public class UISkill : MonoBehaviour
    {
        public Button Button;
        public Text Name;
        public Text Desc;
        public Text CD;

        public Skill SKill;
        public Unit Unit;
        public int index;
        public void Init(Skill sKill, Unit unit, int index)
        {
            SKill = sKill;
            Unit = unit;
            this.index = index;

            Name.text = sKill.Name;
            Desc.text = sKill.Introduction;

            Button.onClick.RemoveAllListeners();
            Button.onClick.AddListener(CastSkill);
        }

        public void CastSkill()
        {
            Unit.Magic.Cast(index);
        }

        public void Update()
        {
            //if (SKill != null)
            //{
            //    CD.text = SKill.SkillCD.ToString();
            //}
        }
    }
}