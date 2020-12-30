using System.Collections.Generic;
using Modules;
using UnityEngine;

public sealed class UIMgr : IModule {
    private Transform _uiRoot;
    private Dictionary<string, GameObject> _uiDict;

    public bool NeedUpdate { get; } = false;

    public void Awake() {
        _uiRoot = GameObject.Find("UIRoot").transform;
        _uiDict = new Dictionary<string, GameObject>();
    }

    public void Dispose() {

    }

    public void Update() {
    }

    public void ShowUI(string uiName) {
        if (_uiDict.TryGetValue(uiName, out var ui)) {
            ui.gameObject.SetActive(true);
        } else {
            ui = _uiRoot.Find(uiName)?.gameObject;
            if (ui == null) {
                return;
            }
            _uiDict.Add(uiName, ui);
            ui.SetActive(true);
        }
    }

    public void HideUI(string uiName) {
        if (_uiDict.TryGetValue(uiName, out var ui)) {
            ui.gameObject.SetActive(false);
        } else {
            ui = _uiRoot.Find(uiName)?.gameObject;
            if (ui == null) {
                return;
            }
            _uiDict.Add(uiName, ui);
            ui.SetActive(false);
        }
    }
}
