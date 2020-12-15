using DA;
using DA.AssetsBundle;
using System;
using System.Collections;
using UnityEngine;

namespace NullNamespace
{
    public class Test1323 : MonoBehaviour
    {


        public void Start()
        {

            //AssetBundle.LoadFromFileAsync("1").completed += (a) =>
            //{
            //    Debug.Log($"{Time.time} 111 CallBack");
            //};

            //AssetBundle.LoadFromFileAsync("22").completed += (a) =>
            //{
            //    Debug.Log($"{Time.time} 22 CallBack");
            //};

            //AssetBundle.LoadFromFileAsync("333").completed += (a) =>
            //{
            //    Debug.Log($"{Time.time} 333 CallBack");
            //};
            //StartCoroutine(Test());

            //string dlcPath = @"C:\Users\v_cqqcchen\AppData\LocalLow\DefaultCompany\Project\DLC\8f429582379ed5ffce9d866a4363f93b.bundle";

            //AssetLoader loader = new AssetLoader();


            //loader.LoadAsync<GameObject>(dlcPath, (asset) => {

            //    Debug.Log($"{Time.time} 111 CallBack");
            //    Instantiate(asset.assetBundle.LoadAsset<GameObject>("TestObj"));
            //});


            string dlcPath2 = @"C:\Users\v_cqqcchen\AppData\LocalLow\DefaultCompany\Project\DLC\3cfcd7a62a1174e2fb82f5fa0cf3b839.bundle";

            var t2 = AssetBundle.LoadFromFileAsync(dlcPath2);
            t2.completed += (a) =>
            {
                //Debug.Log($"{Time.time} 111 CallBack");
                //t2.assetBundle.LoadAsset<Sprite>("img_icon_qq");
            };

            //AssetBundle.LoadFromFileAsync(dlcPath).completed += (a) =>
            //{
            //    Debug.Log($"{Time.time} 22 CallBack");
            //};

            //AssetBundle.LoadFromFileAsync(dlcPath).completed += (a) =>
            //{
            //    Debug.Log($"{Time.time} 33 CallBack");
            //};

            //AssetBundle.LoadFromFileAsync(dlcPath).completed += (a) =>
            //{
            //    Debug.Log($"{Time.time} 44 CallBack");
            //};

            StartCoroutine(Test2());
        }

        private void Update()
        {
            Debug.Log("update");
        }

        IEnumerator Test()
        {
            while (true)
            {
                Debug.Log("Test");
                yield return null;
            }
        }

        IEnumerator Test2()
        {
            while (true)
            {
                Debug.Log("Test2");
                yield return null;
            }
        }
    }
}