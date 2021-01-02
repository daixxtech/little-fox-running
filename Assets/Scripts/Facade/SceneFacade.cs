using System;

namespace Facade {
    public static class SceneFacade {
        public static Action<string, Action> AddSceneLoadedCallback;
        public static Action<string, Action> RemoveSceneLoadedCallback;
        public static Action<string> LoadScene;
        public static Action<string> LoadSceneAsync;
    }
}