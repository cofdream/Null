using UnityEditor;

namespace CofdreamEditor.Core.Asset
{
    public delegate void CreateCallback(AssetBundleBuild assetBundleBuild);
    public interface IBuildRule
    {
        int GetAssetBundleBuildCount();
        void CreateAssetBundleBuild(CreateCallback addAssetBundleBuild);
    }
}
