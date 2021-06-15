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
        public void Init(Skill sKill)
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