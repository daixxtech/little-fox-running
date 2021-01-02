using Facade;
using Modules.Base;
using UI;
using UnityEngine.SceneManagement;

namespace Modules {
    public class SceneModule : IModule {
        public bool NeedUpdate { get; } = false;

        public void Init() {
            SceneFacade.LoadSceneAsync += LoadSceneAsync;
        }

        public void Dispose() {
            SceneFacade.LoadSceneAsync -= LoadSceneAsync;
        }

        public void Update() { }

        private void LoadSceneAsync(string sceneName) {
            SceneManager.LoadScene("Scenes/Loading");
            UIFacade.ShowUIByParam?.Invoke(UIDef.UI_LOADING, sceneName);
        }
    }
}