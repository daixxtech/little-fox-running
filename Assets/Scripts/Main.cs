using Facade;
using Modules;
using UnityEngine;

public class Main : MonoBehaviour {
    private ModuleMgr _moduleMgr;

    private void Awake() {
        DontDestroyOnLoad(this);

        _moduleMgr = new ModuleMgr();
        _moduleMgr.AddModule(new UIModule());
        _moduleMgr.AddModule(new SceneModule());
        _moduleMgr.AddModule(new HealthModule(5));
        _moduleMgr.AddModule(new ScoreModule());
        _moduleMgr.Init();
        SceneFacade.LoadScene?.Invoke("Start");
    }

    private void OnDestroy() {
        _moduleMgr.Dispose();
    }

    private void Update() {
        _moduleMgr.Update();
    }
}
