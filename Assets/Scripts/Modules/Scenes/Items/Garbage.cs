using Config;
using Facade;
using UI;
using UnityEngine;

namespace Modules.Scenes {
    public class Garbage : MonoBehaviour {
        public Animator animator;
        public ConfGarbage Conf { get; set; }

        private void Awake() {
            animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other) {
            if (!"Player".Equals(other.tag)) {
                return;
            }

            if (PlayerFacade.GetGarbageBin?.Invoke() == Conf.Category) {
                ScoreFacade.AddScore?.Invoke(Conf.Score);
            } else {
                HealthFacade.MinusHealth?.Invoke(1);
                UIFacade.ShowUIByParam?.Invoke(UIDef.UI_TIPS, Conf);
            }
        }
    }
}