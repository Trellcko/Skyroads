using System;
using Trell.Skyroads.Extra;
using Trell.Skyroads.Gameplay.Asteroid;
using Trell.Skyroads.Infrastructure;
using Trell.Skyroads.Infrastructure.Input;
using UnityEngine;

namespace Trell.Skyroads.Gameplay.Score
{
    public class ScoreCounter : MonoBehaviour, IScore
    {
        [SerializeField] private ScoreData _scoreData;
        [SerializeField] private EndOfTheMapCollisionEventInvoker _endOfTheMapCollisionEventInvoker;
        public float CurrentValue { get; private set; }

        private IStaticDataService _staticDataService;
        private IInputService _inputService;

        private BetterTimer _betterTimer;

        private float _modificatorForTime = 1;

        public event Action ScoreUpdated;

        private void Awake()
        {
            _staticDataService = ServiceLocator.Instance.Get<IStaticDataService>();
            _inputService = ServiceLocator.Instance.Get<IInputService>();
            _betterTimer = new(1f, loop: true);
            _betterTimer.Completed += OnTimerCompleted;
        }

        private void OnEnable()
        {
            _endOfTheMapCollisionEventInvoker.AsteroidCollided += OnAsteroidPassed;
        }

        private void OnDisable()
        {
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

        private void OnAsteroidPassed()
        {
            CurrentValue += _scoreData.ScoreForPassingAsteroids;
            ScoreUpdated?.Invoke();
        }

        private void OnTimerCompleted()
        {
            CurrentValue += _scoreData.BaseScore * _modificatorForTime;
            ScoreUpdated?.Invoke();
        }

        public void Reset()
        {
            CurrentValue = 0;
        }

        public void StartCount()
        {
            _inputService.BoostPerformed += OnBoostPerformed;
            _inputService.BoostReleased += OnBoostReleased;
            _betterTimer.Reset();
        }

        public void StopCount()
        {
            _inputService.BoostPerformed -= OnBoostPerformed;
            _inputService.BoostReleased -= OnBoostReleased;
            _betterTimer.Pause();
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

    public interface IScore
    {
        void Reset();
        void StartCount();
        void StopCount();
    }
}