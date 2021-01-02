using Facade;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class PauseUIHandler : MonoBehaviour {
        private void Awake() {
            transform.Find("Title/CloseBtn").GetComponent<Button>().onClick.AddListener(OnCloseBtnClicked);
            transform.Find("Operation/ContinueBtn").GetComponent<Button>().onClick.AddListener(OnCloseBtnClicked);
            transform.Find("Operation/BackBtn").GetComponent<Button>().onClick.AddListener(OnBackBtnClicked);
        }

        private void OnEnable() {
            Time.timeScale = 0;
        }

        private void OnDisable() {
            Time.timeScale = 1;
        }

        private void OnCloseBtnClicked() {
            UIFacade.HideUI(UIDef.PAUSE);
        }

        private void OnBackBtnClicked() {
            UIFacade.HideUI(UIDef.PAUSE);
            SceneFacade.LoadSceneAsync?.Invoke("Start");
        }
    }
}