using UnityEngine;
using UnityEngine.UI;

namespace DA.UI
{
    public class LoginWindow : UIWindow
    {
        public LoginWindowBind bind;
        public override GameObject Context { set { value.GetComponent<LoginWindowBind>(); } }
        #region Lift
        public override void Awake()
        {
            #region AddListener
            bind.Btn_Login.onClick.AddListener(Login);
            #endregion
        }
        public override void OnDestory()
        {
            bind = null;

            #region RemoveListener
            bind.Btn_Login.onClick.RemoveListener(Login);
            #endregion
        }
        public override void OnEnable(object age)
        {
        }
        public override void OnDisable()
        {
        }

        #endregion

        public void Login()
        {
            // 开始读取
        }
    }
}