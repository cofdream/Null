using NullNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoot : MonoBehaviour
{
    //private RoleBattleData player;
    private int playerPetIndex;
    //private RoleBattleData enemy;
    private int enemyPetIndex;

    private int isPlayerWin;

    private void Awake()
    {
        //LoadModel loadModel = new LoadModel();

        

        //loadModel.InitLoad();
    }


    void Start()
    {
        //DataModels = new DataModels();
        //DataModels.InitBattleDataModel();

        //player = DataModels.BattleDataModel.roleBattleDatas[0];
        //playerPetIndex = 0;
        //enemy = DataModels.BattleDataModel.roleBattleDatas[1];
        //enemyPetIndex = 0;

        //isPlayerWin = -1;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    HurtEnemy(Random.Range(0, 10));
        //    DataModels.BattleDataModel.AddBattleRoundCount();
        //}
        //else if (Input.GetKeyDown(KeyCode.J))
        //{
        //    HurtPlayer(Random.Range(0, 10));
        //    DataModels.BattleDataModel.AddBattleRoundCount();
        //}
    }

    private void OnDestroy()
    {
        
    }

    private void HurtPlayer(int hurtValue)
    {
        //ReduceHp(player, playerPetIndex, hurtValue);
        //Debug.Log("Player Hurt,vlue " + hurtValue);
        //if (enemy.PetBattleDatas[enemyPetIndex].HP == 0)
        //{
        //    playerPetIndex++;
        //    if (playerPetIndex >= player.PetBattleDatas.Length)
        //    {
        //        isPlayerWin = 1;
        //        GameOver();
        //    }
        //}
    }
    private void HurtEnemy(int hurtValue)
    {
        //ReduceHp(enemy, enemyPetIndex, hurtValue);
        //Debug.Log("Enemy Hurt,vlue " + hurtValue);
        //if (enemy.PetBattleDatas[enemyPetIndex].HP == 0)
        //{
        //    enemyPetIndex++;
        //    if (enemyPetIndex >= enemy.PetBattleDatas.Length)
        //    {
        //        isPlayerWin = 0;
        //        GameOver();
        //    }
        //}
    }
    //public void ReduceHp(RoleBattleData role, int petIndex, int reduceValue)
    //{
    //    int hp = role.PetBattleDatas[petIndex].HP - reduceValue;
    //    if (hp <= 0)
    //    {
    //        role.PetBattleDatas[petIndex].HP = 0;
    //    }
    //    else
    //    {
    //        role.PetBattleDatas[petIndex].HP = hp;
    //    }
    //}

    //public void GameOver()
    //{
    //    if (isPlayerWin == 0)
    //    {
    //        Debug.Log("失败");
    //    }
    //    else if (isPlayerWin == -1)
    //    {
    //        Debug.Log("胜利");
    //    }

    //    Destroy(this.gameObject);
    //}
}

//public class LoadModel
//{
//    public void InitLoad()
//    {
//        Debug.Log("Init Load Done.");
//    }
//}