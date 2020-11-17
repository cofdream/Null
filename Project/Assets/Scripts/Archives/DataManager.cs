using UnityEngine;

namespace DA.DataModule
{
    public static class DataManager
    {
        public static DataBind<string?> Name;
        public static DataBind<int?> Lv;


        public static void Init()
        {
            var archive = ArchivesData.Instance.Archive;
            Name = new DataBind<string?>(archive.Name);
            Lv = new DataBind<int?>(archive.lv);
        }
    }
}