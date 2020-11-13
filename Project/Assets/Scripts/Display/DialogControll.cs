using DAProto;
using UnityEngine;

namespace DA.UI
{
    public delegate void DialogOver();
    public class DialogControll
    {
        private DialogWindow dialogWindow;


        public DialogControll()
        {
            dialogWindow = new DialogWindow();
        }
        public void DisplayDialog(Dialog_Config config, DialogOver overCallBack)
        {
            dialogWindow.ShowDialogBox(config, overCallBack);
        }
    }
}