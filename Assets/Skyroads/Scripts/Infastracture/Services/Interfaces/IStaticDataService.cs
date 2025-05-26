using Trell.Skyroads.Gameplay.Asteroid;
using Trell.Skyroads.Gameplay.Ship;

namespace Trell.Skyroads.Infrastructure
{
    public interface IStaticDataService : IService
    {
        ShipData GetShipData();
        AsteroidData GetAsteroidData();
        TimeData GetTimeData();
    }
}