using Facade;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class StartUIHandler : MonoBehaviour {
        [SerializeField] private Transform _introducePanel;

        private void Awake() {
            _introducePanel = transform.Find("Introduce");
            transform.Find("Introduce/Title/CloseBtn").GetComponent<Button>().onClick.AddListener(OnIntroduceCloseBtnClicked);
            transform.Find("Introduce/Operation/ConfirmBtn").GetComponent<Button>().onClick.AddListener(OnIntroduceCloseBtnClicked);

            transform.Find("Operation/StartBtn").GetComponent<Button>().onClick.AddListener(OnStartBtnClicked);
            transform.Find("Operation/IntroduceBtn").GetComponent<Button>().onClick.AddListener(OnIntroduceBtnClicked);
            transform.Find("Operation/QuitBtn").GetComponent<Button>().onClick.AddListener(OnQuitBtnClicked);
        }

        private void OnStartBtnClicked() {
            UIFacade.HideUI?.Invoke(UIDef.UI_START);
            SceneFacade.LoadSceneAsync?.Invoke("Scenes/Level_01", (operation => { UIFacade.ShowUI?.Invoke(UIDef.UI_MAIN); }));
        }

        private void OnIntroduceBtnClicked() {
            _introducePanel.gameObject.SetActive(true);
        }

        private void OnQuitBtnClicked() {
            Application.Quit();
        }

        private void OnIntroduceCloseBtnClicked() {
            _introducePanel.gameObject.SetActive(false);
        }
    }
}