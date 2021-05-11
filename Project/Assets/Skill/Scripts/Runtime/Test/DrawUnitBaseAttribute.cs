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
            helath    .text = "helath    :" + Unit.unitBaseAttribute.health     .GetValue().ToString();
            maxhelath .text = "maxhelath :" + Unit.unitBaseAttribute.maxHealth  .GetValue().ToString();
            atk       .text = "atk       :" + Unit.unitBaseAttribute.atk        .GetValue().ToString();
            def       .text = "def       :" + Unit.unitBaseAttribute.def        .GetValue().ToString();
            moveSpeed .text = "moveSpeed :" + Unit.unitBaseAttribute.moveSpeed  .GetValue().ToString();
        }
    }
}
