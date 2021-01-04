using UnityEditor;
using UnityEngine;

namespace Editor {
    public static class AssetBundleUtil {
        [MenuItem("Pack AssetBundle", menuItem = "Tools/Pack AssetBundle")]
        public static void PackAssetBundle() {
            BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath, 0, BuildTarget.StandaloneWindows64);
        }
    }
}
