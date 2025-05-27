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

        private void Awake()
        {
            _scoreContainer = ServiceLocator.Instance.Get<IScoreContainer>();
        }

        private void OnEnable()
        {
            _asteroidFacade.CollisionEventInvoker.EndOfTheMapCollided += OnEndOfTheMapCollided;
        }

        private void OnDisable()
        {
            _asteroidFacade.CollisionEventInvoker.EndOfTheMapCollided -= OnEndOfTheMapCollided;
        }

        public void SetPool(ObjectPool<AsteroidFacade> pool)
        {
            _pool = pool;
        }
        
        private void OnEndOfTheMapCollided()
        {
            _scoreContainer.PassedMeteorites.AddCurrency(1);
            _pool.Release(_asteroidFacade);
        }
    }
}