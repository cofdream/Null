using System.Collections;
using System.Collections.Generic;
using DA.AssetLoad;
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
            AssetLoader loader = AssetLoader.GetAssetLoader();

            var sp = loader.LoadAsset<Sprite>("Assets/Resources/img_icon_qq_Editor.png");
            Assert.IsNotNull(sp);

            loader.Unload("Assets/Resources/img_icon_qq_Editor.png");

            loader.UnloadAll();
            loader.ReleaseAssetLoader();
        }
        [Test]
        public void EditorLoadSync_Resources()
        {
            AssetLoader loader = AssetLoader.GetAssetLoader();

            loader.LoadAssetSync<Sprite>("Assets/Resources/img_icon_qq_EditorSync.png", (sp) =>
            {
                Assert.IsNotNull(sp);

                loader.Unload("Assets/Resources/img_icon_qq_EditorSync.png");
            });

            loader.UnloadAll();
            loader.ReleaseAssetLoader();
        }

        [Test]
        public void EditorLoad()
        {
            AssetLoader loader = AssetLoader.GetAssetLoader();

            loader.LoadAssetSync<Sprite>("Assets/Tests_Load/img_icon_qq_EditorSync.png", (sp) =>
            {

                Assert.IsNotNull(sp);

                loader.Unload("Assets/Tests_Load/img_icon_qq_EditorSync.png");
            });

            loader.UnloadAll();
            loader.ReleaseAssetLoader();
        }
        [Test]
        public void EditorLoadSync()
        {
            AssetLoader loader = AssetLoader.GetAssetLoader();

            loader.LoadAssetSync<Sprite>("Assets/Tests_Load/img_icon_qq.png", (sp) =>
            {
                Assert.IsNotNull(sp);

                loader.Unload("Assets/Tests_Load/img_icon_qq.png");
            });

            loader.UnloadAll();
            loader.ReleaseAssetLoader();
        }
    }
}
