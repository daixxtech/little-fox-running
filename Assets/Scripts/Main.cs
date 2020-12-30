using UnityEngine;

public class Main : MonoBehaviour {
    private ModuleMgr _moduleMgr;

    private void Awake() {
        _moduleMgr = new ModuleMgr();
        _moduleMgr.Awake();
    }

    private void OnDestroy() {
        _moduleMgr.Dispose();
    }

    private void Update() {
        _moduleMgr.Update();
    }
}
