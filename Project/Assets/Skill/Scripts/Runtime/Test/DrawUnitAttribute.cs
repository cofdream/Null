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
            helath    .text = "helath    :" + Unit.unitAttribute.Health     .ToString();
            maxhelath .text = "maxhelath :" + Unit.unitAttribute.MaxHealth  .ToString();
            atk       .text = "atk       :" + Unit.unitAttribute.Atk        .ToString();
            def       .text = "def       :" + Unit.unitAttribute.Def        .ToString();
            moveSpeed .text = "moveSpeed :" + Unit.unitAttribute.MoveSpeed  .ToString();
        }
    }
}
