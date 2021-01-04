using System;

namespace Facade {
    public class HealthFacade {
        public static Func<int> GetHealthMax;
        public static Func<int> GetHealth;
        public static Action<int> AddHealth;
        public static Action<int> OnHealthUpdated;
    }
}
