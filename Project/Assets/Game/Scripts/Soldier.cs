using UnityEngine;

namespace Game
{
    public class Soldier
    {
        public GameObject soldierPrefab;

        public void Create(Vector3 position, Quaternion rotation, Transform parent)
        {
            GameObject soldierGameObject = GameObject.Instantiate<GameObject>(soldierPrefab, position, rotation, parent);
        }
    }
}