using DA.DataConfig;
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

            ArchivesData.Instance.LoadArchive();

            UIManager.Instance.Init();



            UIManager.Instance.OpenWindow<LoginWindow>("LoginWindow");
        }



        private void OnDestroy()
        {
            DataConfigManager.DisposeAllConfig();

            ArchivesData.Instance.DeleArchiveFile();
        }
    }
}