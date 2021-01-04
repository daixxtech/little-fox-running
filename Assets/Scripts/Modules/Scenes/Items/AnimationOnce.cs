using System;
using UnityEngine;

namespace Modules.Scenes {
    [RequireComponent(typeof(Animator))]
    public class AnimationOnce : MonoBehaviour {
        [SerializeField] private Animator _animator;
        public event Action animationOver;

        private void Awake() {
            _animator = GetComponent<Animator>();
        }

        private void Update() {
            AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= 1.0F) {
                gameObject.SetActive(false);
                animationOver?.Invoke();
                animationOver = null;
            }
        }
    }
}
