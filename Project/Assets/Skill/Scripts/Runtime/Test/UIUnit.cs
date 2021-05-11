using Skill;
using UnityEngine;
using UnityEngine.UI;

public class UIUnit : MonoBehaviour
{
    public Button Button;
    public Text Name;

    private Unit unit;

    public void Init(Unit unit)
    {
        this.unit = unit;

        Name.text = unit.Name;

        Button.onClick.RemoveAllListeners();

        Button.onClick.AddListener(SelectUnit);
    }

    private void SelectUnit()
    {
        TestUIManager.SelectUnit(unit);
    }
}
