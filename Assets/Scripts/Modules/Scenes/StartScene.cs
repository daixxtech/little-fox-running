using Facade;
using UI;
using UnityEngine;

namespace Modules.Scenes {
    public class StartScene : MonoBehaviour {
        private void Awake() {
            UIFacade.HideUIAll?.Invoke();
            UIFacade.ShowUI?.Invoke(UIDef.START);
        }
    }
}
