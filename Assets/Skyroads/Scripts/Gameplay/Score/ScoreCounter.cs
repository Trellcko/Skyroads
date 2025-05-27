using System;
using Trell.Skyroads.Extra;
using Trell.Skyroads.Gameplay.Asteroid;
using Trell.Skyroads.Gameplay.Ship;
using Trell.Skyroads.Infrastructure;
using Trell.Skyroads.Infrastructure.Factories;
using Trell.Skyroads.Infrastructure.Input;
using UnityEngine;

namespace Trell.Skyroads.Gameplay.Score
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private ScoreData _scoreData;
        [SerializeField] private EndOfTheMapCollisionEventInvoker _endOfTheMapCollisionEventInvoker;
        
        private IStaticDataService _staticDataService;
        private IInputService _inputService;
        private IScore _score;
        private ILosingNotifiedService _losingNotifiedService;

        private BetterTimer _betterTimer;

        private float _modificatorForTime = 1;
        private ShipFacade _ship;

        private bool _isCounting;
        
        private void Awake()
        {
            _staticDataService = ServiceLocator.Instance.Get<IStaticDataService>();
            _inputService = ServiceLocator.Instance.Get<IInputService>();
            _score = ServiceLocator.Instance.Get<IScoreContainer>().Score;
            _losingNotifiedService = ServiceLocator.Instance.Get<ILosingNotifiedService>();
            
            _losingNotifiedService.Lost += StopCount;
            
            _betterTimer = new(1f, loop: true);
            _betterTimer.Completed += OnTimerCompleted;
        }

        private void OnEnable()
        {
            _endOfTheMapCollisionEventInvoker.AsteroidCollided += OnAsteroidPassed;

        }

        private void OnDisable()
        {
            _losingNotifiedService.Lost -= StopCount;
            _endOfTheMapCollisionEventInvoker.AsteroidCollided -= OnAsteroidPassed;
            _inputService.BoostPerformed -= OnBoostPerformed;
            _inputService.BoostReleased -= OnBoostReleased;
        }

        private void Start()
        {
            StartCount();
        }

        private void Update()
        {
            _betterTimer.Tick();
        }

        public void Reset()
        {
            _score.Reset();
        }

        public void StartCount()
        {
            _isCounting = true;
            _inputService.BoostPerformed += OnBoostPerformed;
            _inputService.BoostReleased += OnBoostReleased;
            _betterTimer.Reset();
        }

        public void StopCount()
        {
            _isCounting = false;
            _inputService.BoostPerformed -= OnBoostPerformed;
            _inputService.BoostReleased -= OnBoostReleased;
            _betterTimer.Pause();
        }
        
        private void OnAsteroidPassed()
        {
            if(!_isCounting)
                return;
            
            _score.AddCurrency(_scoreData.ScoreForPassingAsteroids);
        }

        private void OnTimerCompleted()
        {
            _score.AddCurrency((int)(_scoreData.BaseScore * _modificatorForTime));
        }

        private void OnBoostReleased()
        {
            _modificatorForTime = 1;
        }

        private void OnBoostPerformed()
        {
            _modificatorForTime *= _staticDataService.GetTimeData().BoostTimeSpeed;
        }
    }
}