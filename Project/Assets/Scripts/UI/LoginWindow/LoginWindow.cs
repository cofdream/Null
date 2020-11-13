using DA.DataModule;
using UnityEngine;

namespace DA.UI
{
    public class LoginWindow : UIWindowBase
    {
        private LoginWindowBind bind;
        private LoginDataModule dataModule;
        //public override GameObject Context { get { return bind.gameObject; } set { bind = value.GetComponent<LoginWindowBind>(); } }

        #region Lift
        public override void OnInit()
        {
            bind = BindBase as LoginWindowBind;
            dataModule = new LoginDataModule();

            bind.Btn_Login.onClick.AddListener(Login);
        }
        public override void OnDestory()
        {
            bind.Btn_Login.onClick.RemoveListener(Login);

            bind = null;
            dataModule = null;
        }
        public override void OnOpen()
        {

        }
        public override void OnClose()
        {

        }
        #endregion

        public void Login()
        {
            if (dataModule.isFirst)
            {
                UIManager.Instance.OpenWindow("SetNameWindow");
            }
        }
    }
}