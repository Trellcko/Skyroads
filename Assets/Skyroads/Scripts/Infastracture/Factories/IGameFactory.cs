using System;
using System.Threading.Tasks;
using Trell.Skyroads.Gameplay.Asteroid;
using Trell.Skyroads.Gameplay.Ship;
using Trell.Skyroads.UI;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        Task<ShipFacade> CreateShip();
        Task<AsteroidFacade> CreateAsteroid(Vector3 asteroidPosition);
        event Action<ShipFacade> ShipCreated;
        Task<LosePopup> CreateLosePopup();
        void CleanUp();
        ShipFacade SpawnedShip { get; }
    }
}