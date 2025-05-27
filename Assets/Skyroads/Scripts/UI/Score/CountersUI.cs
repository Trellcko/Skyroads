using TMPro;
using Trell.Skyroads.Gameplay.Score;
using Trell.Skyroads.Infrastructure;
using UnityEngine;

namespace Trell.Skyroads.UI
{
    public class CountersUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentScoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;
        [SerializeField] private TextMeshProUGUI _passedMeteoritesText;

        private IScoreContainer _scoreContainer;

        private IScore Score => _scoreContainer.Score;
        private ICurrency PassedMeteorites => _scoreContainer.PassedMeteorites;
        
        private void Awake()
        {
            _scoreContainer = ServiceLocator.Instance.Get<IScoreContainer>();
        }

        private void Start()
        {
            OnHighValueChanged();
        }

        private void OnEnable()
        {
            PassedMeteorites.CurrencyChanged += OnPassedMeteoritesChanged;
            Score.CurrencyChanged += OnScoreCurrencyChanged;
            Score.HighValueChanged += OnHighValueChanged;
        }

        private void OnDisable()
        {
            PassedMeteorites.CurrencyChanged -= OnPassedMeteoritesChanged;
            Score.CurrencyChanged -= OnScoreCurrencyChanged;
            Score.HighValueChanged -= OnHighValueChanged;
        }

        private void OnPassedMeteoritesChanged()
        {
            _passedMeteoritesText.SetText($"Meteorites: {PassedMeteorites.CurrentValue}");
        }

        private void OnScoreCurrencyChanged()
        {
            _currentScoreText.SetText($"Score: {Score.CurrentValue}");
        }

        private void OnHighValueChanged()
        {
            _highScoreText.SetText($"High Score: {Score.HighValue}");
        }
    }
}