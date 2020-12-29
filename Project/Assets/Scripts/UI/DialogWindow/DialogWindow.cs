using UnityEngine;
using DAProto;
using DA.DataConfig;

namespace DA.UI
{
    public class DialogWindow : UIWindowBase
    {


        public void ShowDialogBox(Dialog_Config config, DialogOver dialogCallBack)
        {
            Debug.Log($"ID: {config.Id} Next {config.NextDialogId}  Content {config.Content}");
            if (config.NextDialogId == 0)
            {
                dialogCallBack?.Invoke();
            }
            else
            {
                ShowDialogBox(DataConfigManager.GetDialogConfig(config.NextDialogId), dialogCallBack);
            }
        }
    }
}