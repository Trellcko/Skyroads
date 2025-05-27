using System;
using Trell.Skyroads.Gameplay.Asteroid;
using UnityEngine;

namespace Trell.Skyroads.Gameplay.Ship
{
    [RequireComponent(typeof(Collider))]
    public class ShipCollisionEventsInvoker : MonoBehaviour
    {
        public event Action AsteroidCollided;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out AsteroidFacade _))
            {
                AsteroidCollided?.Invoke();
            }      
        }
    }
}