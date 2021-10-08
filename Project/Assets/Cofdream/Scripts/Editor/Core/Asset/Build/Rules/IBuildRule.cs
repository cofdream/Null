using UnityEditor;

namespace CofdreamEditor.Core.Asset
{
    public delegate void CreateCallback(AssetBundleBuild assetBundleBuild);
    public interface IBuildRule
    {
        void CreateAssetBundleBuild(CreateCallback addAssetBundleBuild);
    }
}
