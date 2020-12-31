using System;

namespace Facade {
    public static class UIFacade {
        public static Action<string> ShowUI;
        public static Action<string> HideUI;
    }
}