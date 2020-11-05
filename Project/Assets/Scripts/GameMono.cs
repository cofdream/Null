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

            CreateRole createRole = new CreateRole();
        }

    }
}