using System;

namespace Facade {
    public static class UIFacade {
        public static Func<object> GetUIParam;
        public static Action<string> ShowUI;
        public static Action<string, object> ShowUIByParam;
        public static Action<string> HideUI;
    }
}