using DA.AssetsBundle;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DA
{
	public partial class AssetLoader
	{
#if UNITY_EDITOR
        public static bool IsSimulationMode = false;
#endif
		private static List<AssetLoader> loaders = new List<AssetLoader>();

        public static AssetLoader Loader
        {
            get
            {
                var loader = new AssetLoader();
                loaders.Add(loader);
                return loader;
            }
        }

    }
}