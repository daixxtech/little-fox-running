using System.Collections.Generic;
using Config;
using Facade;
using UnityEngine;

namespace Utils {
    public static class ConfUtil {
        public static Dictionary<int, T> LoadConf<T>() where T : Object, IConfig {
            AssetBundle configBundle = AssetBundleFacade.LoadAssetBundle?.Invoke("config.bundle");
            if (configBundle is null) {
                return null;
            }
            T[] assets = configBundle.LoadAllAssets<T>();
            int count = assets.Length;
            var dict = new Dictionary<int, T>(count);
            for (int i = 0; i < count; i++) {
                T conf = assets[i];
                dict.Add(conf.ID, conf);
            }
            return dict;
        }
    }
}
