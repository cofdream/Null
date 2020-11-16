using DA.DataModule;
using DA.Event;
using UnityEngine;

namespace DA.UI
{
    public class LoginWindow : UIWindowBase
    {
        public enum LoginWindowEvent : short
        {
            LoginWindowClose,
        }

        private LoginWindowBind bind;
        private LoginDataModule dataModule;
        //public override GameObject Context { get { return bind.gameObject; } set { bind = value.GetComponent<LoginWindowBind>(); } }

        #region Lift
        public override void OnInit()
        {
            base.OnInit();

            bind = BindBase as LoginWindowBind;
            dataModule = new LoginDataModule();

            bind.Btn_Login.onClick.AddListener(Login);
        }
        public override void OnDestory()
        {
            bind.Btn_Login.onClick.RemoveListener(Login);

            bind = null;
            dataModule = null;

            Object.Destroy(bind);
        }
        public override void OnOpen()
        {

        }
        public override void OnClose()
        {
            DisposeCallBack();

            OnDestory();
        }
        #endregion

        public void Login()
        {
            if (dataModule.ExistName())
            {
                var dispatcher = UIManager.Instance.OpenWindow("SetNameWindow").Dispatcher;
                dispatcher.Subscribe((short)SetNameWindow.SetNameWindowEvent.SetNameEnd, SetNameEnd);
            }
            else
            {
                SetNameEnd(short.MaxValue);
            }
        }
        public void SetNameEnd(short type)
        {
            eventDispatcher.SendEvent((short)LoginWindowEvent.LoginWindowClose);
            OnClose();
        }
    }
}