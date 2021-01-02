using Facade;
using Modules;
using Modules.Scenes.Cameras;
using UI;
using UnityEngine;

public class Main : MonoBehaviour {
    private ModuleMgr _moduleMgr;
    private UIMgr _uiMgr;

    private void Awake() {
        DontDestroyOnLoad(this);

        _moduleMgr = new ModuleMgr();
        _moduleMgr.AddModule(new SceneModule());
        _moduleMgr.AddModule(new HealthModule());
        _moduleMgr.AddModule(new ScoreModule());
        _moduleMgr.Init();

        _uiMgr = new UIMgr();
        _uiMgr.Init();

        SceneFacade.AddLoadedCallback?.Invoke("Start", () => UIFacade.ShowUI?.Invoke(UIDef.START));
        SceneFacade.AddUnloadedCallback?.Invoke("Start", () => UIFacade.HideUI?.Invoke(UIDef.START));

        SceneFacade.AddLoadedCallback?.Invoke("Loading", () => UIFacade.ShowUI?.Invoke(UIDef.LOADING));
        SceneFacade.AddUnloadedCallback?.Invoke("Loading", () => UIFacade.HideUI?.Invoke(UIDef.LOADING));

        SceneFacade.AddLoadedCallback?.Invoke("Level_01", () => {
            UIFacade.ShowUI?.Invoke(UIDef.MAIN);
            gameObject.AddComponent<CameraController>();
        });
        SceneFacade.AddUnloadedCallback?.Invoke("Level_01", () => {
            UIFacade.HideUI?.Invoke(UIDef.MAIN);
            DestroyImmediate(GetComponent<CameraController>());
        });

        SceneFacade.LoadScene?.Invoke("Start");
    }

    private void OnDestroy() {
        _moduleMgr.Dispose();
        _uiMgr.Dispose();
    }

    private void Update() {
        _moduleMgr.Update();
        _uiMgr.Update();
    }
}