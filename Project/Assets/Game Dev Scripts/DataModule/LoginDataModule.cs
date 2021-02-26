
namespace DA.DataModule
{
    public class LoginDataModule
    {
        public void SaveName(string name)
        {
            DataManager.NameBind.Value = name;
        }
    }
}