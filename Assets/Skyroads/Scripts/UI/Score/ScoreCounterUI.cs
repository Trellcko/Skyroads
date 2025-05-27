using TMPro;
using Trell.Skyroads.Gameplay.Score;
using Trell.Skyroads.Infrastructure;
using UnityEngine;

namespace Trell.Skyroads.UI
{
    public class ScoreCounterUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentScoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;

        private IScore _score;

        private void Awake()
        {
            _score = ServiceLocator.Instance.Get<IScore>();
        }

        private void Start()
        {
            OnHighValueChanged();
        }

        private void OnEnable()
        {
            _score.CurrencyChanged += OnScoreCurrencyChanged;
            _score.HighValueChanged += OnHighValueChanged;
        }

        private void OnDisable()
        {
            _score.CurrencyChanged -= OnScoreCurrencyChanged;
            _score.HighValueChanged -= OnHighValueChanged;
        }

        private void OnScoreCurrencyChanged()
        {
            _currentScoreText.SetText($"Score: {_score.CurrentValue}");
        }

        private void OnHighValueChanged()
        {
            _highScoreText.SetText($"High Score: {_score.HighValue}");
        }
    }
}