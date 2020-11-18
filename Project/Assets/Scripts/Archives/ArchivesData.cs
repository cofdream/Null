using DA.Utils;
using System.IO;
using UnityEngine;

namespace DA.DataModule
{
    public static class ArchivesData
    {
        private static readonly string path = Application.persistentDataPath + "/Archive";
        private static Archive archive;
        public static Archive Archive
        {
            get { return archive; }
        }

        public static void LoadArchivesData()
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

        public static void SaveArchive()
        {
            File.WriteAllText(path, JsonUtility.ToJson(archive));
        }

        public static void DeleArchiveFile()
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