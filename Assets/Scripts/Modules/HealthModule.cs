using Facade;
using Modules.Base;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modules {
    public class HealthModule : IModule {
        private readonly int _healthMax;
        private int _health;
        public bool NeedUpdate { get; } = false;

        public HealthModule(int health) {
            _healthMax = _health = health;
        }

        public void Init() {
            HealthFacade.GetHealthMax += GetHealthMax;
            HealthFacade.GetHealth += GetHealth;
            HealthFacade.AddHealth += AddHealth;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void Dispose() {
            HealthFacade.GetHealthMax -= GetHealthMax;
            HealthFacade.GetHealth -= GetHealth;
            HealthFacade.AddHealth -= AddHealth;

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void Update() { }

        private int GetHealthMax() {
            return _healthMax;
        }

        private int GetHealth() {
            return _health;
        }

        private void AddHealth(int value) {
            _health = Mathf.Clamp(_health + value, 0, _healthMax);
            HealthFacade.OnHealthUpdated?.Invoke(_health);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            if (scene.name.StartsWith("Level")) {
                AddHealth(_healthMax);
            }
        }
    }
}