using Modules;
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