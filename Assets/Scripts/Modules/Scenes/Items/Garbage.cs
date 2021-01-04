using Config;
using Facade;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Scenes {
    [RequireComponent(typeof(SpriteRenderer))]
    public class Garbage : MonoBehaviour {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ConfGarbage _conf;
        [SerializeField] private bool _hasTriggered;
        [SerializeField] private Text _nameTxt;

        public ConfGarbage Conf {
            get => _conf;
            set {
                _conf = value;
                _nameTxt.text = _conf.Name;
                _spriteRenderer.sprite = _conf.Icon;
            }
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
            if (PlayerFacade.GetGarbageBin?.Invoke() == Conf.Category) {
                ScoreFacade.AddScore?.Invoke(Conf.Score);
                SceneFacade.OnGarbageDestroy?.Invoke(this, true);
            } else {
                HealthFacade.AddHealth?.Invoke(-1);
                SceneFacade.OnGarbageDestroy?.Invoke(this, false);
            }
            Destroy(gameObject);
        }
    }
}
