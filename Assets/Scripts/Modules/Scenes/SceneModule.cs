using System;
using Facade;
using Modules.Base;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modules.Scenes {
    public class SceneModule : IModule {
        public bool NeedUpdate { get; } = false;

        public void Init() {
            SceneFacade.LoadSceneAsync += LoadSceneAsync;
        }

        public void Dispose() {
            SceneFacade.LoadSceneAsync -= LoadSceneAsync;
        }

        public void Update() { }

        private void LoadSceneAsync(string sceneName, Action<AsyncOperation> completedCallback) {
            SceneManager.LoadScene("Scenes/Loading");
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.completed += completedCallback;
            UIFacade.ShowUIByParam?.Invoke(UIDef.UI_LOADING, operation);
        }
    }
}