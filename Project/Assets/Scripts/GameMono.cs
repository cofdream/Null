using DA.DataConfig;
using DA.DataModule;
using DA.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DA
{
    public sealed class GameMono : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        private void Initialize()
        {
            Debug.Log("Init Done.");
        }

        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            print("Enter");

            DataConfigManager.LoadAllConfig();

            ArchivesData.LoadArchivesData();

            DataManager.Init();

            UIManager.Instance.Init();

            var dispatcher = UIManager.Instance.OpenWindow("LoginWindow").Dispatcher;
            dispatcher.Subscribe((short)LoginWindow.LoginWindowEvent.LoginWindowClose, EnterGame);
        }

        private void EnterGame(short type)
        {
            // 读取当前游戏进度

            // 新玩家
            // 读取新玩家流程配置表

            // 老玩家
            // 传送至存档的位置

            // 启动游戏所需的各模块
        }



        private void OnDestroy()
        {
            DataConfigManager.DisposeAllConfig();

            ArchivesData.DeleArchiveFile();
        }
    }
}