using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Config {
    public enum EGarbage {
        /// <summary> 厨余垃圾 </summary>
        Kitchen,
        /// <summary> 可回收垃圾 </summary>
        Recyclable,
        /// <summary> 有害垃圾 </summary>
        Harmful,
        /// <summary> 其他垃圾 </summary>
        Residual,
    }

    [CreateAssetMenu(fileName = "Garbage", menuName = "GameConfig/Garbage")]
    public class ConfGarbage : ScriptableObject, IConfig {
        private static Dictionary<int, ConfGarbage> Dict;
        private static ConfGarbage[] CacheArray;
        [SerializeField] private int _id;
        [SerializeField] private EGarbage _category;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] [Multiline(4)] private string _description;
        [SerializeField] private int _score;

        public int ID => _id;
        public EGarbage Category => _category;
        public string Name => _name;
        public Sprite Icon => _icon;
        public string Description => _description;
        public int Score => _score;

        public static ConfGarbage[] Array {
            get {
                if (CacheArray != null) {
                    return CacheArray;
                }
                if (Dict == null) {
                    Dict = ConfUtil.LoadConf<ConfGarbage>();
                }
                CacheArray = new ConfGarbage[Dict.Count];
                int index = -1;
                foreach (var pair in Dict) {
                    CacheArray[++index] = pair.Value;
                }
                return CacheArray;
            }
        }

        public ConfGarbage Get(int id) {
            if (Dict == null) {
                Dict = ConfUtil.LoadConf<ConfGarbage>();
            }
            Dict.TryGetValue(id, out var conf);
            return conf;
        }
    }
}