using Cofdream.Utils;
using DA.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Transform windowsTran = null;

    private BattlePanel battlePanel = null;

    Dictionary<string, UIWindow> windows = null;

    #region Life
    protected override void SingletonInit()
    {
        windowsTran = GameObject.Find("Windos").transform;
        windows = new Dictionary<string, UIWindow>();
    }
    public override void Free()
    {

    } 
    #endregion

    public void Init()
    {
        Debug.Log("UIManager Init Done.");
    }

    public void CreateBattlePanel()
    {
        GameObject game = Resources.Load<GameObject>("Prefabs/BattlePanel/BattlePanel");
        game = GameObject.Instantiate(game, windowsTran);
        battlePanel = new BattlePanel()
        {
            bind = game.GetComponent<BattlePanelBind>(),
        };
        battlePanel.Init();
    }


    public void OpenWindow<T>() where T : UIWindow, new()
    {
        string windowName = typeof(T).Name;
        GameObject gameObject = CreateWindow(windowName);
        if (gameObject == null)
        {
            Debug.LogError($"{windowName} not find.");
            return;
        }
        gameObject = GameObject.Instantiate(gameObject, windowsTran);

        UIWindow window = new T();
        window.SetContext(gameObject);
        windows.Add(windowName, window);
    }
    public void OpenWindow(string windowName)
    {
        GameObject gameObject = CreateWindow(windowName);
        if (gameObject == null)
        {
            Debug.LogError($"{windowName} not find.");
            return;
        }
        gameObject = GameObject.Instantiate(gameObject, windowsTran);

        var type = Type.GetType(windowName);
        var window = Activator.CreateInstance(type) as UIWindow;
        window.SetContext(gameObject);
        windows.Add(windowName, window);
    }

    private GameObject CreateWindow(string windowName)
    {
        GameObject win = Resources.Load<GameObject>($"Prefabs/BattlePanel/{windowName}");
        return win;
    }


    public static void OpenWindow<T>(string path) where T : UIWindow, new()
    {
        GameObject gameObject = Resources.Load<GameObject>("Prefabs/UI/" + path);
        gameObject = GameObject.Instantiate(gameObject, instance.windowsTran.GetChild(0));
        T t = new T();
        t.SetContext(gameObject);
        t.Awake();
        t.OnEnable();
    }
}