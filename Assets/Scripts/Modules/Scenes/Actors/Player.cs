using Config;
using UnityEngine;

namespace Modules.Scenes {
    public class Player : MonoBehaviour {
        public EGarbage GarbageBin { get; private set; }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.H)) {
                GarbageBin = EGarbage.Kitchen;
            }
            if (Input.GetKeyDown(KeyCode.J)) {
                GarbageBin = EGarbage.Recyclable;
            }
            if (Input.GetKeyDown(KeyCode.K)) {
                GarbageBin = EGarbage.Harmful;
            }
            if (Input.GetKeyDown(KeyCode.L)) {
                GarbageBin = EGarbage.Residual;
            }
        }
    }
}