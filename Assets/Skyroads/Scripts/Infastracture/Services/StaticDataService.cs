using Trell.Skyroads.Gameplay.Asteroid;
using Trell.Skyroads.Gameplay.Ship;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure
{
    public class StaticDataService : MonoBehaviour, IStaticDataService
    {
        [SerializeField] private ShipData _shipData;
        [SerializeField] private AsteroidData _asteroidData;
        [SerializeField] private TimeData _timeData;
        public ShipData GetShipData() =>
            _shipData;
        
        public TimeData GetTimeData() =>
            _timeData;
        
        public AsteroidData GetAsteroidData() =>
            _asteroidData;
    }
}