using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofdream.Core.Asset
{
    public class Loader
    {
        public static Stack<Loader> loaderStack = new Stack<Loader>();
        public static Loader Take()
        {
            if (loaderStack.Count > 0) return loaderStack.Pop();
            return new Loader();
        }
        public static void Put(Loader loader)
        {
            loaderStack.Push(loader);
        }

        private Loader() { }


    }
}
