using DA.UI;
using UnityEngine;
using UnityEditor;
using DAProto;
using DA.DataConfig;
using DA.DataModule;

namespace DA.UI
{
    public class SetNameWindow : UIWindowBase
    {
        public enum SetNameWindowEvent : short
        {
            SetNameEnd,
        }

        SetNameWindowBind bind;

        LoginDataModule loginDataModule;
        public override void OnInit()
        {
            base.OnInit();
            bind = BindBase as SetNameWindowBind;
            loginDataModule = new LoginDataModule();
        }

        public override void OnDestory()
        {
            Object.Destroy(bind);
        }

        public override void OnOpen()
        {
            ShowDialog();
        }

        public override void OnClose()
        {
            DisposeCallBack();
            OnDestory();
        }

        public void EndInputName()
        {
            Debug.Log(11);
            string name = bind.inputFieldName.text;
            if (string.IsNullOrWhiteSpace(name))
            {
                Debug.Log("重新输入名字！");
            }
            else
            {
                if (loginDataModule.SaveName(name))
                {
                    bind.buttonEnd.onClick.RemoveListener(EndInputName);
                }
                eventDispatcher.SendEvent((short)SetNameWindowEvent.SetNameEnd);
                OnClose();
            }
        }

        public void ShowDialog()
        {
            var config = DataConfigManager.GetDialogConfig(1);
            if (config != null)
            {
                UIManager.Instance.DialogManager.DisplayDialog(config, DialogOver);
            }
            else
            {
                DialogOver();
            }
        }
        private void DialogOver()
        {
            Debug.Log("对话结束");
            bind.buttonEnd.onClick.AddListener(EndInputName);
        }
    }
}