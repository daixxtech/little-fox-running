using Facade;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Main {
    public class MainUIHandler : MonoBehaviour {
        [SerializeField] private Image[] _heartImgs;
        [SerializeField] private Text _scoreTxt;

        private void Awake() {
            Transform healthRootTrans = transform.Find("Health");
            int count = healthRootTrans.childCount;
            _heartImgs = new Image[count];
            for (int i = 0; i < count; i++) {
                _heartImgs[i] = transform.GetChild(i).GetComponent<Image>();
            }
            _scoreTxt = transform.Find("ScoreTxt").GetComponent<Text>();

            HealthFacade.OnHealthUpdated += OnHealthUpdated;
            ScoreFacade.OnScoreUpdated += OnScoreUpdated;
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
            _scoreTxt.text = value.ToString();
        }
    }
}