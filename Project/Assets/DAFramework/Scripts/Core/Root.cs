using DA.DataConfig;
using QFramework;
using UnityEngine;

namespace DA
{
    public sealed class Root : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Game Start.");
        }

        private void Start()
        {
            DataConfigManager.LoadAllConfig();

            ResMgr.Init();
            UI.UIManager.ShowWin("LobbyWindow");
        }
    }
}
