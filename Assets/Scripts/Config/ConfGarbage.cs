using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Config {
    public enum EGarbage {
        /// <summary> 可回收垃圾 </summary>
        Recyclable,
        /// <summary> 厨余垃圾 </summary>
        Kitchen,
        /// <summary> 其他垃圾 </summary>
        Residual,
        /// <summary> 有害垃圾 </summary>
        Harmful,
    }

    [CreateAssetMenu(fileName = "Garbage", menuName = "GameConfig/Garbage")]
    public class ConfGarbage : ScriptableObject {
        private static readonly Dictionary<int, ConfGarbage> DICT;
        private static ConfGarbage[] CacheArray;
        [SerializeField] private int _id;
        [SerializeField] private EGarbage _category;
        [SerializeField] private string _name;
        [SerializeField] private string _displayName;
        [SerializeField] [Multiline(4)] private string _description;
        [SerializeField] private int _score;

        public int ID => _id;
        public EGarbage Category => _category;
        public string Name => _name;
        public string DisplayName => _displayName;
        public string Description => _description;
        public int Score => _score;

        static ConfGarbage() {
            string[] guids = AssetDatabase.FindAssets("t: ConfGarbage");
            int count = guids.Length;
            DICT = new Dictionary<int, ConfGarbage>(count);
            for (int i = 0; i < count; i++) {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                ConfGarbage conf = AssetDatabase.LoadAssetAtPath<ConfGarbage>(path);
                DICT.Add(conf._id, conf);
            }
        }

        public static ConfGarbage[] Array {
            get {
                if (CacheArray != null) {
                    return CacheArray;
                }
                CacheArray = new ConfGarbage[DICT.Count];
                int index = -1;
                foreach (var pair in DICT) {
                    CacheArray[++index] = pair.Value;
                }
                return CacheArray;
            }
        }

        public ConfGarbage Get(int id) {
            DICT.TryGetValue(id, out var conf);
            return conf;
        }
    }
}