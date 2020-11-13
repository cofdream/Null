using DA.UI;
using UnityEngine;
using UnityEditor;
using DAProto;
using DA.DataConfig;

namespace DA.UI
{
    public class SetNameWindow : UIWindowBase
    {
        SetNameWindowBind bind;
        public override void OnInit()
        {
            bind = BindBase as SetNameWindowBind;

        }

        public override void OnDestory()
        {

        }

        public override void OnOpen()
        {
            ShowDialog();
        }

        public void EndInputName()
        {
            string name = bind.inputFieldName.text;
            if (string.IsNullOrWhiteSpace(name))
            {
                ArchivesData.Instance.SaveName(bind.inputFieldName.text);
                bind.buttonEnd.onClick.RemoveListener(EndInputName);
                // close UIManager.Instance
            }
            else
            {
                Debug.Log("重新输入名字！");
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