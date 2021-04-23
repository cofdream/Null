using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DA.Node
{
    public class NodeRuntime : MonoBehaviour
    {
        static void Load()
        {
            var assembly = typeof(DASerializeClass).Assembly;

            //Type[] types = assembly.geta;



            //foreach (var type in types)
            //{
            //    MethodInfo[] methodInfos = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            //    foreach (MethodInfo methodInfo in methodInfos)
            //    {
            //        var attribute = methodInfo.GetCustomAttribute<SearchToolsAttribute>(true);

            //        if (attribute != null)
            //        {
            //            allSearchDataList.Add(new SearchData(attribute, methodInfo));
            //        }
            //    }
            //}

            //drawSearchDataList = new List<SearchData>(allSearchDataList);
        }


        public Text nameText;
        private string Name = "Node";

        public GameObject VariablePrefab;

        private void Awake()
        {

        }
    }
}