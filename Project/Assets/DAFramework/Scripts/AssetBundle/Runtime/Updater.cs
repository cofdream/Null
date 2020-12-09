using DA.AssetsBundle;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace DA
{
    public class Updater : MonoBehaviour
    {
        Action update;
        string dlcPath;

        [SerializeField]
        private string baseURL = "http://10.46.88.66/Windows/";

        public  int newFileCount = 0;
        public  int DownCount = 0;

        void Start()
        {
            dlcPath = Application.persistentDataPath + "/DLC/"; ;
            if (!Directory.Exists(dlcPath)) Directory.CreateDirectory(dlcPath);

            Message("检测更新文件...");

            //UnityWebRequest webRequest = UnityWebRequest.Get(GetDownURL(Versions.FileName));
            //webRequest.downloadHandler = new DownloadHandlerFile(dlcPath + Versions.FileName);
            //var asyncOperation = webRequest.SendWebRequest();
            //asyncOperation.completed += (operation) =>
            //{
            //    if (webRequest.isNetworkError || webRequest.isHttpError)
            //    {
            //        Message("获取服务器版本错误： " + webRequest.error);
            //        //return;
            //    }


            //};
            Versions.serverVersion = Versions.LoadFullVersion(dlcPath + Versions.FileName);
            List<VFile> newFiles = Versions.GetNewFiles(PatchId.Level1, dlcPath);
            newFileCount = newFiles.Count;
            foreach (var file in newFiles)
            {
                UnityWebRequest webRequest2 = UnityWebRequest.Get(GetDownURL(file.name));
                webRequest2.downloadHandler = new DownloadHandlerFile(dlcPath + file.name);
                var asyncOperation2 = webRequest2.SendWebRequest();
                asyncOperation2.completed += (op) =>
                {

                    if (webRequest2.isNetworkError || webRequest2.isHttpError)
                    {
                        Message("获取文件错误： " + webRequest2.error);
                            //return;
                        }
                    newFileCount++;
                };
            }

            update += CheckUpdateFile;
        }
        void Update()
        {
            update?.Invoke();
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    AssetBundle.LoadFromFile();
            //}
        }

        void CheckUpdateFile()
        {
            update -= CheckUpdateFile;


        }


        void Message(string content)
        {
            Debug.Log($"Message: {content}");
        }


        string GetDownURL(string fileName)
        {
            return $"{baseURL}{BuildScript.GetPlatformName()}/" + fileName;
        }
    }

}