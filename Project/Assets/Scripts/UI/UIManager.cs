using Cofdream.Utils;
using DA.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Transform Windows = null;

    private BattlePanel battlePanel = null;

    Dictionary<string, IUIWindow> windows = null;

    UIManager() { }

    protected override void SingletonInit()
    {
        Windows = GameObject.Find("Windos").transform;
        windows = new Dictionary<string, IUIWindow>();
    }
    public void Init()
    {
        Debug.Log("UIManager Init Done.");
    }

    public void CreateBattlePanel()
    {
        GameObject game = Resources.Load<GameObject>("Prefabs/BattlePanel/BattlePanel");
        game = GameObject.Instantiate(game, Windows);
        battlePanel = new BattlePanel()
        {
            bind = game.GetComponent<BattlePanelBind>(),
        };
        battlePanel.Init();
    }


    public void OpenWindow<T>() where T : IUIWindow, new()
    {
        string windowName = typeof(T).Name;
        GameObject gameObject = CreateWindow(windowName);
        if (gameObject == null)
        {
            Debug.LogError($"{windowName} not find.");
            return;
        }
        gameObject = GameObject.Instantiate(gameObject, Windows);

        IUIWindow window = new T();
        window.SetBind(gameObject);
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
        gameObject = GameObject.Instantiate(gameObject, Windows);

        var type = Type.GetType(windowName);
        IUIWindow window = Activator.CreateInstance(type) as IUIWindow;
        window.SetBind(gameObject);
        windows.Add(windowName, window);
    }

    private GameObject CreateWindow(string windowName)
    {
        GameObject win = Resources.Load<GameObject>($"Prefabs/BattlePanel/{windowName}");
        return win;
    }


    public static void OpenWindow<T>(string path) where T : IUIWindow, new()
    {
        GameObject gameObject = Resources.Load<GameObject>("Prefabs/UI/" + path);
        gameObject = GameObject.Instantiate(gameObject, instance.Windows.GetChild(0));
        T t = new T();
        t.SetBind(gameObject);
        t.Awake();
        t.OnEnable();
    }
}