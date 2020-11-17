using DA.Utils;
using System;
using System.IO;
using UnityEngine;

namespace DA.DataModule
{
    public sealed class ArchivesData : Singleton<ArchivesData>
    {
        private readonly string path = Application.persistentDataPath + "/Archive";
        private Archive archive;
        public Archive Archive
        {
            get { return archive; }
        }

        private ArchivesData() { }

        protected override void InitSingleton()
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
}