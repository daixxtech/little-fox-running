using Facade;
using Modules.Base;

namespace Modules {
    public class ScoreModule : IModule {
        private int _score;
        public bool NeedUpdate { get; } = false;

        public void Init() {
            ScoreFacade.GetScore += GetScore;
            ScoreFacade.AddScore += AddScore;
        }

        public void Dispose() {
            ScoreFacade.GetScore -= GetScore;
            ScoreFacade.AddScore -= AddScore;
        }

        public void Update() { }

        private int GetScore() {
            return _score;
        }

        private void AddScore(int value) {
            _score += value;
            ScoreFacade.OnScoreUpdated?.Invoke(_score);
        }
    }
}