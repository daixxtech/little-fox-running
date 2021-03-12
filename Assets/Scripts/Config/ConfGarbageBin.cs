/* Auto generated code */

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace Config {
    /// <summary> Generate From Garbage.xlsx </summary>
    public class ConfGarbageBin {
        /// <summary> ID </summary>
        public readonly int id;
        /// <summary> 名称 </summary>
        public readonly string name;
        /// <summary> 类别 </summary>
        public readonly int category;
        /// <summary> 图标 </summary>
        public readonly string icon;

        public ConfGarbageBin(int id, string name, int category, string icon) {
            this.id = id;
            this.name = name;
            this.category = category;
            this.icon = icon;
        }

        private static Dictionary<int, ConfGarbageBin> _Dict;
        private static ConfGarbageBin[] _Array;

        public static async Task<ConfGarbageBin> Get(int id) {
            return (_Dict ?? (_Dict = await ConfUtil.LoadFromJSON<ConfGarbageBin>())).TryGetValue(id, out var conf) ? conf : null;
        }

        public static async Task<ConfGarbageBin[]> GetArray() {
            return _Array ?? (_Array = (_Dict ?? (_Dict = await ConfUtil.LoadFromJSON<ConfGarbageBin>())).Values.ToArray());
        }
    }
}

/* End of auto generated code */
