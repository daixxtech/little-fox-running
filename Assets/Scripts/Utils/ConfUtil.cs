using Config;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Utils {
    public static class ConfUtil {
        public static async Task<Dictionary<int, T>> LoadFromJSON<T>() {
            TextAsset textAsset = await Addressables.LoadAssetAsync<TextAsset>(typeof(T).Name).Task;
            return textAsset ? JsonConvert.DeserializeObject<Dictionary<int, T>>(textAsset.text) : new Dictionary<int, T>();
        }
    }
}
