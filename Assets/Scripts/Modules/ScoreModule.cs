using Facade;
using Modules.Base;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modules {
    public class ScoreModule : IModule {
        private int _score;
        public bool NeedUpdate { get; } = false;

        public void Init() {
            ScoreFacade.GetScore += GetScore;
            ScoreFacade.AddScore += AddScore;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void Dispose() {
            ScoreFacade.GetScore -= GetScore;
            ScoreFacade.AddScore -= AddScore;

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void Update() { }

        private int GetScore() {
            return _score;
        }

        private void AddScore(int value) {
            _score = Mathf.Clamp(_score + value, 0, 99999999);
            ScoreFacade.OnScoreUpdated?.Invoke(_score);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            if (scene.name.StartsWith("Level")) {
                AddScore(-_score);
            }
        }
    }
}
