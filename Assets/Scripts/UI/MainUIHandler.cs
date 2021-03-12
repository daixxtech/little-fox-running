using Config;
using Facade;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace UI {
    public class MainUIHandler : MonoBehaviour {
        [SerializeField] private Image[] _heartImgs;
        [SerializeField] private Sprite[] _heartSprites;
        [SerializeField] private Image _garbageBinImg;
        [SerializeField] private Text _scoreTxt;
        private int _loading;
        private Action _onLoaded;

        private void Awake() {
            Transform heartCtnr = transform.Find("Health/HeartContainer");
            GameObject template = heartCtnr.Find("Template").gameObject;
            int count = HealthFacade.GetHealthMax?.Invoke() ?? 0;
            _heartImgs = new Image[count];
            for (int i = 0; i < count; i++) {
                _heartImgs[i] = Instantiate(template, heartCtnr).GetComponent<Image>();
            }
            template.SetActive(false);
            _garbageBinImg = transform.Find("GarbageBin/GarbageBinImg").GetComponent<Image>();
            _scoreTxt = transform.Find("Scoreboard/ScoreTxt").GetComponent<Text>();

            LoadRes();
        }

        private void LoadRes() {
            _loading = 2;
            _heartSprites = new Sprite[2];
            Addressables.LoadAssetAsync<Sprite>("tex_ui_hud_heart").Completed += handle => {
                _heartSprites[0] = handle.Result;
                OnResLoaded();
            };
            Addressables.LoadAssetAsync<Sprite>("tex_ui_hud_heart-empty").Completed += handle => {
                _heartSprites[1] = handle.Result;
                OnResLoaded();
            };
        }

        private void OnResLoaded() {
            if (--_loading <= 0) {
                _onLoaded?.Invoke();
                _onLoaded = null;
            }
        }

        private void OnEnable() {
            PlayerFacade.OnGarbageBinChanged += OnGarbageBinChanged;
            HealthFacade.OnHealthUpdated += OnHealthUpdated;
            ScoreFacade.OnScoreUpdated += OnScoreUpdated;

            OnGarbageBinChanged(PlayerFacade.GetGarbageBin?.Invoke() ?? EGarbageDef.Kitchen);
            OnHealthUpdated(HealthFacade.GetHealth?.Invoke() ?? 0);
            OnScoreUpdated(ScoreFacade.GetScore?.Invoke() ?? 0);
        }

        private void OnDisable() {
            PlayerFacade.OnGarbageBinChanged -= OnGarbageBinChanged;
            HealthFacade.OnHealthUpdated -= OnHealthUpdated;
            ScoreFacade.OnScoreUpdated -= OnScoreUpdated;
        }

        private async void OnGarbageBinChanged(EGarbageDef category) {
            // TODO: Reduce GC Alloc
            ConfGarbageBin conf = await ConfGarbageBin.Get((int) category);
            Addressables.LoadAssetAsync<Sprite>(conf.icon).Completed += handle => { _garbageBinImg.sprite = handle.Result; };
        }

        private void OnHealthUpdated(int value) {
            if (_loading <= 0) {
                OnLoaded();
            } else {
                _onLoaded += OnLoaded;
            }

            void OnLoaded() {
                int count = _heartImgs.Length;
                int i = 0;
                while (i < value) {
                    _heartImgs[i++].sprite = _heartSprites[0];
                }
                while (i < count) {
                    _heartImgs[i++].sprite = _heartSprites[1];
                }
            }
        }

        private void OnScoreUpdated(int value) {
            _scoreTxt.text = value.ToString("D8");
        }
    }
}
