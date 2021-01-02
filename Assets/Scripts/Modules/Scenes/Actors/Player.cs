using Config;
using Facade;
using UnityEngine;

namespace Modules.Scenes {
    public class Player : MonoBehaviour {
        [SerializeField] private EGarbage _garbageBin;

        private void Awake() {
            PlayerFacade.GetGarbageBin += GetGarbageBin;
        }

        private void OnDestroy() {
            PlayerFacade.GetGarbageBin -= GetGarbageBin;
        }

        private void Update() {
            EGarbage pre = _garbageBin;
            if (Input.GetKeyDown(KeyCode.H)) {
                _garbageBin = EGarbage.Kitchen;
            }
            if (Input.GetKeyDown(KeyCode.J)) {
                _garbageBin = EGarbage.Recyclable;
            }
            if (Input.GetKeyDown(KeyCode.K)) {
                _garbageBin = EGarbage.Harmful;
            }
            if (Input.GetKeyDown(KeyCode.L)) {
                _garbageBin = EGarbage.Residual;
            }
            if (_garbageBin != pre) {
                PlayerFacade.OnGarbageBinChanged?.Invoke(_garbageBin);
            }
        }

        private EGarbage GetGarbageBin() {
            return _garbageBin;
        }
    }
}