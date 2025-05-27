using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Constants;
using Trell.Skyroads.Gameplay.Asteroid;
using Trell.Skyroads.Gameplay.Ship;
using Trell.Skyroads.UI;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Trell.Skyroads.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;

        private readonly Dictionary<GameObject, ObjectPool<AsteroidFacade>> _asteroidPools = new();
        
        public ShipFacade SpawnedShip { get; private set; }
        
        public event Action<ShipFacade> ShipCreated;
        
        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public void CleanUp()
        {
            foreach (KeyValuePair<GameObject, ObjectPool<AsteroidFacade>> asteroidPool in _asteroidPools)
            {
                asteroidPool.Value.Clear();
            }
            _asteroidPools.Clear();
            _assetProvider.CleanUp();
        }

        public async Task<ShipFacade> CreateShip()
        {
            ShipData shipData = _staticDataService.GetShipData();
            GameObject shipPrefab = await _assetProvider.Load<GameObject>(shipData.ShipReference);
            
            ShipFacade spawned = Object.Instantiate(shipPrefab, shipData.SpawnPoint, Quaternion.identity).GetComponent<ShipFacade>();
            
            spawned.ShipMovement.Init(
                shipData.BaseSpeed, _staticDataService.GetTimeData().BoostTimeSpeed, shipData.XLimit);
            ShipCreated?.Invoke(spawned);
            SpawnedShip = spawned;
            return spawned;
        }

        public async Task<LosePopup> CreateLosePopup()
        {
            GameObject losePopupPrefab = await _assetProvider.Load<GameObject>(AddressableNames.LosePopup);
            return Object.Instantiate(losePopupPrefab).GetComponent<LosePopup>();
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