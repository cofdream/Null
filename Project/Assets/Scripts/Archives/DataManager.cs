using UnityEngine;

namespace DA.DataModule
{
    public static class DataManager
    {
        public static DataBind<string> NameBind;
        public static void Init()
        {
            NameBind = new DataBind<string>(SetName);
        }

        private static void SetName(string old, string @new)
        {
            ArchivesData.Archive.Name = @new;
        }
    }
}