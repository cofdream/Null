using Skill;
using UnityEngine;
using UnityEngine.UI;

public class UISkill : MonoBehaviour
{
    public Button Button;
    public Text Name;
    public Text Desc;
    public Text CD;

    private SkillTimer skillTimer;
    public void Init(SKillBase sKill)
    {
        skillTimer = sKill as SkillTimer;

        Name.text = sKill.Name;
        Desc.text = sKill.Introduction;

        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(CastSkill);
    }

    public void CastSkill()
    {
        bool iscd = skillTimer.Cast(TestUIManager.selectUnit);
    }

    public void Update()
    {
        if (skillTimer != null)
        {
            CD.text = skillTimer.SkillCD.ToString();
        }
    }
}
