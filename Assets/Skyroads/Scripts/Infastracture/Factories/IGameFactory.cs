using System.Threading.Tasks;
using Trell.Skyroads.Gameplay.Asteroid;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        Task<GameObject> CreateShip();
        Task<AsteroidFacade> CreateAsteroid(Vector3 asteroidPosition);
    }
}