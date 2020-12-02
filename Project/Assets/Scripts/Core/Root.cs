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

            UI.UIManager.ShowWin("LobbyWindow");
        }
    }
}
