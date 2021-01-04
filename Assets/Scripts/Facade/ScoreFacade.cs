using System;

namespace Facade {
    public static class ScoreFacade {
        public static Func<int> GetScore;
        public static Action<int> AddScore;
        public static Action<int> OnScoreUpdated;
    }
}
