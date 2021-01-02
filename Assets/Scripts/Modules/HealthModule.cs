using Facade;
using Modules.Base;

namespace Modules {
    public class HealthModule : IModule {
        private int _health;
        public bool NeedUpdate { get; } = false;

        public void Init() {
            HealthFacade.GetHealth += GetHealth;
            HealthFacade.MinusHealth += MinusHealth;
        }

        public void Dispose() {
            HealthFacade.GetHealth -= GetHealth;
            HealthFacade.MinusHealth -= MinusHealth;
        }

        public void Update() { }

        private int GetHealth() {
            return _health;
        }

        private void MinusHealth(int value) {
            _health -= value;
            HealthFacade.OnHealthUpdated?.Invoke(_health);
        }
    }
}