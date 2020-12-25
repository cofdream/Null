using UnityEngine;
using UnityEngine.UI;

namespace NullNamespace
{
    public class TestLoad : MonoBehaviour
    {

        public Image image;
        public RawImage rawImage;
        public string path = "img_icon_qq";
        public string path2 = "img_icon_qq2";
        public bool isSprite = false;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                Load2();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Unload();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Resources.UnloadUnusedAssets();
            }
        }


        AssetLoader loader = new AssetLoader();
        void Load()
        {
            if (isSprite)
            {
                image.sprite = null;

            }
            else
            {
                rawImage.texture = null;
                //rawImage.texture = loader.LoadAsset<Texture>(path2);
            }
        }
        void Load2()
        {
            if (isSprite)
            {
                image.sprite = null;
                //loader.LoadAsset<Sprite>(path);
            }
            else
            {
                rawImage.texture = null;
                //loader.LoadAsset<Texture>(path2);

            }
        }

        void Unload()
        {
            image.sprite = null;
            //rawImage.texture = null;
            loader.UnloadAll();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Load Sprite"))
            {
                //image.sprite = loader.LoadAsset<Sprite>(path);
            }
            if (GUILayout.Button("Sprite Set Null"))
            {
                image.sprite = null;
            }


            if (GUILayout.Button("Load Texture"))
            {
                var tt = rawImage.texture;

                //rawImage.texture = loader.LoadAsset<Texture>(path2);

                Debug.Log(tt.Equals(rawImage.texture));
            }
            if (GUILayout.Button("Texture Set Null"))
            {
                rawImage.texture = null;
            }

            if (GUILayout.Button("Unload All"))
            {
                loader.UnloadAll();
            }
        }
    }
}