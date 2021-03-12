using Config;
using Facade;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Modules.Scenes {
    [RequireComponent(typeof(SpriteRenderer))]
    public class Garbage : MonoBehaviour {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ConfGarbage _conf;
        [SerializeField] private bool _hasTriggered;
        [SerializeField] private Text _nameTxt;

        public ConfGarbage Conf => _conf;

        public async void SetData(ConfGarbage conf) {
            _conf = conf;
            _nameTxt.text = _conf.name;
            _spriteRenderer.sprite = await Addressables.LoadAssetAsync<Sprite>(_conf.icon).Task;
        }

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _nameTxt = transform.Find("Canvas/Text").GetComponent<Text>();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (_hasTriggered || !"Player".Equals(other.tag)) {
                return;
            }

            _hasTriggered = true;
            if (PlayerFacade.GetGarbageBin?.Invoke() == (EGarbageDef) Conf.category) {
                ScoreFacade.AddScore?.Invoke(Conf.score);
                SceneFacade.OnGarbageDestroy?.Invoke(this, true);
            } else {
                HealthFacade.AddHealth?.Invoke(-1);
                SceneFacade.OnGarbageDestroy?.Invoke(this, false);
            }
            Destroy(gameObject);
        }
    }
}
