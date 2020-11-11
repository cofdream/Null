using DA.Utils;
using System;
using System.IO;
using UnityEngine;

public sealed class ArchivesData : Singleton<ArchivesData>
{
    private readonly string path = Application.persistentDataPath + "/Archive";
    private Archive archive;
    public Archive Archive
    {
        get { return archive; }
    }

    private ArchivesData() { }
    protected override void SingletonInit()
    {
        base.SingletonInit();
    }

    public void LoadArchive()
    {
        if (File.Exists(path) == false)
        {
            archive = new Archive();
            archive.Init();

            File.WriteAllText(path, JsonUtility.ToJson(archive));
        }
        else
        {
            archive = JsonUtility.FromJson<Archive>(File.ReadAllText(path));
        }
    }

    public void SaveArchive()
    {
        File.WriteAllText(path, JsonUtility.ToJson(archive));
    }


    public void SaveName(string name)
    {
        archive.Name = name;
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