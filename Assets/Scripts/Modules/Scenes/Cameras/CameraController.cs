using Facade;
using UI;
using UnityEngine;

namespace Modules.Scenes {
    public class CameraController : MonoBehaviour {
        private void Update() {
            if (Input.GetButton("Cancel")) {
                UIFacade.ShowUI?.Invoke(UIDef.PAUSE);
            }
        }

        private void LateUpdate() {
            Vector3 playerPos = PlayerFacade.GetPosition?.Invoke() ?? Vector3.zero;
            Vector3 cameraPos = transform.position;
            transform.position = new Vector3(playerPos.x, Mathf.Clamp(playerPos.y, -2.99F, 4.49F), cameraPos.z);
        }

        private void OnDestroy() {
            transform.position = new Vector3(0, 0, -10);
        }
    }
}
