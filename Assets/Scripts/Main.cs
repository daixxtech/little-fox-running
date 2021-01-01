using UnityEngine;

public class Main : MonoBehaviour {
    private ModuleMgr _moduleMgr;
    private UIMgr _uiMgr;

    private void Awake() {
        _moduleMgr = new ModuleMgr();
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