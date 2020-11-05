using Cofdream.Utils;
using System;
using System.IO;
using UnityEngine;

public sealed class ArchivesData : Singleton<ArchivesData>
{
    private readonly string path = Application.persistentDataPath + "/Archive";
    public static Archive Archive
    {
        get;
        private set;
    }

    protected override void SingletonInit()
    {
        base.SingletonInit();
    }

    public void LoadArchive()
    {

        if (File.Exists(path) == false)
        {
            Archive = new Archive();
            Archive.Init();

            File.WriteAllText(path, JsonUtility.ToJson(Archive));
        }
        else
        {
            Archive = JsonUtility.FromJson<Archive>(path);
        }
    }

    public void SaveArchive()
    {
        File.WriteAllText(path, JsonUtility.ToJson(Archive));
    }


    public void SaveName(string name)
    {
        Archive.Name = name;
    }



    public void DeleArchiveFile()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else
        {
            Debug.Log("Delet archive failure,Path error");
        }
    }
}