using DA.UI;
using UnityEngine;
using UnityEditor;
using NullNamespace;

namespace DA.UI
{
    public class SetNameWindow : UIWindowBase
    {
        SetNameWindowBind bind;

        public override void OnInit()
        {
            bind = BindBase as SetNameWindowBind;
            bind.buttonEnd.onClick.AddListener(EndInputName);

            // DialogWindow
        }

        public override void OnDestory()
        {
            bind.buttonEnd.onClick.RemoveListener(EndInputName);
        }

        public override void OnOpen()
        {
            
        }

        public void EndInputName()
        {
             ArchivesData.Instance.SaveName(bind.inputFieldName.text);

        }
    }
}