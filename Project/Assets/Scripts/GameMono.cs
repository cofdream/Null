using DA.DataConfig;
using DA.DataModule;
using DA.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullNamespace
{
    public class GameMono : MonoBehaviour
    {
        private void Awake()
        {
            Initialize();
        }
        private void Initialize()
        {
            // 加载模块

            // 初始化各类

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

            ArchivesData.Instance.LoadSingleton();
            DataManager.Init();

            UIManager.Instance.Init();

            WorldManager.Instance.LoadSingleton();

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

            DataManager.Name.Bind((a, b) => { print($"old {a}  new {b}"); });

            DataManager.Lv.Bind((a, b) => { print($"old {a}  new {b}"); });

            DataManager.Name.Value = "111";

            print($"Arc {ArchivesData.Instance.Archive.Name}");

            DataManager.Lv.Value = 10;

            print($"Arc {ArchivesData.Instance.Archive.lv}");


            DataManager.Name.Value = "222";

            print($"Arc {ArchivesData.Instance.Archive.Name}");

            DataManager.Lv.Value = 20;

            print($"Arc {ArchivesData.Instance.Archive.lv}");

        }



        private void OnDestroy()
        {
            DataConfigManager.DisposeAllConfig();

            ArchivesData.Instance.DeleArchiveFile();
        }
    }
}