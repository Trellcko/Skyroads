using System.Threading.Tasks;
using Trell.Skyroads.Gameplay.Asteroid;
using Trell.Skyroads.Gameplay.Ship;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        
        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public async Task<GameObject> CreateShip()
        {
            ShipData shipData = _staticDataService.GetShipData();
            GameObject shipPrefab = await _assetProvider.Load<GameObject>(shipData.ShipReference);
            
            GameObject spawned = Object.Instantiate(shipPrefab, shipData.SpawnPoint, Quaternion.identity);
            
            spawned.GetComponent<ShipMovement>().Init(
                shipData.BaseSpeed, _staticDataService.GetTimeData().BoostTimeSpeed, shipData.XLimit);
            
            return spawned;
        }

        public async Task<GameObject> CreateAsteroid(Vector3 asteroidPosition)
        {
            AsteroidData asteroidData = _staticDataService.GetAsteroidData();
            GameObject asteroidPrefab = await _assetProvider.Load<GameObject>(asteroidData.AssetReferences[Random.Range(0, asteroidData.AssetReferences.Count)]);
            GameObject spawned = Object.Instantiate(asteroidPrefab, asteroidPosition, Quaternion.identity);
            spawned.GetComponent<AsteroidMovement>().SetSpeed(asteroidData.BaseSpeed, asteroidData.AngularSpeed, _staticDataService.GetTimeData().BoostTimeSpeed);
            
            return spawned;
        }
    }
}