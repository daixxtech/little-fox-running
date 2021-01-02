using Facade;
using Modules;
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

        SceneFacade.AddSceneLoadedCallback?.Invoke("Start", () => UIFacade.ShowUI?.Invoke(UIDef.UI_START));
        SceneFacade.AddSceneLoadedCallback?.Invoke("Level_01", () => UIFacade.ShowUI?.Invoke(UIDef.UI_MAIN));
        SceneFacade.LoadScene?.Invoke("Scenes/Start");
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