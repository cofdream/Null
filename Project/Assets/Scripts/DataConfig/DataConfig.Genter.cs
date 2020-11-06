using UnityEngine;
using DAProto;
using Google.Protobuf;

namespace DA.DataConfig
{
    public static partial class DataConfigManager
    {
        public static Excel_Tack_Config Tack_Config_Array { get; private set; }
        public static Excel_UIWindow_Config UIWindow_Config_Array { get; private set; }

        public static void LoadAllConfig()
        {
            Tack_Config_Array = LoadConfig<Excel_Tack_Config>(LoadData("DataConfig/Tack_Config"));
            UIWindow_Config_Array = LoadConfig<Excel_UIWindow_Config>(LoadData("DataConfig/UIWindow_Config"));
        }
        public static void DisposeAllConfig()
        {
            Tack_Config_Array = null;
        }
        public static T LoadConfig<T>(byte[] data) where T : IMessage<T>, new()
        {
            var parser = new MessageParser<T>(() => new T());
            return parser.ParseFrom(data);
        }

        public static UIWindow_Config GetUIWindowConfig(string windowName)
        {
            var datas = UIWindow_Config_Array.Data.Values;
            foreach (var data in datas)
            {
                if (data.WindowName == windowName)
                {
                    return data;
                }
            }
            return null;
        }
    }
}