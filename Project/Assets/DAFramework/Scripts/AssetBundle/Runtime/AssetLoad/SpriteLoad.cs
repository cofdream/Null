using UnityEngine;
using UnityEngine.UI;

namespace DA
{
    public enum IconType : ushort
    {
        BagIcon,
        HeadIcon,
        TaskIcon,
    }

    public class SpriteLoad : MonoBehaviour
    {
        [SerializeField] private Image image;

        #region Tip
        [Tooltip(
    "1 = BagIcon \n" +
    "2 = HeadIcon \n" +
    "3 = TaskIcon \n")]
        #endregion
        [SerializeField] private ushort iconType;


        private AssetLoader loader;

        private string spriteName;


        private void Start()
        {
            //AssetLoader.Init();

            loader = AssetLoader.Loader;

            //  loader.Load<AssetBundle>(@"E:\Git\Null\Project\AssetBundle\Windows\cube");

            Debug.Log("11");
        }


        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    image.sprite = loader.Load<Sprite>("img_icon_qqSSS");
            //}
        }


        public void Load(string name)
        {
            spriteName = name;

            string pathRoot;
            switch (iconType)
            {
                case 1: pathRoot = "Assets/Resources/Icons"; break;
                case 2: pathRoot = "Assets/Resources/Icons"; break;
                case 3: pathRoot = "Assets/Resources/Icons"; break;
                default: pathRoot = string.Empty; break;
            }

            //loader.LoadAsync<AssetBundle>(pathRoot, Loaded);
        }

        private void Loaded(UnityEngine.Object obj)
        {
            var assetBundle = obj as AssetBundle;
            image.sprite = assetBundle.LoadAsset<Sprite>(spriteName);
        }

        private void OnDestroy()
        {
            loader?.UnloadAll();
            loader = null;
        }
    }
}