using Config;
using Facade;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class MainUIHandler : MonoBehaviour {
        [SerializeField] private Image[] _heartImgs;
        [SerializeField] private Sprite[] _heartSprites;
        [SerializeField] private Image _garbageBinImg;
        [SerializeField] private Text _scoreTxt;

        private void Awake() {
            Transform heartCtnr = transform.Find("Health/HeartContainer");
            GameObject template = heartCtnr.Find("Template").gameObject;
            int count = HealthFacade.GetHealthMax?.Invoke() ?? 0;
            _heartImgs = new Image[count];
            for (int i = 0; i < count; i++) {
                _heartImgs[i] = Instantiate(template, heartCtnr).GetComponent<Image>();
            }
            template.SetActive(false);
            _heartSprites = new Sprite[2];
            AssetBundle assetBundle = AssetBundleFacade.LoadAssetBundle?.Invoke("texture.bundle");
            if (!(assetBundle is null)) {
                _heartSprites[0] = assetBundle.LoadAsset<Sprite>("心.png");
                _heartSprites[1] = assetBundle.LoadAsset<Sprite>("空心.png");
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
            int i = 0;
            while (i < value) {
                _heartImgs[i++].sprite = _heartSprites[0];
            }
            while (i < count) {
                _heartImgs[i++].sprite = _heartSprites[1];
            }
        }

        private void OnScoreUpdated(int value) {
            _scoreTxt.text = value.ToString("D8");
        }
    }
}
