using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Config {
    [CreateAssetMenu(fileName = "GarbageBin", menuName = "GameConfig/GarbageBin")]
    public class ConfGarbageBin : ScriptableObject, IConfig {
        private static Dictionary<int, ConfGarbageBin> Dict;
        private static ConfGarbageBin[] CacheArray;
        private static Dictionary<int, ConfGarbageBin> CategoryDict;
        [SerializeField] private int _id;
        [SerializeField] private EGarbage _category;
        [SerializeField] private Sprite _icon;

        public int ID => _id;
        public EGarbage Category => _category;
        public Sprite Icon => _icon;

        public static ConfGarbageBin[] Array {
            get {
                if (CacheArray != null) {
                    return CacheArray;
                }
                if (Dict == null) {
                    Dict = ConfUtil.LoadConf<ConfGarbageBin>();
                }
                CacheArray = new ConfGarbageBin[Dict.Count];
                int index = -1;
                foreach (var pair in Dict) {
                    CacheArray[++index] = pair.Value;
                }
                return CacheArray;
            }
        }

        public static ConfGarbageBin Get(int id) {
            if (Dict == null) {
                Dict = ConfUtil.LoadConf<ConfGarbageBin>();
            }
            Dict.TryGetValue(id, out var conf);
            return conf;
        }

        public static ConfGarbageBin GetByCategory(EGarbage category) {
            if (CategoryDict == null) {
                if (Dict == null) {
                    Dict = ConfUtil.LoadConf<ConfGarbageBin>();
                }
                CategoryDict = new Dictionary<int, ConfGarbageBin>(Dict.Count);
                foreach (var pair in Dict) {
                    CategoryDict.Add((int) pair.Value._category, pair.Value);
                }
            }
            CategoryDict.TryGetValue((int) category, out var conf);
            return conf;
        }
    }
}
