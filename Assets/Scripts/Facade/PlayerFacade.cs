using System;
using Config;

namespace Facade {
    public static class PlayerFacade {
        public static Func<EGarbage> GetGarbageBin;
        public static Action<EGarbage> OnGarbageBinChanged;
    }
}