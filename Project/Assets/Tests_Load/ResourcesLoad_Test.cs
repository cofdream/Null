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
            AssetLoader loader = new AssetLoader();

            var sp = loader.LoadAsset<Sprite>("resources://img_icon_qq_Res");
            Assert.IsNotNull(sp);


            loader.Unload("resources://img_icon_qq_Res");
        }
        [Test]
        public void ResLoadSync()
        {
            var loader = new AssetLoader();
            loader.LoadAssetSync<Sprite>("resources://img_icon_qq_ResSync", (sp) =>
            {
                Assert.IsNotNull(sp);

                loader.Unload("resources://img_icon_qq_ResSync");
            });
        }
    }
}