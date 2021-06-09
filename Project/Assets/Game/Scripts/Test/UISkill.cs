using Game.Skill;
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

        public SKill SKill;
        public void Init(SKill sKill)
        {
            SKill = sKill;

            Name.text = sKill.Name;
            Desc.text = sKill.Introduction;

            Button.onClick.RemoveAllListeners();
            Button.onClick.AddListener(CastSkill);
        }

        public void CastSkill()
        {
            SKill.CastSkill();
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