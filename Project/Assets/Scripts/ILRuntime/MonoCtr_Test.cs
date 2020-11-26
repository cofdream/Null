using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoCtr_Test : MonoBehaviour
{
    static MonoCtr_Test instance;
    public static MonoCtr_Test Instance { get => instance; }
    static MonoCtr_Test()
    {
        instance = new GameObject().AddComponent<MonoCtr_Test>();
        DontDestroyOnLoad(instance.gameObject);
    }
}