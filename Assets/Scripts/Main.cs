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
        _moduleMgr.AddModule(new HealthModule(5));
        _moduleMgr.AddModule(new ScoreModule());
        _moduleMgr.Init();

        _uiMgr = new UIMgr();
        _uiMgr.Init();

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