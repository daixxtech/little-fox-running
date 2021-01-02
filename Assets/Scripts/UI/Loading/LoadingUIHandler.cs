using System.Collections;
using Config;
using Facade;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.Loading {
    public class LoadingUIHandler : MonoBehaviour {
        [SerializeField] private Text _tipsTxt;
        [SerializeField] private Image _progressBarImg;

        private void Awake() {
            _tipsTxt = transform.Find("Tips/Text").GetComponent<Text>();
            _progressBarImg = transform.Find("ProgressBar/ValueImg").GetComponent<Image>();
        }

        private void OnEnable() {
            ConfLoadingTips[] tips = ConfLoadingTips.Array;
            if (tips.Length != 0) {
                int randomIndex = Random.Range(0, tips.Length);
                _tipsTxt.text = tips[randomIndex].Content;
            }

            AsyncOperation operation = UIFacade.GetUIParam?.Invoke() as AsyncOperation;
            if (operation == null) {
                return;
            }
            StartCoroutine(UpdateProgress(operation));
        }

        private IEnumerator UpdateProgress(AsyncOperation operation) {
            int curValue, targetValue;
            _progressBarImg.fillAmount = curValue = 0;
            operation.allowSceneActivation = false;
            // 进度条平滑加载至当前异步操作的进度值
            while (operation.progress < 0.9F) {
                targetValue = (int) (operation.progress * 100);
                while (curValue < targetValue) {
                    _progressBarImg.fillAmount = ++curValue / 100.0F;
                    yield return CoroutineUtil.END_OF_FRAME;
                }
                yield return CoroutineUtil.END_OF_FRAME;
            }
            // 当异步加载至 90% 时，平滑加载剩余 10%
            targetValue = 100;
            while (curValue < targetValue) {
                _progressBarImg.fillAmount = ++curValue / 100.0F;
                yield return CoroutineUtil.END_OF_FRAME;
            }
            operation.allowSceneActivation = true;
            UIFacade.HideUI?.Invoke(UIDef.UI_LOADING);
        }
    }
}