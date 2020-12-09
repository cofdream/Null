using UnityEngine;

public class BattlePanel
{
    public BattlePanelBind bind = null;

    //public BattleDataModel battleDataModel = null;

    public void Init()
    {
        //battleDataModel = ModelManager.Instance.GetDataModel().BattleDataModel;

        CreatPlayer();
    }
    public void CreatPlayer()
    {
        //int length = battleDataModel.roleBattleDatas.Length;

        //for (int i = 0; i < length; i++)
        //{
        //    var gameObject = GameObject.Instantiate(bind.PlayerItem, bind.PlayerRoot);

        //    PlayerItem playerItem = new PlayerItem()
        //    {
        //        bind = gameObject.GetComponent<PlayerItemBind>(),
        //        PlayerIndex = i,
        //    };
        //    playerItem.Init();
        //}
    }

}