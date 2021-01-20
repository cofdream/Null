using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using DA.AssetLoad;

namespace Tests
{
    public class ResourcesLoad_Test
    {

        [Test]
        public void ResourcesLoad()
        {
            AssetLoader loader = AssetLoader.GetAssetLoader();

            var sp = loader.LoadAsset<Sprite>("resources://img_icon_qq_Res");
            Assert.IsNotNull(sp);

            loader.Unload("resources://img_icon_qq_Res");

            loader.UnloadAll();
            loader.ReleaseAssetLoader();
        }
        [Test]
        public void ResLoadSync()
        {
            var loader = AssetLoader.GetAssetLoader();
            loader.LoadAssetSync<Sprite>("resources://img_icon_qq_ResSync", (sp) =>
            {
                Assert.IsNotNull(sp);

                loader.Unload("resources://img_icon_qq_ResSync");
            });

            loader.UnloadAll();
            loader.ReleaseAssetLoader();
        }
    }
}