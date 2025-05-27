using System;
using UnityEngine;

namespace Trell.Skyroads.Gameplay.Asteroid
{
    [RequireComponent(typeof(Collider))]
    public class EndOfTheMapCollisionEventInvoker : MonoBehaviour
    {
        public event Action AsteroidCollided;
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out AsteroidCollisionEventInvoker _))
            {
                AsteroidCollided?.Invoke();
            }
        }
    }
}