using System;
using System.Collections.Generic;
using Facade;
using Modules.Base;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modules {
    public class SceneModule : IModule {
        private Dictionary<string, Action> _loadedCallbackDict;

        public bool NeedUpdate { get; } = false;

        public void Init() {
            _loadedCallbackDict = new Dictionary<string, Action>();

            SceneFacade.AddSceneLoadedCallback += AddSceneLoadedCallback;
            SceneFacade.RemoveSceneLoadedCallback += RemoveSceneLoadedCallback;
            SceneFacade.LoadScene += LoadScene;
            SceneFacade.LoadSceneAsync += LoadSceneAsync;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void Dispose() {
            SceneFacade.AddSceneLoadedCallback -= AddSceneLoadedCallback;
            SceneFacade.RemoveSceneLoadedCallback -= RemoveSceneLoadedCallback;
            SceneFacade.LoadScene -= LoadScene;
            SceneFacade.LoadSceneAsync -= LoadSceneAsync;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void Update() { }

        private void AddSceneLoadedCallback(string sceneName, Action action) {
            _loadedCallbackDict[sceneName] = action;
        }

        private void RemoveSceneLoadedCallback(string sceneName, Action action) {
            _loadedCallbackDict.Remove(sceneName);
        }

        private void LoadScene(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }

        private void LoadSceneAsync(string sceneName) {
            SceneManager.LoadScene("Scenes/Loading");
            UIFacade.ShowUIByParam?.Invoke(UIDef.UI_LOADING, sceneName);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            _loadedCallbackDict.TryGetValue(scene.name, out var callback);
            callback?.Invoke();
        }
    }
}