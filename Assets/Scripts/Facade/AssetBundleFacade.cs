using System;
using UnityEngine;

namespace Facade {
    public static class AssetBundleFacade {
        public static Func<string, AssetBundle> LoadAssetBundle;
    }
}
