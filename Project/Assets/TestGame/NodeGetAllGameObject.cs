using UnityEngine;
using UnityEngine.SceneManagement;

public class NodeGetAllGameObject : SKillNode
{
    public override void Run()
    {
        var allGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var gameObject in allGameObjects)
        {
            Debug.Log(gameObject.name);
        }
    }
}