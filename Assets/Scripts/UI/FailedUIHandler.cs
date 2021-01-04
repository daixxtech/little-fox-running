using Facade;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI {
    public class FailedUIHandler : MonoBehaviour {
        private void Awake() {
            transform.Find("Operation/RestartBtn").GetComponent<Button>().onClick.AddListener(OnRestartBtnClicked);
            transform.Find("Operation/BackBtn").GetComponent<Button>().onClick.AddListener(OnBackBtnClicked);
        }

        private void OnEnable() {
            Time.timeScale = 0;
        }

        private void OnDisable() {
            Time.timeScale = 1;
        }

        private void OnRestartBtnClicked() {
            UIFacade.HideUI(UIDef.FAILED);
            SceneFacade.LoadSceneAsync?.Invoke(SceneManager.GetActiveScene().name);
        }

        private void OnBackBtnClicked() {
            UIFacade.HideUI(UIDef.FAILED);
            SceneFacade.LoadSceneAsync?.Invoke("Start");
        }
    }
}
