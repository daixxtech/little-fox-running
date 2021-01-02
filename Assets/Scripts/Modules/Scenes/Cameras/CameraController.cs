using Facade;
using UI;
using UnityEngine;

namespace Modules.Scenes.Cameras {
    public class CameraController : MonoBehaviour {
        private void Update() {
            if (Input.GetButton("Cancel")) {
                UIFacade.ShowUI(UIDef.PAUSE);
            }
        }

        private void LateUpdate() {
            Vector3 playerPos = PlayerFacade.GetPosition?.Invoke() ?? Vector3.zero;
            Vector3 cameraPos = transform.position;
            transform.position = new Vector3(playerPos.x, playerPos.y, cameraPos.z);
        }
    }
}