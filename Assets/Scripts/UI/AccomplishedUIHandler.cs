using Facade;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI {
    public class AccomplishedUIHandler : MonoBehaviour {
        private void Awake() {
            int score = ScoreFacade.GetScore?.Invoke() ?? 0;
            transform.Find("Score/ScoreTxt").GetComponent<Text>().text = $"{score.ToString(),8}";

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
            UIFacade.HideUI(UIDef.ACCOMPLISHED);
            SceneFacade.LoadSceneAsync?.Invoke(SceneManager.GetActiveScene().name);
        }

        private void OnBackBtnClicked() {
            UIFacade.HideUI(UIDef.ACCOMPLISHED);
            SceneFacade.LoadSceneAsync?.Invoke("Start");
        }
    }
}