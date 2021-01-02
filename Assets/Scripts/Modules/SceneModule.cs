using System;
using System.Collections.Generic;
using Facade;
using Modules.Base;
using UI;
using UnityEngine.SceneManagement;

namespace Modules {
    public class SceneModule : IModule {
        private Dictionary<string, Action> _loadedCallbackDict;
        private Dictionary<string, Action> _unloadedCallbackDict;

        public bool NeedUpdate { get; } = false;

        public void Init() {
            _loadedCallbackDict = new Dictionary<string, Action>();
            _unloadedCallbackDict = new Dictionary<string, Action>();

            SceneFacade.AddLoadedCallback += AddLoadedCallback;
            SceneFacade.RemoveLoadedCallback += RemoveLoadedCallback;
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneFacade.AddUnloadedCallback += AddUnloadedCallback;
            SceneFacade.RemoveUnloadedCallback += RemoveUnloadedCallback;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            SceneFacade.LoadScene += LoadScene;
            SceneFacade.LoadSceneAsync += LoadSceneAsync;
        }

        public void Dispose() {
            SceneFacade.AddLoadedCallback -= AddLoadedCallback;
            SceneFacade.RemoveLoadedCallback -= RemoveLoadedCallback;
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneFacade.AddUnloadedCallback -= AddUnloadedCallback;
            SceneFacade.RemoveUnloadedCallback -= RemoveUnloadedCallback;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
            SceneFacade.LoadScene -= LoadScene;
            SceneFacade.LoadSceneAsync -= LoadSceneAsync;
        }

        public void Update() { }

        private void AddLoadedCallback(string sceneName, Action action) {
            _loadedCallbackDict[sceneName] = action;
        }

        private void RemoveLoadedCallback(string sceneName, Action action) {
            _loadedCallbackDict.Remove(sceneName);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            _loadedCallbackDict.TryGetValue(scene.name, out var callback);
            callback?.Invoke();
        }

        private void AddUnloadedCallback(string sceneName, Action action) {
            _unloadedCallbackDict[sceneName] = action;
        }

        private void RemoveUnloadedCallback(string sceneName, Action action) {
            _unloadedCallbackDict.Remove(sceneName);
        }

        private void OnSceneUnloaded(Scene scene) {
            _unloadedCallbackDict.TryGetValue(scene.name, out var callback);
            callback?.Invoke();
        }

        private void LoadScene(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }

        private void LoadSceneAsync(string sceneName) {
            SceneManager.LoadScene("Loading");
            UIFacade.ShowUIByParam?.Invoke(UIDef.LOADING, sceneName);
        }
    }
}