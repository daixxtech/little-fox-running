using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Config {
    [CreateAssetMenu(fileName = "LoadingTips", menuName = "GameConfig/LoadingTips")]
    public class ConfLoadingTips : ScriptableObject, IConfig {
        private static Dictionary<int, ConfLoadingTips> Dict;
        private static ConfLoadingTips[] CacheArray;
        [SerializeField] private int _id;
        [SerializeField] private string _content;

        public int ID => _id;
        public string Content => _content;

        public static ConfLoadingTips[] Array {
            get {
                if (CacheArray != null) {
                    return CacheArray;
                }
                if (Dict == null) {
                    Dict = ConfUtil.LoadConf<ConfLoadingTips>();
                }
                CacheArray = new ConfLoadingTips[Dict.Count];
                int index = -1;
                foreach (var pair in Dict) {
                    CacheArray[++index] = pair.Value;
                }
                return CacheArray;
            }
        }

        public static ConfLoadingTips Get(int id) {
            if (Dict == null) {
                Dict = ConfUtil.LoadConf<ConfLoadingTips>();
            }
            Dict.TryGetValue(id, out var conf);
            return conf;
        }
    }
}
