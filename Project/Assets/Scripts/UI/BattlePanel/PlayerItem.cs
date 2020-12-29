using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem
{
    public PlayerItemBind bind = null;
    public int PlayerIndex { get; set; }

    //private RoleBattleData roleBattleData = null;
    public void Init()
    {
       // roleBattleData = StartRoot.DataModels.BattleDataModel.GetRoleBattleData(PlayerIndex);
        Refresh();
    }

    public void Refresh()
    {
       // bind.Name.text = roleBattleData.Name.ToString();
    }


}
