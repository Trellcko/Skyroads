using System.Collections.Generic;
using System.Threading.Tasks;
using Trell.Skyroads.Gameplay.Asteroid;
using Trell.Skyroads.Gameplay.Ship;
using UnityEngine;
using UnityEngine.Pool;

namespace Trell.Skyroads.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;

        private readonly Dictionary<GameObject, ObjectPool<AsteroidFacade>> _asteroidPools = new();
        
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

        public async Task<AsteroidFacade> CreateAsteroid(Vector3 asteroidPosition)
        {
            AsteroidData asteroidData = _staticDataService.GetAsteroidData();
            GameObject asteroidPrefab = await _assetProvider.Load<GameObject>(asteroidData.AssetReferences[Random.Range(0, asteroidData.AssetReferences.Count)]);
            if (!_asteroidPools.TryGetValue(asteroidPrefab, out ObjectPool<AsteroidFacade> _))
            {
                _asteroidPools.Add(asteroidPrefab, CreateAsteroidObjectPool(asteroidPrefab, asteroidData));
            }

            ObjectPool<AsteroidFacade> pool = _asteroidPools[asteroidPrefab];
            AsteroidFacade spawned = pool.Get();
            spawned.AsteroidReturnToThePool.SetPool(pool);
            spawned.transform.position = asteroidPosition;
            return spawned;
        }

        private ObjectPool<AsteroidFacade> CreateAsteroidObjectPool(GameObject asteroidPrefab, AsteroidData asteroidData)
        {
            return new(()=>
                {
                    AsteroidFacade spawned = Object.Instantiate(asteroidPrefab, Vector3.zero, Quaternion.identity).GetComponent<AsteroidFacade>();
                    spawned.AsteroidMovement.SetSpeed(asteroidData.BaseSpeed, asteroidData.AngularSpeed, _staticDataService.GetTimeData().BoostTimeSpeed);
                    return spawned;
                }, asteroid =>
                {
                    asteroid.gameObject.SetActive(true);
                },
                asteroid=> asteroid.gameObject.SetActive(false));
        }
    }
}