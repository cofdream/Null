using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_IL : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MonoCtr_Test.Instance.StartCoroutine(ILRuntimeManager.AsyncLoadHotFixAssembly());
    }
    private void OnDestroy()
    {
        ILRuntimeManager.DisposeDLL();
    }

    IType initType;
    IMethod Func;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ILRuntimeManager.Appdomain.Invoke("HotFix_Project.Init", "Fun", null, null);

            //ILRuntimeManager.Appdomain.Invoke("HotFix_Project.Init", "Fun", null, "11");

            ILRuntimeManager.Appdomain.Invoke("HotFix_Project.Init", "Fun", null, 123);

            initType = ILRuntimeManager.Appdomain.LoadedTypes["HotFix_Project.Init"];

            Func = initType.GetMethod("Fun", 1);

            using (var ctx = ILRuntimeManager.Appdomain.BeginInvoke(Func))
            {
                ctx.PushInteger(123);
                ctx.Invoke();
            }
        }

    }
}
