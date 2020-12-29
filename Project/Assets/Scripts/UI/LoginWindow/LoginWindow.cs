using DA.DataConfig;
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

        #region Lift
        public override void OnInit()
        {
            base.OnInit();

            bind = BindBase as LoginWindowBind;
            dataModule = new LoginDataModule();

            bind.Btn_Login.onClick.AddListener(ShowDialog);
            bind.Btn_EndName.onClick.AddListener(EndInputName);
        }
        public override void OnOpen()
        {
            bind.DefaultPanelGameObj.SetActive(true);
            bind.SetNamePanelGameObj.SetActive(false);
        }
        public override void OnDestory()
        {
            bind.Btn_Login.onClick.RemoveListener(ShowDialog);
            bind.Btn_EndName.onClick.RemoveListener(EndInputName);

            bind = null;
            dataModule = null;

            Object.Destroy(bind);
        }
        public override void OnClose()
        {
            DisposeCallBack();

            OnDestory();
        }
        #endregion

        private void SendCloseEvent()
        {
            eventDispatcher.SendEvent((short)LoginWindowEvent.LoginWindowClose);
            OnClose();
        }

        private void ShowDialog()
        {
            var config = DataConfigManager.GetDialogConfig(1);
            if (config != null)
            {
                UIManager.DialogManager.DisplayDialog(config, DialogOver);
            }
            else
            {
                DialogOver();
            }
        }
        private void DialogOver()
        {
            Debug.Log("对话结束");

            bind.SetNamePanelGameObj.SetActive(true);
        }
        private void EndInputName()
        {
            DataManager.NameBind.Value = bind.NameField.text;

            SendCloseEvent();
        }
    }
}