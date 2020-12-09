#define ILRUNTIME_ENABLEPDB //pdb 调试

using System.Collections;
using UnityEngine;
using System.IO;
using ILRuntime.Runtime.Enviorment;
using UnityEngine.Networking;

public sealed class ILRuntimeManager
{
#if UNITY_ANDROID
    const string hotFixDllPath = "/HotFix_Project.dll";
    const string hotFixPdbPath = "/HotFix_Project.pdb";
#else
    const string hotFixDllPath = "/HotFix_Project.dll";
    const string hotFixPdbPath = "/HotFix_Project.pdb";
#endif

    public static AppDomain Appdomain { get; private set; }

    static MemoryStream dllStream;
    static MemoryStream symbolStream;
    static ILRuntimeManager()
    {
        //首先实例化ILRuntime的AppDomain，AppDomain是一个应用程序域，每个AppDomain都是一个独立的沙盒
        Appdomain = new AppDomain();

#if ILRUNTIME_ENABLEPDB
        Appdomain.DebugService.StartDebugService(56000); 
#endif
    }

    public static IEnumerator AsyncLoadHotFixAssembly()
    {
        //正常项目中应该是自行从其他地方下载dll，或者打包在AssetBundle中读取，平时开发以及为了演示方便直接从StreammingAssets中读取，
        //正式发布的时候需要大家自行从其他地方读取dll


        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //这个DLL文件是直接编译HotFix_Project.sln生成的，已经在项目中设置好输出目录为StreamingAssets，在VS里直接编译即可生成到对应目录，无需手动拷贝
        //工程目录在Assets\Samples\ILRuntime\1.6\Demo\HotFix_Project~
        //以下加载写法只为演示，并没有处理在编辑器切换到Android平台的读取，需要自行修改

        // 加载DLL
#if UNITY_ANDROID
        UnityWebRequest request = new UnityWebRequest(Application.streamingAssetsPath + hotFixDllPath);
#else
        UnityWebRequest request = new UnityWebRequest("file:///" + Application.streamingAssetsPath + hotFixDllPath);
#endif
        request.downloadHandler = new DownloadHandlerBuffer(); ;

        yield return request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError)
            Debug.LogError(request.error);

        byte[] dll = request.downloadHandler.data;

        request.Dispose();

        //PDB文件是调试数据库，如需要在日志中显示报错的行号，则必须提供PDB文件，不过由于会额外耗用内存，正式发布时请将PDB去掉，下面LoadAssembly的时候pdb传null即可
#if ILRUNTIME_ENABLEPDB

#if UNITY_ANDROID
        request = new UnityWebRequest(Application.streamingAssetsPath + hotFixPdbPath);
#else
        request = new UnityWebRequest("file:///" + Application.streamingAssetsPath + hotFixPdbPath);
#endif
        request.downloadHandler = new DownloadHandlerBuffer(); ;


        yield return request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError)
            Debug.LogError(request.error);

        byte[] pdb = request.downloadHandler.data;

        request.Dispose();

        symbolStream = new MemoryStream(pdb);
#endif
        dllStream = new MemoryStream(dll);

        try
        {
            Appdomain.LoadAssembly(dllStream, symbolStream, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
        }
        catch
        {
            Debug.LogError("加载热更DLL失败，请确保已经通过VS打开Assets/HotFix_Project~/HotFix_Project.sln编译过热更DLL \n有可能是Dll读取的文件错误，检查文件");
        }

        InitializeILRuntime();
    }

    static void InitializeILRuntime()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        Appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        //这里做一些ILRuntime的注册
    }

    public static void DisposeDLL()
    {
        if (dllStream != null)
            dllStream.Close();
        if (symbolStream != null)
            symbolStream.Close();
        dllStream = null;
        symbolStream = null;
    }
}

// ILR
// 创建类实例
//    1.可以通过调用静态函数去返回实例
//    2.继承原工程的类，通过名字去创建