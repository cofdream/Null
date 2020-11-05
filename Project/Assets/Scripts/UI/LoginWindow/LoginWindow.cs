using UnityEngine;
using UnityEngine.UI;

namespace DA.UI
{
    public class LoginWindow : UIWindow
    {
        public LoginWindowBind bind;

        public override void Awake()
        {
            #region AddListener
            bind.Btn_Login.onClick.AddListener(Login);
            #endregion
        }

        public override void OnDestory()
        {
            #region RemoveListener
            bind.Btn_Login.onClick.RemoveListener(Login);
            #endregion
        }
        public override void OnEnable()
        {
        }
        public override void OnDisable()
        {
        }
        public override void SetContext(Object context)
        {
            bind = context as LoginWindowBind;
        }


        public void Login()
        {
            // 开始读取
        }
    }
}