using TMPro;
using Trell.Skyroads.Gameplay.Score;
using UnityEngine;

namespace Trell.Skyroads.UI
{
    public class ScoreCounterUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private ScoreCounter _scoreCounter;

        private void OnEnable()
        {
            _scoreCounter.ScoreUpdated += OnScoreUpdated;
        }

        private void OnDisable()
        {
            _scoreCounter.ScoreUpdated -= OnScoreUpdated;
        }

        private void OnScoreUpdated()
        {
            _text.SetText($"Score: {_scoreCounter.CurrentValue:0}");
        }
    }
}