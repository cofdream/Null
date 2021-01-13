using DA.Archives;
using DA.DataConfig;
using DA.DataModule;
using DA.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    [DisallowMultipleComponent]
    public sealed class Splash : MonoBehaviour
    {
        private void Awake()
        {
            var ins = GameObject.FindObjectsOfType<Splash>();
            if (ins.Length > 1)
            {
                DestroyImmediate(this);
                Debug.LogError("There are multiple " + typeof(Splash));
                return;
            }

            Debug.Log("Awake Done.");
        }

        private void Start()
        {
            Debug.Log("Start Done.");

            DataConfigManager.LoadAllConfig();

            ArchivesData.LoadArchivesData();

            DataManager.Init();

            UIManager.OpenWindow("LoginWindow");

            SceneManager.UnloadSceneAsync(0);
        }
    }
}