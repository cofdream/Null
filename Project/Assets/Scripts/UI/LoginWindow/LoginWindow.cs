using UnityEngine;
using UnityEngine.UI;

namespace DA.UI
{
    public class LoginWindow : UIWindow
    {
        #region Lift
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
        #endregion

        public void Login()
        {
            // 开始读取
        }
    }
}