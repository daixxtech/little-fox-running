using Facade;
using Modules.Base;

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
            HealthFacade.MinusHealth += MinusHealth;
        }

        public void Dispose() {
            HealthFacade.GetHealthMax -= GetHealthMax;
            HealthFacade.GetHealth -= GetHealth;
            HealthFacade.MinusHealth -= MinusHealth;
        }

        public void Update() { }

        private int GetHealthMax() {
            return _healthMax;
        }

        private int GetHealth() {
            return _health;
        }

        private void MinusHealth(int value) {
            _health -= value;
            HealthFacade.OnHealthUpdated?.Invoke(_health);
        }
    }
}