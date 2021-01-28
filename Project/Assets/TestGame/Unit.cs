using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public Skill ChopSkill = new Skill();


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChopSkill.Cast();
        }
    }
}

