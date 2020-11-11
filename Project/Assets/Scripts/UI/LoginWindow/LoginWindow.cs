using UnityEngine;

namespace DA.UI
{
    public class LoginWindow : UIWindowBase
    {
        public LoginWindowBind bind;
        //public override GameObject Context { get { return bind.gameObject; } set { bind = value.GetComponent<LoginWindowBind>(); } }

        #region Lift
        public override void OnInit()
        {
            bind = BindBase as LoginWindowBind;

            bind.Btn_Login.onClick.AddListener(Login);
        }
        public override void OnDestory()
        {
            bind.Btn_Login.onClick.RemoveListener(Login);

            bind = null;
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
            UIManager.Instance.OpenWindow("LobbyWindow");
        }
    }
}