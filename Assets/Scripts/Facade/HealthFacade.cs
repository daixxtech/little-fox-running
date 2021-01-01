using System;

namespace Facade {
    public class HealthFacade {
        public static Func<int> GetHealth;
        public static Action<int> MinusHealth;
        public static Action<int> OnHealthUpdated;
    }
}