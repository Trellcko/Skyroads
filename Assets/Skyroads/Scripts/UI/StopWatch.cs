using TMPro;
using Trell.Skyroads.Gameplay;
using Trell.Skyroads.Infrastructure;
using UnityEngine;

namespace Trell.Skyroads.UI
{
    public class StopWatch : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private float _currentTime;

        private ILosingNotifiedService _losingNotifiedService;

        private bool _isWorking = true;

        private void Awake()
        {
            _losingNotifiedService = ServiceLocator.Instance.Get<ILosingNotifiedService>();
            _losingNotifiedService.Lost += OnLost;
        }

        private void OnDisable()
        {
            _losingNotifiedService.Lost -= OnLost;
        }

        private void Update()
        {
            if(!_isWorking)
                return;
            
            _currentTime += Time.deltaTime;
            int minutes = (int)(_currentTime / 60);
            int seconds = (int)(_currentTime % 60);

            string formattedTime = $"{minutes:D2}:{seconds:D2}";
            _text.SetText(formattedTime);
        }

        private void OnLost()
        {
            _isWorking = false;
        }
    }
}