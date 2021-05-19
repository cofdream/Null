using Skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawUnitBaseAttribute : MonoBehaviour
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
            helath    .text = "helath    :" + Unit.unitBaseAttribute.Health     .ToString();
            maxhelath .text = "maxhelath :" + Unit.unitBaseAttribute.MaxHealth  .ToString();
            atk       .text = "atk       :" + Unit.unitBaseAttribute.Atk        .ToString();
            def       .text = "def       :" + Unit.unitBaseAttribute.Def        .ToString();
            moveSpeed .text = "moveSpeed :" + Unit.unitBaseAttribute.MoveSpeed  .ToString();
        }
    }
}
