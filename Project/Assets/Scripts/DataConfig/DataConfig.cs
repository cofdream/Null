using DA.Utils;
using System;
using UnityEngine;

namespace DA.DataConfig
{
    public static partial class DataConfigManager
    {
        private static byte[] LoadData(string path)
        {
            var data = Resources.Load<TextAsset>(path);
            if (data == null)
            {
                Debug.LogError($"数据读取失败 {path}");
                return new byte[0];
            }
            return data.bytes;
        }
    }
}