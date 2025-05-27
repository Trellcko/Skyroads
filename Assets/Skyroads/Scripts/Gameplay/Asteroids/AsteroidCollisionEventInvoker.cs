using System;
using UnityEngine;

namespace Trell.Skyroads.Gameplay.Asteroid
{
    [RequireComponent(typeof(Collider))]
    public class AsteroidCollisionEventInvoker : MonoBehaviour
    {
        public event Action EndOfTheMapCollided;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EndOfTheMapCollisionEventInvoker endOfTheMap))
            {
                EndOfTheMapCollided?.Invoke();
            }
        }
    }
}