using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix_Project
{
    public class Init
    {
        const string hellow = "Hello";
        public static void Fun()
        {
            Debug.Log(hellow);
        }
        public static void Fun(int a)
        {
            //Debug.Log(a);
        }
    }
}
