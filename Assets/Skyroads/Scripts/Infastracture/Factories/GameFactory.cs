using System.Threading.Tasks;
using Trell.Skyroads.Gameplay.Ship;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private IAssetProvider _assetProvider;
        private IStaticDataService _staticDataService;
        
        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public async Task<GameObject> CreateShip()
        {
            ShipData shipData = _staticDataService.GetShipData();
            GameObject shipPrefab = await _assetProvider.Load<GameObject>(shipData.ShipReference);
            
            GameObject spawned = GameObject.Instantiate(shipPrefab, shipData.SpawnPoint, Quaternion.identity);
            
            spawned.GetComponent<ShipMovement>().Init(
                shipData.BaseSpeed, shipData.BoostSpeed, shipData.XLimit);
            
            return spawned;
        }
    }
}