using Config;
using Facade;
using UI;
using UnityEngine;

namespace Modules.Scenes {
    [RequireComponent(typeof(SpriteRenderer))]
    public class Garbage : MonoBehaviour {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ConfGarbage _conf;
        [SerializeField] private bool _hasTriggered;

        public ConfGarbage Conf {
            get => _conf;
            set {
                _conf = value;
                _spriteRenderer.sprite = _conf.Icon;
            }
        }

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (_hasTriggered || !"Player".Equals(other.tag)) {
                return;
            }

            _hasTriggered = true;
            if (PlayerFacade.GetGarbageBin?.Invoke() == Conf.Category) {
                ScoreFacade.AddScore?.Invoke(Conf.Score);
                SceneFacade.ShowGarbageTriggerEffect?.Invoke(true, transform.position, null);
            } else {
                HealthFacade.AddHealth?.Invoke(-1);
                Time.timeScale = 0;
                SceneFacade.ShowGarbageTriggerEffect?.Invoke(false, transform.position, () => {
                    UIFacade.ShowUIByParam?.Invoke(UIDef.TIPS, Conf);
                });
            }
            Destroy(gameObject);
        }
    }
}