﻿using System.Collections.Generic;
using Facade;
using Modules.Base;
using UnityEngine;

namespace Modules {
    public sealed class UIModule : IModule {
        private Transform _uiRoot;
        private Dictionary<string, GameObject> _uiDict;
        private object _uiParam;

        public bool NeedUpdate { get; } = false;

        public void Init() {
            _uiRoot = GameObject.Find("UIRoot").transform;
            Object.DontDestroyOnLoad(_uiRoot);
            _uiDict = new Dictionary<string, GameObject>();

            UIFacade.GetUIParam += GetUIParam;
            UIFacade.ShowUI += ShowUI;
            UIFacade.ShowUIByParam += ShowUIByParam;
            UIFacade.HideUI += HideUI;
            UIFacade.HideUIAll += HideUIAll;
        }

        public void Dispose() {
            UIFacade.GetUIParam -= GetUIParam;
            UIFacade.ShowUI -= ShowUI;
            UIFacade.ShowUIByParam -= ShowUIByParam;
            UIFacade.HideUI -= HideUI;
            UIFacade.HideUIAll -= HideUIAll;
        }

        public void Update() { }

        private object GetUIParam() {
            object param = _uiParam;
            _uiParam = null;
            return param;
        }

        private void ShowUI(string uiName) {
            if (_uiDict.TryGetValue(uiName, out GameObject ui)) {
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

        private void ShowUIByParam(string uiName, object param) {
            _uiParam = param;
            ShowUI(uiName);
        }

        private void HideUI(string uiName) {
            if (_uiDict.TryGetValue(uiName, out GameObject ui)) {
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

        private void HideUIAll() {
            foreach (var pair in _uiDict) {
                pair.Value.SetActive(false);
            }
        }
    }
}
