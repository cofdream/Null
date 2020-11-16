using UnityEngine;

namespace DA.DataModule
{
    public class LoginDataModule
    {
        public bool ExistName()
        {
            return ArchivesData.Instance.Archive.Name == null;
        }

        public bool SaveName(string name)
        {
            ArchivesData.Instance.Archive.Name = name;
            return true;
        }
        
    }
}