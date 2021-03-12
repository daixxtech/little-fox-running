/* Auto generated code */

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace Config {
    /// <summary> Generate From Garbage.xlsx </summary>
    public enum EGarbageDef {
        /// <summary> 厨余垃圾 </summary>
        Kitchen = 1,
        /// <summary> 可回收垃圾 </summary>
        Recyclable = 2,
        /// <summary> 有害垃圾 </summary>
        Harmful = 3,
        /// <summary> 其他垃圾 </summary>
        Residual = 4,
    }

    /// <summary> Generate From Garbage.xlsx </summary>
    public class ConfGarbageDef {
        /// <summary> ID </summary>
        public readonly int id;
        /// <summary> 名称 </summary>
        public readonly string name;

        public ConfGarbageDef(int id, string name) {
            this.id = id;
            this.name = name;
        }

        private static Dictionary<int, ConfGarbageDef> _Dict;
        private static ConfGarbageDef[] _Array;

        public static async Task<ConfGarbageDef> Get(int id) {
            return (_Dict ?? (_Dict = await ConfUtil.LoadFromJSON<ConfGarbageDef>())).TryGetValue(id, out var conf) ? conf : null;
        }

        public static async Task<ConfGarbageDef[]> GetArray() {
            return _Array ?? (_Array = (_Dict ?? (_Dict = await ConfUtil.LoadFromJSON<ConfGarbageDef>())).Values.ToArray());
        }
    }
}

/* End of auto generated code */
