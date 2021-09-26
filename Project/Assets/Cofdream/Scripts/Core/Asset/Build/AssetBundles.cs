using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofdream.Core.Asset
{
    public class AssetBundles : ScriptableObject
    {
        public Bundle[] Builds;
    }

    [System.Serializable]
    public class Bundle
    {
        public string Name;
        public string Path;
    }

}