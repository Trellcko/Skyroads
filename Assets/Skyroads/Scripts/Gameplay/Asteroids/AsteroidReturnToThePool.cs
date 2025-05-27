using System;
using Trell.Skyroads.Gameplay.Score;
using Trell.Skyroads.Infrastructure;
using UnityEngine;
using UnityEngine.Pool;

namespace Trell.Skyroads.Gameplay.Asteroid
{
    public class AsteroidReturnToThePool : MonoBehaviour
    {
        [SerializeField] private AsteroidFacade _asteroidFacade;

        private ObjectPool<AsteroidFacade> _pool;
        
        private IScoreContainer _scoreContainer;
        private ILosingNotifiedService _losingNotifiedService;
        private bool _stopCounting;

        private void Awake()
        {
            _losingNotifiedService = ServiceLocator.Instance.Get<ILosingNotifiedService>();
            _scoreContainer = ServiceLocator.Instance.Get<IScoreContainer>();
        }

        private void OnEnable()
        {
            _losingNotifiedService.Lost += OnLost;
            _asteroidFacade.CollisionEventInvoker.EndOfTheMapCollided += OnEndOfTheMapCollided;
        }

        private void OnDisable()
        {
            _losingNotifiedService.Lost -= OnLost;
            _asteroidFacade.CollisionEventInvoker.EndOfTheMapCollided -= OnEndOfTheMapCollided;
        }

        public void SetPool(ObjectPool<AsteroidFacade> pool)
        {
            _pool = pool;
        }

        private void OnLost()
        {
            _stopCounting = true;
        }

        private void OnEndOfTheMapCollided()
        {
            if(!_stopCounting)
                _scoreContainer.PassedMeteorites.AddCurrency(1);
            _pool.Release(_asteroidFacade);
        }
    }
}