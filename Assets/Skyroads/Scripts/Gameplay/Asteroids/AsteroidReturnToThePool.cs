using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Trell.Skyroads.Gameplay.Asteroid
{
    public class AsteroidReturnToThePool : MonoBehaviour
    {
        [SerializeField] private AsteroidFacade _asteroidFacade;

        private ObjectPool<AsteroidFacade> _pool;
        
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
            _pool.Release(_asteroidFacade);
        }
    }
}