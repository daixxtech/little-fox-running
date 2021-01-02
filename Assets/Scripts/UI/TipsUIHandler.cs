using Config;
using Facade;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class TipsUIHandler : MonoBehaviour {
        [SerializeField] private Image _iconImg;
        [SerializeField] private Text _nameTxt;
        [SerializeField] private Text _descTxt;

        private void Awake() {
            _iconImg = transform.Find("Content/IconImg").GetComponent<Image>();
            _nameTxt = transform.Find("Content/NameTxt").GetComponent<Text>();
            _descTxt = transform.Find("Content/DescTxt").GetComponent<Text>();

            transform.Find("Title/CloseBtn").GetComponent<Button>().onClick.AddListener(OnCloseBtnClicked);
            transform.Find("Operation/ConfirmBtn").GetComponent<Button>().onClick.AddListener(OnCloseBtnClicked);
        }

        private void OnEnable() {
            ConfGarbage conf = UIFacade.GetUIParam?.Invoke() as ConfGarbage;
            if (conf == null) {
                return;
            }

            _iconImg.sprite = conf.Icon;
            _nameTxt.text = conf.Name;
            _descTxt.text = conf.Description;
        }

        private void OnCloseBtnClicked() {
            UIFacade.HideUI?.Invoke(UIDef.TIPS);
        }
    }
}