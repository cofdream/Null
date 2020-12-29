using DA.Archives;
using DA.DataConfig;
using DA.DataModule;
using DA.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DA
{
    public static class GameMono
    {
        //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            Debug.Log("Init Done.");

            new GameObject("MonoController").AddComponent<MonoController>();

            MonoController.UpdataAction += StartGame;
        }


        private static void StartGame()
        {
            MonoController.UpdataAction -= StartGame;

            Debug.Log("Enter");

            DataConfigManager.LoadAllConfig();

            ArchivesData.LoadArchivesData();

            DataManager.Init();

            var dispatcher = UIManager.OpenWindow("LoginWindow").Dispatcher;
            dispatcher.Subscribe((short)LoginWindow.LoginWindowEvent.LoginWindowClose, EnterGame);
        }

        private static void EnterGame(short type)
        {
            // 读取当前游戏进度

            // 新玩家
            // 读取新玩家流程配置表

            // 老玩家
            // 传送至存档的位置

            // 启动游戏所需的各模块
        }

    }
}