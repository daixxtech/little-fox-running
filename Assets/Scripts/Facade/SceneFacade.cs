using System;
using UnityEngine;

namespace Facade {
    public static class SceneFacade {
        public static Action<string, Action<AsyncOperation>> LoadSceneAsync;
    }
}