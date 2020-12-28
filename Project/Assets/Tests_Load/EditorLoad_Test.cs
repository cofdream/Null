using System.Collections;
using System.Collections.Generic;
using NullNamespace;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class EditorLoad_Test
    {
        [Test]
        public void EditorLoad_Resources()
        {
            AssetLoader loader = new NullNamespace.AssetLoader();

            var sp = loader.LoadAsset<Sprite>("Assets/Resources/img_icon_qq_Editor.png");
            Assert.IsNotNull(sp);

            loader.Unload("Assets/Resources/img_icon_qq_Editor.png");
        }
        [Test]
        public void EditorLoadSync_Resources()
        {
            AssetLoader loader = new NullNamespace.AssetLoader();

            loader.LoadAssetSync<Sprite>("Assets/Resources/img_icon_qq_EditorSync.png", (sp) =>
            {
                Assert.IsNotNull(sp);

                loader.Unload("Assets/Resources/img_icon_qq_EditorSync.png");
            });

        }

        [Test]
        public void EditorLoad()
        {
            AssetLoader loader = new NullNamespace.AssetLoader();

            loader.LoadAssetSync<Sprite>("Assets/Tests_Load/img_icon_qq_EditorSync.png", (sp) =>
            {

                Assert.IsNotNull(sp);

                loader.Unload("Assets/Tests_Load/img_icon_qq_EditorSync.png");
            });
        }
        [Test]
        public void EditorLoadSync()
        {
            AssetLoader loader = new NullNamespace.AssetLoader();

            loader.LoadAssetSync<Sprite>("Assets/Tests_Load/img_icon_qq.png2", (sp) =>
            {
                Assert.IsNotNull(sp);

                loader.Unload("Assets/Tests_Load/img_icon_qq.png2");
            });

        }
    }
}
