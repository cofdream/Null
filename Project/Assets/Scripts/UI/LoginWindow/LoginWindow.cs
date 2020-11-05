using UnityEngine;
using UnityEngine.UI;

namespace DA.UI
{
    public class LoginWindow : UIWindow
    {
        public LoginWindowBind bind;

        public override void Awake()
        {
            bind.Btn_Login.onClick.AddListener(Login);
        }

        public override void OnDestory()
        {
            bind.Btn_Login.onClick.RemoveListener(Login);
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
        }
    }
    public class LoginWindow2 : UIWindow
    {
        Object bind;

        #region MyRegion
        public override void Awake()
        {
        }
        public override void OnDestory()
        {
        }
        public override void OnEnable()
        {
        }
        public override void OnDisable()
        {
        }
        #endregion
        public override void SetContext(Object context)
        {
            bind = context as Object;
        }
    }
}