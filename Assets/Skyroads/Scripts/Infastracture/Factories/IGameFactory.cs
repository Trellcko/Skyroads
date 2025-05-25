using System.Threading.Tasks;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        Task<GameObject> CreateShip();
    }
}