using Config;
using Facade;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class MainUIHandler : MonoBehaviour {
        [SerializeField] private Image[] _heartImgs;
        [SerializeField] private Image _garbageBinImg;
        [SerializeField] private Text _scoreTxt;

        private void Awake() {
            Transform heartCtnr = transform.Find("Health/HeartContainer");
            int count = heartCtnr.childCount;
            _heartImgs = new Image[count];
            for (int i = 0; i < count; i++) {
                _heartImgs[i] = heartCtnr.GetChild(i).GetComponent<Image>();
            }
            _garbageBinImg = transform.Find("GarbageBin/GarbageBinImg").GetComponent<Image>();
            _scoreTxt = transform.Find("Scoreboard/ScoreTxt").GetComponent<Text>();
        }

        private void OnEnable() {
            PlayerFacade.OnGarbageBinChanged += OnGarbageBinChanged;
            HealthFacade.OnHealthUpdated += OnHealthUpdated;
            ScoreFacade.OnScoreUpdated += OnScoreUpdated;

            OnGarbageBinChanged(PlayerFacade.GetGarbageBin?.Invoke() ?? EGarbage.Kitchen);
            OnHealthUpdated(HealthFacade.GetHealth?.Invoke() ?? 0);
            OnScoreUpdated(ScoreFacade.GetScore?.Invoke() ?? 0);
        }

        private void OnDisable() {
            PlayerFacade.OnGarbageBinChanged -= OnGarbageBinChanged;
            HealthFacade.OnHealthUpdated -= OnHealthUpdated;
            ScoreFacade.OnScoreUpdated -= OnScoreUpdated;
        }

        private void OnGarbageBinChanged(EGarbage category) {
            _garbageBinImg.sprite = ConfGarbageBin.GetByCategory(category)?.Icon;
        }

        private void OnHealthUpdated(int value) {
            int count = _heartImgs.Length;
            for (int i = 0; i < value; i++) {
                _heartImgs[i].color = Color.red;
            }
            for (int i = 0; i < count; i++) {
                _heartImgs[i].color = Color.grey;
            }
        }

        private void OnScoreUpdated(int value) {
            _scoreTxt.text = value.ToString("D8");
        }
    }
}