using System.Collections.Generic;
using Facade;
using Modules.Base;
using UnityEngine;

namespace Modules {
    public class AssetBundleModule : IModule {
        private readonly Dictionary<string, AssetBundle> _assetBundleDict;

        public bool NeedUpdate { get; } = false;

        public AssetBundleModule() {
            _assetBundleDict = new Dictionary<string, AssetBundle>();
        }

        public void Init() {
            AssetBundleFacade.LoadAssetBundle += LoadAssetBundle;
        }

        public void Dispose() {
            AssetBundleFacade.LoadAssetBundle -= LoadAssetBundle;

            foreach (var pair in _assetBundleDict) {
                pair.Value.Unload(true);
            }
        }

        public void Update() { }

        private AssetBundle LoadAssetBundle(string name) {
            if (_assetBundleDict.TryGetValue(name, out var assetBundle)) {
                return assetBundle;
            }
            assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + name);
            _assetBundleDict.Add(name, assetBundle);
            return assetBundle;
        }
    }
}
