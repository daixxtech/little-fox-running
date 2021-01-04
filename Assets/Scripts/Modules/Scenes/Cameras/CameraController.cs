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
            float xClamp = Mathf.Clamp(playerPos.x, -68.88F, 68.88F);
            float yClamp = Mathf.Clamp(playerPos.y, -2.49F, 4.49F);
            transform.position = new Vector3(xClamp, yClamp, cameraPos.z);
        }

        private void OnDestroy() {
            transform.position = new Vector3(0, 0, -10);
        }
    }
}
