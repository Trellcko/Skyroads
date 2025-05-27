using Trell.Skyroads.Infrastructure;

namespace Trell.Skyroads.Gameplay.Score
{
    public interface IScoreContainer : IService
    {
        IScore Score { get; }
        ICurrency MeteoritesPassed { get; }
    }
}