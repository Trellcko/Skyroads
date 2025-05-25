using Trell.Skyroads.Gameplay.Ship;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure
{
    public class StaticDataService : MonoBehaviour, IStaticDataService
    {
        [SerializeField] private ShipData _shipData;

        public ShipData GetShipData() =>
            _shipData;
    }
}