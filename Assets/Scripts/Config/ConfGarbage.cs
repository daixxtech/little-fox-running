/* Auto generated code */

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace Config {
    /// <summary> Generate From Garbage.xlsx </summary>
    public class ConfGarbage {
        /// <summary> ID </summary>
        public readonly int id;
        /// <summary> 名称 </summary>
        public readonly string name;
        /// <summary> 类别 </summary>
        public readonly int category;
        /// <summary> 图标 </summary>
        public readonly string icon;
        /// <summary> 描述 </summary>
        public readonly string description;
        /// <summary> 分数 </summary>
        public readonly int score;

        public ConfGarbage(int id, string name, int category, string icon, string description, int score) {
            this.id = id;
            this.name = name;
            this.category = category;
            this.icon = icon;
            this.description = description;
            this.score = score;
        }

        private static Dictionary<int, ConfGarbage> _Dict;
        private static ConfGarbage[] _Array;

        public static async Task<ConfGarbage> Get(int id) {
            return (_Dict ?? (_Dict = await ConfUtil.LoadFromJSON<ConfGarbage>())).TryGetValue(id, out var conf) ? conf : null;
        }

        public static async Task<ConfGarbage[]> GetArray() {
            return _Array ?? (_Array = (_Dict ?? (_Dict = await ConfUtil.LoadFromJSON<ConfGarbage>())).Values.ToArray());
        }
    }
}

/* End of auto generated code */
