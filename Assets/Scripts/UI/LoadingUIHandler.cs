using System.Collections;
using Config;
using Facade;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace UI {
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

            string sceneName = UIFacade.GetUIParam?.Invoke() as string;
            if (string.IsNullOrEmpty(sceneName)) {
                return;
            }
            StartCoroutine(LoadSceneAsyncAndRefreshProgress(sceneName));
        }

        private IEnumerator LoadSceneAsyncAndRefreshProgress(string sceneName) {
            float curValue, targetValue;
            _progressBarImg.fillAmount = curValue = 0;
            yield return null; // Bug(Unity): allowSceneActivation 不生效，需在异步加载场景之前等待一小段时间
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;
            // 进度条平滑加载至当前异步操作的进度值
            while (operation.progress < 0.9F) {
                targetValue = (int) (operation.progress * 100);
                while (curValue < targetValue) {
                    _progressBarImg.fillAmount = (curValue += 0.5F) / 100.0F;
                    yield return CoroutineUtil.END_OF_FRAME;
                }
                yield return CoroutineUtil.END_OF_FRAME;
            }
            // 当异步加载至 90% 时，平滑加载剩余 10%
            targetValue = 100;
            while (curValue < targetValue) {
                _progressBarImg.fillAmount = (curValue += 0.5F) / 100.0F;
                yield return CoroutineUtil.END_OF_FRAME;
            }
            yield return new WaitForSeconds(0.5F);
            operation.allowSceneActivation = true;
        }
    }
}
