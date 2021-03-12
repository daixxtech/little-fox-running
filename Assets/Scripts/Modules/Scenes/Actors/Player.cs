using Config;
using Facade;
using UnityEngine;

namespace Modules.Scenes {
    public class Player : MonoBehaviour {
        [SerializeField] private EGarbageDef _garbageBin = EGarbageDef.Kitchen;

        private void Awake() {
            PlayerFacade.GetPosition += GetPosition;
            PlayerFacade.GetGarbageBin += GetGarbageBin;
        }

        private void OnDestroy() {
            PlayerFacade.GetPosition -= GetPosition;
            PlayerFacade.GetGarbageBin -= GetGarbageBin;
        }

        private void Update() {
            EGarbageDef pre = _garbageBin;
            if (Input.GetKeyDown(KeyCode.H)) {
                _garbageBin = EGarbageDef.Kitchen;
            }
            if (Input.GetKeyDown(KeyCode.J)) {
                _garbageBin = EGarbageDef.Recyclable;
            }
            if (Input.GetKeyDown(KeyCode.K)) {
                _garbageBin = EGarbageDef.Harmful;
            }
            if (Input.GetKeyDown(KeyCode.L)) {
                _garbageBin = EGarbageDef.Residual;
            }
            if (_garbageBin != pre) {
                PlayerFacade.OnGarbageBinChanged?.Invoke(_garbageBin);
            }
        }

        private Vector3 GetPosition() {
            return transform.position;
        }

        private EGarbageDef GetGarbageBin() {
            return _garbageBin;
        }
    }
}
