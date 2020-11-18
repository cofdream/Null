using UnityEngine;

namespace DA.DataModule
{
    public static class DataManager
    {
        public static DataBind<string> NameBind;
        public static DataBind<int> LvBind;
        public static void Init()
        {
            NameBind = new DataBind<string>(SetName);
            LvBind = new DataBind<int>(SetLv);
        }

        private static void SetName(string old, string @new)
        {
            ArchivesData.Archive.Name = @new;
        }
        private static void SetLv(int old, int @new)
        {
            ArchivesData.Archive.lv = @new;
        }
    }
}