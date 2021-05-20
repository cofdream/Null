using Skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawUnitAttribute : MonoBehaviour
{
    public Unit Unit;

    public Text helath;
    public Text maxhelath;
    public Text atk;
    public Text def;
    public Text moveSpeed;


    // Update is called once per frame
    void Update()
    {
        if (Unit != null)
        {
            helath    .text = "helath    :" + Unit.UnitAttribute.Health     .ToString();
            maxhelath .text = "maxhelath :" + Unit.UnitAttribute.MaxHealth  .ToString();
            atk       .text = "atk       :" + Unit.UnitAttribute.Atk        .ToString();
            def       .text = "def       :" + Unit.UnitAttribute.Def        .ToString();
            moveSpeed .text = "moveSpeed :" + Unit.UnitAttribute.MoveSpeed  .ToString();
        }
    }
}
